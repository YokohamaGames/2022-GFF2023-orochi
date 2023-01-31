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
        [Tooltip("回避の幅を指定")]
        private float AvoidSpeed;

        private bool Avoiding = true;

        //private Vector3 Com;

        //火球のプレハブの取得
        [SerializeField]
        private GameObject shellPrefab;

        [SerializeField]
        [Tooltip("カメラの切り替え")]
        private CinemachineVirtualCamera[] VirtualCamera = null;

        [SerializeField]
        private Transform Orochihead = null;
        [SerializeField]
        [Tooltip("変身のクールタイムを指定")]
        float ChangeCoolTime = 10;

        // 実際のクールタイム
        float CoolTime;

        [SerializeField]
        [Tooltip("火球のクールタイム")]
        float shotCoolTime = 0;
        float ShotCoolTime;

        // チェンジを可能にするトリガー
        public bool isChange = false;
        // 火球を発射できるトリガー
        public bool shot = false;

        // AnimatorのパラメーターID
        static readonly int isAvoidId = Animator.StringToHash("isAvoid");

        // 現在のAnimator(大中小のいずれか)
        Animator currentAnimator = null;

        private bool ButtonEnabled = true;

        // UIの指定
        [SerializeField]
        public UI ui = null;

        // Avatarオブジェクトへの参照
        public GameObject avatar = null;

        // Rigidbodyの参照
        public BoxCollider boxCol;

        //回復エフェクトの指定
        [SerializeField]
        public GameObject HealObject;

        // 砂埃エフェクトの指定
        [SerializeField]
        public GameObject RunEffect;

        //サイズ変更エフェクトの指定
        [SerializeField]
        public GameObject ChangeEffect;

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
        private bool isGrounded = false;

        [SerializeField]
        private GameObject damagefire;

        [SerializeField]
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
            //rigidbody.centerOfMass = Com;
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

        void UpdateForJumpingState()
        {
            speed = 5;
            RunEffect.SetActive(false);
        }

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
            //await Task.Delay(300);
            Time.timeScale = 0;
        }

        public void SetInvincible()
        {
            currentState = PlayerState.Invincible;
        }

        public void ClearState()
        {
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
                if (velocity.sqrMagnitude >= 0.0001f)
                {
                    avatar.transform.LookAt(transform.position + velocity.normalized, Vector3.up);
                    velocity *= speed;

                    currentAnimator.SetBool("isWalk", true);
                }
                else if (velocity.sqrMagnitude <= 0)
                {
                    currentAnimator.SetBool("isWalk", false);
                }
                velocity.y = rigidbody.velocity.y;
                rigidbody.velocity = velocity;
            }
        }

        // 回避
        public void Avoid()
        {
            if (Avoiding)
            {
                Avoiding = false;
                currentAnimator.SetTrigger(isAvoidId);
                SetInvincible();
                StartCoroutine(DelayCoroutine());
               
                boxCol.center = new Vector3(0, 0.25f, 0);
                boxCol.size = new Vector3(1f, 0.5f, 1f);
                rigidbody.AddForce(avatar.transform.forward * AvoidSpeed, ForceMode.Impulse);
                //await Task.Delay(2000);
                //Avoiding = true;
            }
        }


        // ジャンプします。
        public void Jump()
        {
            Debug.Log(upforce);
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


        // 攻撃します
        public void Fire()
        {
            if (isGrounded == true)
            {
                if(ButtonEnabled == true)
                {
                    if (currentState != PlayerState.Clear)
                    {
                        SetAttackState();

                        currentAnimator.SetTrigger("isAttack");

                        ButtonEnabled = false;

                        rigidbody.AddForce(avatar.transform.forward * 10, ForceMode.Impulse);
                        if (currentBodySize == BodySize.Large)
                        {
                            attackareas[0].SetActive(false);
                            attackareas[1].SetActive(false);
                            attackareas[2].SetActive(true);
                        }
                        else if (currentBodySize == BodySize.Medium)
                        {
                            attackareas[0].SetActive(false);
                            attackareas[1].SetActive(true);
                            attackareas[2].SetActive(false);
                        }
                        else if (currentBodySize == BodySize.Small)
                        {
                            attackareas[0].SetActive(true);
                            attackareas[1].SetActive(false);
                            attackareas[2].SetActive(false);
                        }

                        StartCoroutine(ButtonCoroutine());
                    }

                }
            }
        }

        // 火球
        public async void ShotAttack()
        {
            if (shot == true)
            {
                if (currentBodySize == BodySize.Large)
                {
                    currentAnimator.SetTrigger("isBeam");
                    shot = false;
                    ShotCoolTime = shotCoolTime;
                    await Task.Delay(800);
                    SE.Instance.FireSE();
                    GameObject shell = Instantiate(shellPrefab, Orochihead.transform.position, Quaternion.identity);
                    Rigidbody shellRb = shell.GetComponent<Rigidbody>();
                    // 弾速を設定
                    shellRb.AddForce(Orochihead.transform.forward * 1500);
                    Destroy(shell, 1.0f);
                }
            }
        }

        // 衝突判定
        void OnTriggerEnter(Collider collision)
        {
            if (currentState == PlayerState.Walk || currentState == PlayerState.Jumping || currentState == PlayerState.Attack)
            {
                if (collision.CompareTag("Enemy_Weapon"))
                {
                    SE.Instance.Damaged();
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
                SetWalkState();
            }

            if (currentState == PlayerState.Walk || currentState == PlayerState.Jumping || currentState == PlayerState.Attack)
            {
                if (collision.gameObject.tag == "Fire")
                {
                    StageScene.Instance.Damage();
                    GameObject fire = Instantiate(damagefire, this.transform.position, Quaternion.identity);
                    Destroy(fire, 2.0f);
                }
            }
        }


        public IEnumerator DelayCoroutine()
        {
            // 1秒間待つ
            yield return new WaitForSeconds(1);
            Avoiding = true;
            SetWalkState();
        }

        public IEnumerator ButtonCoroutine()
        {
            // 2秒待つ
            yield return new WaitForSeconds(1);
            Debug.Log("攻撃");
            SetWalkState();
            ButtonEnabled = true;
        }


        #region プレイヤーの大きさを変形
        // 大きい時
        public void Large()
        {
            //変身エフェクト
            Instantiate(ChangeEffect, this.transform.position, EffectAngle); //パーティクル用ゲームオブジェクト生成
            boxCol.center = new Vector3(0f, 1.8f, 0f);
            boxCol.size = new Vector3(2f, 3.6f, 4.5f);
            upforce = LARGEup;
            rigidbody.mass = mass[0];

            bodies[0].SetActive(false);
            bodies[1].SetActive(false);
            bodies[2].SetActive(true);

            VirtualCamera[0].Priority = 10;
            VirtualCamera[1].Priority = 10;
            VirtualCamera[2].Priority = 100;
            currentBodySize = BodySize.Large;
            currentAnimator = bodies[2].GetComponent<Animator>();

            ui.ChangeNumber(2);

            ResetCoolTime();
        }

        // 中型の時
        public void Medium()
        {
            Debug.Log("中型");

            //変身エフェクト
            Instantiate(ChangeEffect, this.transform.position, EffectAngle); //パーティクル用ゲームオブジェクト生成
            boxCol.center = new Vector3(0f, 1f, 0f);
            boxCol.size = new Vector3(1.5f, 2f, 3f);
            upforce = MEDIUMup;
            rigidbody.mass = mass[1];

            bodies[0].SetActive(false);
            bodies[1].SetActive(true);
            bodies[2].SetActive(false);

            VirtualCamera[0].Priority = 10;
            VirtualCamera[1].Priority = 100;
            VirtualCamera[2].Priority = 10;
            currentBodySize = BodySize.Medium;
            currentAnimator = bodies[1].GetComponent<Animator>();

            ui.ChangeNumber(1);

            ResetCoolTime();
        }

        // 小さい時
        public void Small()
        {
            Debug.Log("小型");

            //変身エフェクト
            Instantiate(ChangeEffect, this.transform.position, EffectAngle); //パーティクル用ゲームオブジェクト生成
            boxCol.center = new Vector3(0f, 1f, 0f);
            boxCol.size = new Vector3(1f, 2f, 1.5f);
            upforce = SMALLup;
            rigidbody.mass = mass[2];

            bodies[0].SetActive(true);
            bodies[1].SetActive(false);
            bodies[2].SetActive(false);

            VirtualCamera[0].Priority = 100;
            VirtualCamera[1].Priority = 10;
            VirtualCamera[2].Priority = 10;
            currentBodySize = BodySize.Small;
            currentAnimator = bodies[0].GetComponent<Animator>();

            ui.ChangeNumber(0);

            ResetCoolTime();
        }
        #endregion

        public void BodyUp()
        {
            switch (currentBodySize)
            {
                case BodySize.Small:
                    Medium();
                    break;
                case BodySize.Medium:
                    Large();
                    break;
                case BodySize.Large:
                default:
                    break;
            }
        }

        public void BodyDown()
        {
            switch (currentBodySize)
            {
                case BodySize.Large:
                    Medium();
                    break;
                case BodySize.Medium:
                    Small();
                    break;
                case BodySize.Small:
                default:
                    break;
            }
        }

        //回復中のエフェクト処理
        public void Heal()
        {
            Debug.Log("回復");
            Instantiate(HealObject, this.transform.position, Quaternion.identity); //パーティクル用ゲームオブジェクト生成
                                                                                   //playerhp += 1;
        }

        // 変身のクールタイムのリセット
        public void ResetCoolTime()
        {
            CoolTime = ChangeCoolTime;
            isChange = false;
        }

    }
}
