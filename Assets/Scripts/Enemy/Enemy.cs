using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.UI;

//敵のAIとステータス設定
public class Enemy : MonoBehaviour
{
    //敵のステータスに関する数値設定

    //敵の追跡時歩行スピードを設定
    [SerializeField] private float ChaseSpeed;
    //攻撃準備時の歩行スピードを設定
    [SerializeField] private float AttackReadySpeed =1;
    //追跡目標の設定
    [SerializeField]
    private Transform target = null;
    //アニメーターの設定
    [SerializeField] private Animator animator = null;
    //敵の回転速度を設定します
    [SerializeField]
    private float rotMax;
    //子オブジェクトを取得
    [SerializeField]
    private SearchArea searchArea;
    //子オブジェクトを取得
    [SerializeField]
    private AttackArea attackArea;
    //敵の持つ武器の当たり判定を取得
    [SerializeField]
    private Collider Weapon_Collider;
    //ステート遷移を遅らせる時間
    [SerializeField]
    private float Transition_Time;                         
    //火球のプレハブの取得
    [SerializeField]
    private GameObject shellPrefab;
    //敵撃破エフェクト
    [SerializeField]
    private GameObject defeateffect;
    //ダメージエフェクト
    [SerializeField]
    private GameObject damageeffect;
    [SerializeField]
    private Transform Enemy_L_Hand;                        //敵の左手の座標を取得します
    //敵のHPを設定
    [SerializeField]
    int EnemyHp;
    //敵のHpBarを参照
    [SerializeField]
    public Slider EnemyHpBar;                              
    //剣のEffectを取得
    [SerializeField]
    private GameObject SwordEffect;
    //攻撃準備から攻撃までの時間の設定
    [SerializeField] private float TimetoAttack = 2;
    //敵撃破時の敵の消滅までの時間の設定
    [SerializeField] private float DeleteEnemyTime;
    //攻撃までの待機時間を設定した値にリセットする変数
    float timetoattack;

    [SerializeField]
    UI ui;
    public bool SearchArea = false;

    public bool AttackArea = false;

