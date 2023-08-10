using System.Collections;
using UnityEngine;
using Cinemachine;
using System.Threading.Tasks;

namespace OROCHI
{
    // 移動するキャラクターを制御します。
    public class MoveBehaviourScript : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("大・中・小の動くスピードを指定")]
        private float LARGEspeed, MEDIUMspeed, SMALLspeed;

        [SerializeField]
        [Tooltip("大・中・小のジャンプ力を指定")]
        private float LARGEup, MEDIUMup, SMALLup;

        [SerializeField]
        [Tooltip("大中小のそれぞれのオブジェクトを指定します。")]
        private GameObject[] bodies = null;
        [SerializeField]
        private GameObject[] attackareas = null;

        [SerializeField]
        [Tooltip("大・中・小の質量")]
        public float[] mass = null;

        // プレイヤーの大きさステート
        enum BodySize
        {
            Small,
            Medium,
            Large,
        }

        // 現在のキャラクターサイズ
        BodySize currentBodySize = BodySize.Medium;

        [SerializeField]
        [Tooltip("回避の幅を指定(上から小・中・大)")]
        private float[] AvoidPower = new float[3];

        // avoidPowerをもとに実際に回避に働く力
        private float AvoidSpeed;

        // 回避中かどうかのフラグ
        private bool Avoiding = false;

        // 回避時間
        private float avoidTime;

        // アニメーション用の移動入力値
        private float walkSpeed;

        [SerializeField]
        [Tooltip("火球のプレハブの取得")]
        private GameObject shellPrefab;

        [SerializeField]
        [Tooltip("カメラの切り替え")]
        private CinemachineVirtualCamera[] VirtualCamera = null;

        [SerializeField]
        [Tooltip("オロチモデルの頭部")]
        private Transform Orochihead = null;

        [SerializeField]
        [Tooltip("変身のクールタイムを指定")]
        float ChangeCoolTime = 5;

        // 実際のクールタイム
        float CoolTime;

        [SerializeField]
        [Tooltip("火球のクールタイムを指定")]
        float shotCoolTime = 0;

        // 実際のクールタイム
        float ShotCoolTime;

        // チェンジを可能にするトリガー
        public bool isChange = false;
        // 火球を発射できるトリガー
        public bool shot = false;

        // AnimatorのパラメーターID
        static readonly int isAvoidId = Animator.StringToHash("isAvoid");

        // 現在のAnimator(大中小のいずれか)
        Animator currentAnimator = null;

        // 攻撃ボタンを押しているかどうかのフラグ
        private bool ButtonEnabled = true;

        [SerializeField]
        [Tooltip("UIの指定")]
        public UI ui = null;

        [Tooltip("Avatarオブジェクトへの参照")]
        public GameObject avatar = null;

        [Tooltip("Rigidbodyの参照")]
        public BoxCollider boxCol;

        [SerializeField]
        [Tooltip("回復エフェクトの指定")]
        public GameObject HealObject;

        [SerializeField]
        [Tooltip("砂埃エフェクトの指定")]
        public GameObject RunEffect;

        [SerializeField]
        [Tooltip("ひっかきエフェクトの指定")]
        public GameObject ClawEffect;

        [SerializeField]
        [Tooltip("見えない壁に当たっている時のエフェクトの指定")]
        public GameObject WallEffect;

        [SerializeField]
        [Tooltip("サイズ変更エフェクトの指定")]
        public GameObject ChangeEffect;

        // ひっかきエフェクトのオブジェクト
        GameObject claw;

        // 変身エフェクトの向き
        Quaternion EffectAngle = Quaternion.Euler(-90f, 0f, 0f);

        // プレイヤーの状態を表します
        enum PlayerState
        {
            // 歩き
            Walk,
            // ジャンプ中
            Jumping,
            // 回避中
            Avoid,
            // 攻撃
            Attack,
            // ゲームオーバー
            Dead,
            // 無敵
            Invincible,
            // クリア後
            Clear,
        }
        // 現在のプレイヤーの状態
        PlayerState currentState = PlayerState.Walk;


        [SerializeField]
        [Tooltip("接地判定")]
        private bool isGrounded = false;

        [SerializeField]
        [Tooltip("被ダメージエフェクト（火花）")]
        private GameObject damagefire;

        [SerializeField]
        [Tooltip("被ダメージエフェクト")]
        private GameObject damaged;

        // コンポーネントを事前に参照しておく変数
        new Rigidbody rigidbody;

        // 今のスピードとジャンプ力
        private float speed = 5.0f;
        private float upforce = 100f;

        // Start is called before the first frame update
        void Start()
        {
            Time.timeScale = 1;
            isGrounded = true;
            isChange = true;
            shot = true;
            rigidbody = GetComponent<Rigidbody>();
            rigidbody.mass = 10;
            boxCol = GetComponent<BoxCollider>();

            // 初めは普通の状態
            bodies[0].SetActive(false);
            bodies[1].SetActive(true);
            bodies[2].SetActive(false);
            attackareas[0].SetActive(false);
            attackareas[1].SetActive(false);
            attackareas[2].SetActive(false);
            currentAnimator = bodies[1].GetComponent<Animator>();
            currentBodySize = BodySize.Medium;

            VirtualCamera[1].Priority = 100;
            CoolTime = ChangeCoolTime;
            ShotCoolTime = shotCoolTime;

            currentAnimator.SetBool("isWalk", false);

            boxCol.center = new Vector3(0f, 1f, 0f);
            boxCol.size = new Vector3(1.5f, 2f, 3f);
        }



        // Update is called once per frame
        void Update()
        {
            switch (currentState)
            {
                case PlayerState.Walk:
                    UpdateForWalkState();
                    break;
                case PlayerState.Jumping:
                    UpdateForJumpingState();
                    break;
                case PlayerState.Avoid:
                    break;
                case PlayerState.Attack:
                    break;
                case PlayerState.Clear:
                    UpdateForClearState();
                    break;
                case PlayerState.Invincible:
                    break;
            }

            // クールタイムの経過時間
            if (CoolTime >= 0)
            {
                CoolTime -= Time.deltaTime;
            }
            if (CoolTime < 0)
            {
                isChange = true;
            }

            // 火球のクールタイムの経過時間
            if (ShotCoolTime >= 0)
            {
                ShotCoolTime -= Time.deltaTime;
            }
            if (ShotCoolTime < 0)
            {
                shot = true;
            }

            // HPが0の時
            if (StageScene.Instance.playerhp == 0)
            {
                SetDeadState();
            }
        }

        void UpdateForWalkState()
        {
            switch (currentBodySize)
            {
                case BodySize.Small:
                    speed = SMALLspeed;
                    break;
                case BodySize.Medium:
                    speed = MEDIUMspeed;
                    break;
                case BodySize.Large:
                    speed = LARGEspeed;
                    break;
                default:
                    break;
            }
            RunEffect.SetActive(true);
        }

        /// <summary>
        /// ジャンプ準備の処理
        /// </summary>
        void UpdateForJumpingState()
        {
            speed = 5;
            RunEffect.SetActive(false);
        }

        /// <summary>
        /// クリア時の処理
        /// </summary>
        void UpdateForClearState()
        {
            currentAnimator.SetBool("isWalk", false);
        }

        // Walkステートに遷移させます。
        public void SetWalkState()
        {
            if (currentBodySize == BodySize.Small)
            {
                boxCol.center = new Vector3(0f, 1f, 0f);
                boxCol.size = new Vector3(1f, 2f, 1.5f);
            }
            else if (currentBodySize == BodySize.Medium)
            {
                boxCol.center = new Vector3(0f, 1f, 0f);
                boxCol.size = new Vector3(1.5f, 2f, 3f);
            }
            else if (currentBodySize == BodySize.Large)
            {
                boxCol.center = new Vector3(0f, 1.8f, 0f);
                boxCol.size = new Vector3(2f, 3.6f, 4.5f);
            }

            attackareas[0].SetActive(false);
            attackareas[1].SetActive(false);
            attackareas[2].SetActive(false);

            currentState = PlayerState.Walk;
        }

        #region ステート毎に遷移させる
        // Jumpingステートに遷移させます。
        public void SetJumpingState()
        {
            currentState = PlayerState.Jumping;
        }

        // Attackステートに遷移させます。
        public void SetAttackState()
        {
            currentState = PlayerState.Attack;
        }

        public void SetDeadState()
        {
            currentState = PlayerState.Dead;
            Time.timeScale = 0;
        }

        public void SetInvincible()
        {
            currentState = PlayerState.Invincible;
        }

        public void SetClearState()
        {
            // クリア演出後に動きを止める
            currentAnimator.SetFloat("WalkSpeed", 0);
            currentState = PlayerState.Clear;
        }
        #endregion

        // 指定した方向へ移動します。
        public void Move(Vector3 motion)
        {
            // WalkとJumpingの時だけ
            if (currentState == PlayerState.Walk || currentState == PlayerState.Jumping)
            {
                // プレイヤーの前後左右の移動
                var velocity = motion;
                // 地上歩行キャラクターを標準とするのでy座標移動は無視
                velocity.y = 0;

                // 入力値の大きい方をアニメーションのスピードに代入
                walkSpeed = Mathf.Max(Mathf.Abs(velocity.x),Mathf.Abs(velocity.z));

                if (velocity.sqrMagnitude >= 0.0001f)
                {
                    // プレイヤーの向きを回転
                    avatar.transform.LookAt(transform.position + velocity.normalized, Vector3.up);
                    velocity *= speed;
                    // 歩きアニメーションを再生
                    currentAnimator.SetBool("isWalk", true);
                }
                else if (velocity.sqrMagnitude <= 0)
                {
                    // 歩きアニメーションの停止
                    currentAnimator.SetBool("isWalk", false);
                }

                currentAnimator.SetFloat("WalkSpeed", walkSpeed);

                // velocityに移動量を代入
                velocity.y = rigidbody.velocity.y;
                rigidbody.velocity = velocity;
            }
        }

        /// <summary>
        /// 回避行動
        /// </summary>
        public void Avoid()
        {
            if (!Avoiding)
            {
                Avoiding = true;
                currentAnimator.SetTrigger(isAvoidId);
                // 無敵状態
                SetInvincible();
               
                // コライダーを小さくしてすり抜けを表現
                boxCol.center = new Vector3(0, 0.25f, 0);
                boxCol.size = new Vector3(1f, 0.5f, 1f);

                if (currentBodySize == BodySize.Small)
                {
                    AvoidSpeed = AvoidPower[0];
                    avoidTime = 0.3f;
                }
                else if(currentBodySize == BodySize.Medium)
                {
                    AvoidSpeed = AvoidPower[1];
                    avoidTime = 0.5f;
                }
                else if(currentBodySize == BodySize.Large)
                {
                    AvoidSpeed = AvoidPower[2];
                    avoidTime = 0.8f;
                }

                // 
                StartCoroutine(DelayCoroutine(avoidTime));
                rigidbody.AddForce(avatar.transform.forward * AvoidSpeed, ForceMode.Impulse);
            }
        }


        /// <summary>
        /// ジャンプする
        /// </summary>
        public void Jump()
        {
            if (isGrounded == true)
            {
                if (currentState != PlayerState.Clear)
                {
                    // spaceが押されたらジャンプ
                    rigidbody.AddForce(transform.up * upforce / 2, ForceMode.Impulse);
                    isGrounded = false;

                    currentAnimator.SetTrigger("isJump");

                    SetJumpingState();
                }
            }
        }


        /// <summary>
        /// 攻撃する
        /// </summary>
        public async void Fire()
        {
            // 接地状態なら
            if (isGrounded == true)
            {
                // 攻撃ボタンを押してなければ
                if(ButtonEnabled == true)
                {
                    if (currentState != PlayerState.Clear)
                    {
                        // 攻撃状態に移行
                        SetAttackState();

                        currentAnimator.SetTrigger("isAttack");

                        ButtonEnabled = false;

                        // 攻撃時に少し前進
                        rigidbody.AddForce(avatar.transform.forward * 10, ForceMode.Impulse);
                        if (currentBodySize == BodySize.Large)
                        {
                            // 0.7秒後
                            await Task.Delay(700);

                            // ひっかきエフェクトを生成
                            claw = Instantiate(ClawEffect, attackareas[2].transform.position, attackareas[2].transform.rotation);
                            claw.transform.localScale = new Vector3(2.5f, 1f, 1f);

                            // 大きい時の攻撃範囲をアクティブ化
                            attackareas[0].SetActive(false);
                            attackareas[1].SetActive(false);
                            attackareas[2].SetActive(true);
                        }
                        else if (currentBodySize == BodySize.Medium)
                        {
                            // 0.7秒後
                            await Task.Delay(700);

                            // ひっかきエフェクトを生成
                            claw = Instantiate(ClawEffect, attackareas[1].transform.position, attackareas[1].transform.rotation); 

                            // 通常時の攻撃範囲をアクティブ化
                            attackareas[0].SetActive(false);
                            attackareas[1].SetActive(true);
                            attackareas[2].SetActive(false);
                        }
                        else if (currentBodySize == BodySize.Small)
                        {
                            // 0.3秒後
                            await Task.Delay(300);

                            // ひっかきエフェクトを生成
                            claw = Instantiate(ClawEffect, attackareas[0].transform.position, attackareas[0].transform.rotation);

                            // 小さい時の攻撃範囲をアクティブ化
                            attackareas[0].SetActive(true);
                            attackareas[1].SetActive(false);
                            attackareas[2].SetActive(false);
                        }

                        // 攻撃の後処理
                        StartCoroutine(ButtonCoroutine());
                    }

                }
            }
        }

        /// <summary>
        /// 火球攻撃
        /// </summary>
        public async void ShotAttack()
        {
            if (shot == true)
            {
                // 大きい状態のみ
                if (currentBodySize == BodySize.Large)
                {
                    currentAnimator.SetTrigger("isBeam");
                    shot = false;
                    // 火球のクールタイムをリセット
                    ShotCoolTime = shotCoolTime;
                    await Task.Delay(800);
                    // アニメーションに合わせて音を再生
                    SE.Instance.FireSE();
                    // 火球オブジェクトを生成
                    GameObject shell = Instantiate(shellPrefab, Orochihead.transform.position, Quaternion.identity);
                    Rigidbody shellRb = shell.GetComponent<Rigidbody>();
                    // 弾速を設定
                    shellRb.AddForce(Orochihead.transform.forward * 1500);
                    // 1秒後に破壊
                    Destroy(shell, 1.0f);
                }
            }
        }

        // 衝突判定
        void OnTriggerEnter(Collider collision)
        {
            if (currentState == PlayerState.Walk || currentState == PlayerState.Jumping || currentState == PlayerState.Attack)
            {
                // 敵の武器に当たったら
                if (collision.CompareTag("Enemy_Weapon"))
                {
                    SE.Instance.Damaged();
                    // 被ダメエフェクトを生成
                    GameObject effectplay = Instantiate(damaged, this.transform.position, Quaternion.identity);
                    Destroy(effectplay, 1.5f);
                    StageScene.Instance.Damage();
                }
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            // 接地判定
            if (collision.gameObject.tag == "ground")
            {
                isGrounded = true;
                if(currentState != PlayerState.Clear)
                {
                    SetWalkState();
                }
            }

            if (currentState == PlayerState.Walk || currentState == PlayerState.Jumping || currentState == PlayerState.Attack)
            {
                // 敵の火球に当たったら
                if (collision.gameObject.tag == "Fire")
                {
                    StageScene.Instance.Damage();
                    // 被ダメエフェクトを生成
                    GameObject fire = Instantiate(damagefire, this.transform.position, Quaternion.identity);
                    Destroy(fire, 2.0f);
                }
            }

            // 透明の壁に当たったら
            if (collision.gameObject.tag == "Wall")
            {
                // 衝突した座標を取得
                Vector3 hitPos = collision.contacts[0].point;
                Vector3 effectVec = this.transform.position - hitPos;
                Vector3 rotation = Vector3.zero - hitPos;
                // プレイヤーと壁の間でエフェクトの向きを調整
                Quaternion quaternion = Quaternion.LookRotation(rotation);
                // 壁とプレイヤーの間にエフェクトを生成
                GameObject wallEffect = Instantiate(WallEffect, hitPos, quaternion);
                // 2秒後に破壊
                Destroy(wallEffect, 2.0f);
            }
        }

        /// <summary>
        /// 一定時間遅らせる
        /// </summary>
        /// <param name="time">遅らせたい時間</param>
        /// <returns></returns>
        public IEnumerator DelayCoroutine(float time)
        {
            // 1秒間待つ
            yield return new WaitForSeconds(time);
            Avoiding = false;
            SetWalkState();
        }

        /// <summary>
        /// 攻撃の後処理
        /// </summary>
        /// <returns></returns>
        public IEnumerator ButtonCoroutine()
        {
            // 0.4秒待つ
            yield return new WaitForSeconds(0.4f);
            SetWalkState();
            // 攻撃ボタンを押せる状態に変更
            ButtonEnabled = true;
            // ひっかきエフェクトを破壊
            Destroy(claw, 2f);
        }


        #region プレイヤーの大きさを変形
        /// <summary>
        /// 大きい状態
        /// </summary>
        public void Large()
        {
            //変身エフェクト
            Instantiate(ChangeEffect, this.transform.position, EffectAngle); //パーティクル用ゲームオブジェクト生成
            SE.Instance.Change();
            // 大きい状態のコライダーに変更
            boxCol.center = new Vector3(0f, 1.8f, 0f);
            boxCol.size = new Vector3(2f, 3.6f, 4.5f);
            // ジャンプ力を変更
            upforce = LARGEup;
            // 重量感を変更
            rigidbody.mass = mass[0];

            // 大きい状態のモデルを表示
            bodies[0].SetActive(false);
            bodies[1].SetActive(false);
            bodies[2].SetActive(true);

            // 大きい時用のカメラを使用
            VirtualCamera[0].Priority = 10;
            VirtualCamera[1].Priority = 10;
            VirtualCamera[2].Priority = 100;

            // 大きい状態に変更
            currentBodySize = BodySize.Large;
            currentAnimator = bodies[2].GetComponent<Animator>();

            // UIで大きい状態を表示
            ui.ChangeNumber(2);

            // 変身用のクールタイムを初期の数値にリセット
            ResetCoolTime();
        }

        /// <summary>
        /// 通常状態
        /// </summary>
        public void Medium()
        {
            //変身エフェクト
            Instantiate(ChangeEffect, this.transform.position, EffectAngle); //パーティクル用ゲームオブジェクト生成
            SE.Instance.Change();

            // 通常状態のコライダーに変更
            boxCol.center = new Vector3(0f, 1f, 0f);
            boxCol.size = new Vector3(1.5f, 2f, 3f);

            // ジャンプ力を変更
            upforce = MEDIUMup;
            // 重量感を変更
            rigidbody.mass = mass[1];

            // 通常状態のモデルを表示
            bodies[0].SetActive(false);
            bodies[1].SetActive(true);
            bodies[2].SetActive(false);

            // 通常状態のカメラを使用
            VirtualCamera[0].Priority = 10;
            VirtualCamera[1].Priority = 100;
            VirtualCamera[2].Priority = 10;

            // 通常状態に変更
            currentBodySize = BodySize.Medium;
            currentAnimator = bodies[1].GetComponent<Animator>();

            // UIで通常状態を表示
            ui.ChangeNumber(1);

            // 変身用のクールタイムを初期の数値にリセット
            ResetCoolTime();
        }

        /// <summary>
        /// 小さい状態
        /// </summary>
        public void Small()
        {
            Debug.Log("小型");

            //変身エフェクト
            Instantiate(ChangeEffect, this.transform.position, EffectAngle);
            SE.Instance.Change();

            // 小さい状態のコライダーに変更
            boxCol.center = new Vector3(0f, 1f, 0f);
            boxCol.size = new Vector3(1f, 2f, 1.5f);

            // ジャンプ力を変更
            upforce = SMALLup;
            // 重量感を変更
            rigidbody.mass = mass[2];

            // 小さい状態のモデルを表示
            bodies[0].SetActive(true);
            bodies[1].SetActive(false);
            bodies[2].SetActive(false);

            // 小さい状態のカメラを使用
            VirtualCamera[0].Priority = 100;
            VirtualCamera[1].Priority = 10;
            VirtualCamera[2].Priority = 10;

            // 小さい状態に変更
            currentBodySize = BodySize.Small;
            currentAnimator = bodies[0].GetComponent<Animator>();

            // UIで小さい状態を表示
            ui.ChangeNumber(0);

            // 変身用のクールタイムを初期の数値にリセット
            ResetCoolTime();
        }
        #endregion

        /// <summary>
        /// 一段階大きくなる処理
        /// </summary>
        public void BodyUp()
        {
            switch (currentBodySize)
            {
                case BodySize.Small:
                    StageScene.Instance.Change();
                    Medium();
                    break;
                case BodySize.Medium:
                    StageScene.Instance.Change();
                    Large();
                    break;
                case BodySize.Large:
                default:
                    break;
            }
        }

        /// <summary>
        /// 一段階小さくなる処理
        /// </summary>
        public void BodyDown()
        {
            switch (currentBodySize)
            {
                case BodySize.Large:
                    StageScene.Instance.Change();
                    Medium();
                    break;
                case BodySize.Medium:
                    StageScene.Instance.Change();
                    Small();
                    break;
                case BodySize.Small:
                default:
                    break;
            }
        }

        /// <summary>
        /// 回復中のエフェクト処理
        /// </summary>
        public void Heal()
        {
            Debug.Log("回復");
            Instantiate(HealObject, this.transform.position, Quaternion.identity);
        }

        /// <summary>
        /// 変身のクールタイムのリセット
        /// </summary>
        public void ResetCoolTime()
        {
            CoolTime = ChangeCoolTime;
            isChange = false;
        }
    }
}
