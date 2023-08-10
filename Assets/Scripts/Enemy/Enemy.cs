using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEditor;

namespace OROCHI
{
    //敵のAIとステータス設定
    public class Enemy : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("敵の追跡時歩行スピードを設定")]
        private float chasespeed;

        [SerializeField]
        [Tooltip("攻撃準備時の歩行スピードを設定")]
        private float attackreadyspeed = 1;

        [SerializeField]
        [Tooltip("追跡目標の設定")]
        private Transform target = null;

        [SerializeField]
        [Tooltip("アニメーターの設定")] 
        private Animator animator = null;

        [SerializeField]
        [Tooltip("敵の回転速度を設定します")]
        private float rotmax;

        [SerializeField]
        [Tooltip("子オブジェクトを取得")]
        private SearchArea searcharea;

        [SerializeField]
        [Tooltip("子オブジェクトを取得")]
        private AttackArea attackArea;

        [SerializeField]
        [Tooltip("敵の持つ武器の当たり判定を取得")]
        private Collider weaponcollider;

        [SerializeField]
        [Tooltip("ステート遷移を遅らせる時間")]
        private float transition_time;

        [SerializeField]
        [Tooltip("火球のプレハブの取得")]
        private GameObject shellprefab;

        [SerializeField]
        [Tooltip("敵撃破エフェクト")]
        private GameObject defeateffect;

        [SerializeField]
        [Tooltip("ダメージエフェクト")]
        private GameObject damageeffect;

        [SerializeField]
        [Tooltip("敵の左手の座標を取得します")]
        private Transform enemy_l_hand;

        [SerializeField]
        [Tooltip("敵のHpBarを参照")]
        public Slider enemyhpbar;

        [SerializeField]
        [Tooltip("剣のEffectを取得")]
        private GameObject swordeffect;

        [SerializeField]
        [Tooltip("被ダメエフェクト")]
        private GameObject HitEffect;

        [SerializeField]
        [Tooltip("攻撃準備から攻撃までの時間の設定")]
        private float timetoattack = 2;

        [SerializeField]
        [Tooltip("敵撃破時の敵の消滅までの時間の設定")]
        private float deleteenemytime;

        //攻撃までの待機時間を設定した値にリセットする変数
        float timetoattackreset;

        [SerializeField]
        [Tooltip("HpBarの取得")]
        private Image hp;

        [SerializeField]
        [Tooltip("火球のクールタイム")]
        float timefire = 1.5f;
        float timetoatk = 0;

        // Search状態のフラグ
        public bool isSearch = false;
        // 攻撃状態のフラグ
        public bool isAttacks = false;
        // 遠距離攻撃状態のフラグ
        public bool isLongAttacks = false;
        // 体力が0になった状態のフラグ
        public bool isDead = false;
        // 移動速度用の変数
        float speed = 0;
        // コンポーネントを事前に参照しておく変数
        new Rigidbody rigidbody;

        [Header("SE")]
        [SerializeField] private AudioClip fire;
        [SerializeField] private AudioClip swing;
        [SerializeField] private AudioClip chargedamaged;

        float sp = 1.0f;

        // AnimatorのパラメーターID
        int baseLayerIndex = -1;
        static readonly int LocomotionHash = Animator.StringToHash("Base Layer.Locomotion");
        static readonly int attackReadyHash = Animator.StringToHash("Base Layer.AttackReady");
        static readonly int isDiscover = Animator.StringToHash("isDiscover");
        static readonly int isLost = Animator.StringToHash("isLost");
        static readonly int isAttackReady = Animator.StringToHash("isAttackReady");
        static readonly int isAttack = Animator.StringToHash("isAttack");
        static readonly int isAttack2 = Animator.StringToHash("isAttack2");
        static readonly int isAttack3 = Animator.StringToHash("isAttack3");
        static readonly int isLongAttack = Animator.StringToHash("isLongAttack");
        static readonly int speedId = Animator.StringToHash("Speed");
        static readonly int DeadId = Animator.StringToHash("Dead");

        //敵のステートパターン
        enum EnemyState
        {
            Idle,                  //  待機
            Discover,              //  発見
            Move,                  //  移動
            AttackReady,           //  攻撃準備
            Attack,                //  攻撃1
            Attack2,               //  攻撃2
            Attack3,               //  攻撃3
            LongAttack,            //  遠距離攻撃
            Dead,                  //  死亡
        }

        // アイドル状態をセットしておく
        EnemyState currentState = EnemyState.Idle;

        void Start()
        {
            baseLayerIndex = animator.GetLayerIndex("Base Layer");
            rigidbody = GetComponent<Rigidbody>();
            timetoattackreset = timetoattack;                  //攻撃時間を指定した時間にリセットする変数に値を代入
            weaponcollider.enabled = false;                    //敵の武器の当たり判定をオフ
            swordeffect.SetActive(false);
            enemyhpbar.value = StageScene.Instance.enemyHp;    // Sliderの初期状態を設定 
        }

        // Update is called once per frame
        void Update()
        {
            // 状態ごとの分岐処理
            switch (currentState)
            {
                case EnemyState.Idle:
                    UpdateForIdle();
                    break;
                case EnemyState.Move:
                    UpdateForMove();
                    break;
                case EnemyState.Discover:
                    UpdateForDiscover();
                    break;
                case EnemyState.Attack:
                case EnemyState.Attack2:
                case EnemyState.Attack3:
                    UpdateForAttack();
                    break;
                case EnemyState.LongAttack:
                    UpdateForLongAttack();
                    break;
                case EnemyState.AttackReady:
                    UpdateForAttackReady();
                    break;
                case EnemyState.Dead:
                    break;
                default:
                    break;
            }
        }

        void UpdateForLongAttack()
        {
            //ターゲット方向のベクトルを求める
            Vector3 vec = target.position - transform.position;

            // ターゲットの方向を向く
            // Quaternion(回転値)を取得
            Quaternion quaternion = Quaternion.LookRotation(vec);
            transform.rotation = quaternion;

            timetoatk += Time.deltaTime;
            if (timetoatk > timefire)
            {
                timetoatk = 0;
                animator.SetTrigger(isLongAttack);
            }
        }

        //遠距離攻撃に切り替え
        public void LongAttack()
        {
            if (currentState == EnemyState.Discover)
            {
                animator.SetTrigger(isLongAttack);
                currentState = EnemyState.LongAttack;
            }
        }

        //索敵範囲外にでたときの処理
        public void SetIdleState()
        {
            speed = 0;
            currentState = EnemyState.Idle;
            animator.SetTrigger(isLost);
            animator.SetFloat(speedId, 0.0f);
        }

        //索敵範囲内に入った時の処理
        public void SetDiscoverState()
        {
            currentState = EnemyState.Discover;
            speed = chasespeed;
        }

        //Moveステートに変更
        public void SetMoveState()
        {
            currentState = EnemyState.Move;
            speed = chasespeed;
        }

        //攻撃範囲内に入った時にステートを攻撃準備に切り替え
        public void SetAttackReadyState()
        {
            if (!isDead)
            {
                Debug.Log("攻撃準備");
                currentState = EnemyState.AttackReady;
                speed = attackreadyspeed;                          //攻撃範囲に入ったら様子見で移動速度を小さくする
                animator.SetTrigger(isAttackReady);
                timetoattackreset = timetoattack;                       ////攻撃までの時間のカウントをリセット
            }
        }

        //ランダムに攻撃する。攻撃時は移動速度を0に設定
        public void Attacks()
        {
            Debug.Log("攻撃");
            float tmp = Random.Range(1.0f, 4.0f);              //1～攻撃種類数の乱数を取得
            int random = (int)tmp;                             //float型の乱数をint型にキャスト
                                                               
            if (!isDead)
            {
                switch (random)
                {
                    case 1:
                        currentState = EnemyState.Attack;
                        animator.SetTrigger(isAttack);
                        break;
                    case 2:
                        currentState = EnemyState.Attack2;
                        animator.SetTrigger(isAttack2);
                        break;
                    case 3:
                        currentState = EnemyState.Attack3;
                        animator.SetTrigger(isAttack3);
                        break;
                    default:
                        break;
                }
            }
            rigidbody.velocity = Vector3.zero;                       //立ち止まる
            timetoattackreset = timetoattack;                          //攻撃までの時間のカウントをリセット
        }

        public void AfterAttack()
        {
            SetMoveState();
        }

        //当たり判定をONにする関数
        public void SetColliderOn(Collider collider)
        {
            SE.Instance.PlaySound(swing);
            swordeffect.SetActive(true);
            collider.enabled = true;
        }

        //当たり判定をOFFにする関数
        public void SetColliderOff(Collider collider)
        {
            swordeffect.SetActive(false);
            collider.enabled = false;
        }

        void UpdateForDiscover()
        {
            UpdateForMove();
        }

        //待機状態の処理
        void UpdateForIdle()
        {
            Vector3 vec = Vector3.zero;
            rigidbody.velocity = transform.forward * speed;
        }

        float spd;
        float cnt = 0;
        //プレイやーに向かって動く処理
        void UpdateForMove()
        {
            if (!isDead)
            {
                //ターゲット方向のベクトルを求める
                Vector3 vec = target.position - transform.position;
                
                // ターゲットの方向を向く
                // Quaternion(回転値)を取得
                Quaternion quaternion = Quaternion.LookRotation(vec);
                // 算出した回転値をこのゲームオブジェクトのrotationに代入
                transform.rotation = quaternion;
                rigidbody.velocity = transform.forward * speed;// 正面方向に移動

                if (currentState == EnemyState.Discover && spd <= 2.00f || currentState == EnemyState.Move && spd <= 2.00f)
                {
                    spd += sp * Time.deltaTime;
                }

                if (speed <= chasespeed)
                {
                    speed += (chasespeed * Time.deltaTime) / 2;
                }
                animator.SetFloat(speedId, spd);

                if (!isAttacks && isLongAttacks)
                {
                    cnt += Time.deltaTime;
                    if (cnt > 3)
                    {
                        cnt = 0;
                        LongAttack();
                    }
                }
            }
        }

        //攻撃範囲にとどまっている時間をカウントして一定時間を超えたらAttackStateに切り替える
        void UpdateForAttackReady()
        {
            //移動速度を0にしプレイヤーの向きに回転する。
            //rigidbody.velocity = Vector3.zero;
            Rotate();
            timetoattackreset -= Time.deltaTime;

            if (0 > timetoattackreset && isAttacks)                               //攻撃までの時間が0になればステート遷移。カウントをリセットする。
            {
                //ランダムな攻撃
                Attacks();
            }
        }

        //プレイヤーの方向に回転する関数
        void Rotate()
        {
            Quaternion rot = this.transform.rotation;
            //ターゲット方向のベクトルを求める
            Vector3 vec = target.position - transform.position;
            // ターゲットの方向を向く
            // Quaternion(回転値)を取得
            Quaternion quaternion = Quaternion.LookRotation(vec);
            // 算出した回転値をこのゲームオブジェクトのrotationに代入
            rot = quaternion;
            rot.x = 0;
            this.transform.rotation = rot;
        }

        //Attackステート時の処理
        void UpdateForAttack()
        {
            var stateInfo = animator.GetCurrentAnimatorStateInfo(baseLayerIndex);
            if (stateInfo.fullPathHash == LocomotionHash)
            {
                SetAttackReadyState();
            }
            Rotate();
        }

        void SetDeadState()
        {
            currentState = EnemyState.Dead;
            speed = 0;
            rigidbody.mass = 100;
            animator.SetTrigger(DeadId);
        }

        //敵の遠距離攻撃のプレハブの生成
        public void EnemyShotAttack()
        {
            SE.Instance.PlaySound(fire);
            GameObject shell = Instantiate(shellprefab, enemy_l_hand.transform.position, Quaternion.identity);
            Rigidbody shellRb = shell.GetComponent<Rigidbody>();
            // 弾速を設定
            shellRb.AddForce(transform.forward * 1500);
            Destroy(shell, 1.0f);
        }

        //敵のHPバーの処理
        public void EnemyDamage(float n)
        {
            if (currentState != EnemyState.Dead)
            {
                StageScene.Instance.enemyHp -= n;
                n /= 100.0f;
                hp.fillAmount += n;
                SE.Instance.PlaySound(chargedamaged);
                GameObject Hit = Instantiate(HitEffect, this.transform.position, Quaternion.identity);
                Destroy(Hit, 1.5f);

                //HP0のとき撃破エフェクトの生成と敵オブジェクトの削除
                if (StageScene.Instance.enemyHp <= 0)
                {
                    isDead = true;
                    SetDeadState();
                    GameObject defeat = Instantiate(defeateffect, this.transform.position, Quaternion.identity);
                    Destroy(this.gameObject, deleteenemytime);
                }
            }
        }
    }
}