    public bool LongAttackArea = false;
    //現在の敵の歩行スピード
    float speed = 0;                                       
    // コンポーネントを事前に参照しておく変数
    new Rigidbody rigidbody;

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
        Idle,                                              //  待機
        Discover,                                          //  発見
        Move,                                              //  移動
        AttackReady,                                       //  攻撃準備
        Attack,                                            ////攻撃////
        Attack2,                                           //    |
        Attack3,                                           //    |
        LongAttack,                                        //  攻撃////
        Dead,                                              //  死亡
    }
    EnemyState currentState = EnemyState.Idle;
    float sp = 1.0f;
    void Start()
    {
        baseLayerIndex = animator.GetLayerIndex("Base Layer");
        rigidbody = GetComponent<Rigidbody>();             
        timetoattack = TimetoAttack;                       //攻撃時間を指定した時間にリセットする変数に値を代入
        Weapon_Collider.enabled = false;                   //敵の武器の当たり判定をオフ
        SwordEffect.SetActive(false);
        EnemyHpBar.value = EnemyHp;                        // Sliderの初期状態を設定 
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
                    UpdateForDead();
                    break;
                default:
                    break;
            }
        //animator.SetFloat("Speed", 0.01f);
        
        Debug.Log(currentState);

    }

    IEnumerator Wait()
    {
        yield return null;
    }
    void UpdateForDead()
    {
        StartCoroutine(Wait());
    }
    void UpdateForLongAttack()
    {

    }

    void SetLongAttackState()
    {
        currentState = EnemyState.LongAttack;

    }


    //遠距離攻撃に切り替え
    public void LongAttack()
    {
        if (currentState == EnemyState.Move)
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
        speed = ChaseSpeed;
    }
   
    //Moveステートに変更
    public void SetMoveState()
    {
        currentState = EnemyState.Move;
        //animator.
        speed = ChaseSpeed;
    }

    //攻撃範囲内に入った時にステートを攻撃準備に切り替え
    public void SetAttackReadyState()
    {
        currentState = EnemyState.AttackReady;
        speed = AttackReadySpeed;                          //攻撃範囲に入ったら様子見で移動速度を小さくする
        animator.SetTrigger(isAttackReady);
        timetoattack = TimetoAttack;                       ////攻撃までの時間のカウントをリセット
    }
    
    public void SetAttackReady()
    {
        currentState = EnemyState.AttackReady;
        speed = AttackReadySpeed;                          //攻撃範囲に入ったら様子見で移動速度を小さくする
        timetoattack = TimetoAttack;                       //攻撃までの時間のカウントをリセット
        
    }
   
    //ランダムに攻撃する。攻撃時は移動速度を0に設定
    public void Attacks()
    {
        float tmp = Random.Range(1.0f, 4.0f);              //1～攻撃種類数の乱数を取得
        int random = (int)tmp;                             //float型の乱数をint型にキャスト
        //SetColliderOn(Weapon_Collider);
        //Debug.Log(random);
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
        rigidbody.velocity = Vector3.zero;                       //立ち止まる
        timetoattack = TimetoAttack;                          //攻撃までの時間のカウントをリセット

    }
    
    //当たり判定をONにする関数
    public void SetColliderOn(Collider collider)
    {
        SwordEffect.SetActive(true);
        collider.enabled = true;
        Debug.Log("呼ばれた");
    }
   
    //当たり判定をOFFにする関数
    public void SetColliderOff(Collider collider)
    {
        SwordEffect.SetActive(false);
        collider.enabled = false;
    }

    float timefire = 1.5f;
    float timetoatk = 0;
    void UpdateForDiscover()
    {
        //UpdateForMove();
        Rotate();
        timetoatk += Time.deltaTime;
        if (timetoatk > timefire)
        {

            timetoatk = 0;
            animator.SetTrigger(isLongAttack);

        }
    }    
    
    //待機状態の処理
    void UpdateForIdle()
    {
        Vector3 vec = Vector3.zero;
        rigidbody.velocity = transform.forward * speed;
    }

    float spd;
    //プレイやーに向かって動く処理
    void UpdateForMove()
    {
        //ターゲット方向のベクトルを求める
        Vector3 vec = target.position - transform.position;
        vec.y = 0;
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

        if (speed <= ChaseSpeed)
        {
            speed += (ChaseSpeed * Time.deltaTime) /2;
        }
        animator.SetFloat(speedId, spd);


    }

    //攻撃範囲にとどまっている時間をカウントして一定時間を超えたらAttackStateに切り替える
    void UpdateForAttackReady()
    {
        //移動速度を0にしプレイヤーの向きに回転する。
        //rigidbody.velocity = Vector3.zero;
        Rotate();
        timetoattack -= Time.deltaTime;
        
        if(0 > timetoattack)                               //攻撃までの時間が0になればステート遷移。カウントをリセットする。
        {
            //ランダムな攻撃
            Attacks();  
        }
    }

    //プレイヤーの方向に回転する関数
    void Rotate()
    {
        //ターゲット方向のベクトルを求める
        Vector3 vec = target.position - transform.position;

        // ターゲットの方向を向く
        // Quaternion(回転値)を取得
        Quaternion quaternion = Quaternion.LookRotation(vec);
        // 算出した回転値をこのゲームオブジェクトのrotationに代入
        this.transform.rotation = quaternion;
    }
    
    //Attackステート時の処理
    void UpdateForAttack()
    {
        var stateInfo = animator.GetCurrentAnimatorStateInfo(baseLayerIndex);
        if (stateInfo.fullPathHash == LocomotionHash)
        {
            SetAttackReady();
        }
        Rotate();
    }
    
    void SetDeadState()
    {
        currentState = EnemyState.Dead;
        speed = 0;
        animator.SetTrigger(DeadId);

    }
    //ステート遷移を遅らせる関数　未使用
    IEnumerator DelayState()
    {
        yield return new WaitForSeconds(Transition_Time);
    }

    //敵の遠距離攻撃のプレハブの生成
    public void EnemyShotAttack()
    {
        GameObject shell = Instantiate(shellPrefab, Enemy_L_Hand.transform.position, Quaternion.identity);
        Rigidbody shellRb = shell.GetComponent<Rigidbody>();
        // 弾速を設定
        shellRb.AddForce(transform.forward * 1500);
        Destroy(shell, 1.0f);
    }

    //敵のHPバーの処理
    public void EnemyDamage(int n)
    {
        EnemyHp -= n;
        EnemyHpBar.value = EnemyHp;

        //HP0のとき撃破エフェクトの生成と敵オブジェクトの削除
        if (EnemyHp <= 0)
        {
            SetDeadState();
            GameObject defeat = Instantiate(defeateffect, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject,DeleteEnemyTime);
            Destroy(defeat, 8.0f);
            ui.StageClear();

        }
    }


    //当たり判定メソッド
    private void OnCollisionEnter(Collision collision)
    {
        //衝突したオブジェクトがBullet(大砲の弾)だったとき
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("敵と弾が衝突しました！！！");
            GameObject damege = Instantiate(damageeffect, this.transform.position, Quaternion.identity);
            Destroy(damege, 1.5f);
        }
    }
}






