using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
//敵のAiとステータス設定

 //マジックナンバーをに名前を付けるクラス
public static class Define
{
    public const float Number_of_Attack_Type = 4;          //攻撃種類数
}

public class Enemy : MonoBehaviour
{
    //敵のステータスに関する数値設定

    
    [SerializeField]
    private float ChaseSpeed;                              //敵の追跡時歩行スピードを設定

    [SerializeField]
    private float AttackReadySpeed = 1;                    //攻撃準備時の歩行スピードを設定  

    [SerializeField]
    private float TimetoAttack = 2;                        //攻撃準備から攻撃までの時間の設定

    [SerializeField]
    private Transform target;                              //追跡目標の設定

    [SerializeField]
    private Animator animator;
    
    //敵の回転速度を設定します
    [SerializeField]
    private float rotMax;
    //子オブジェクトを取得
    [SerializeField]
    private SearchArea searchArea;
    //子オブジェクトを取得
    [SerializeField]
    private AttackArea attackArea;
    [SerializeField]
    private Collider Weapon_Collider;                      //敵の持つ武器の当たり判定を取得
    [SerializeField]
    private float Transition_Time;                         //ステート遷移を遅らせる時間
    
    float timetoattack;                                    //攻撃時間を設定した時間にリセットする変数

    [SerializeField]
    int EnemyHp = 2;

    public bool SearchArea = false;

    public bool AttackArea = false;
    float speed = 1;                                       //現在の敵の歩行スピード

    int[] AttacksCount = { 0, 0, 0 };                      //攻撃の種類分配列を用意。技を使ったらカウントを+1する。

    
    // コンポーネントを事前に参照しておく変数
    new Rigidbody rigidbody;
    //Animator animator;
    
    // AnimatorのパラメーターID
    static readonly int isDiscover = Animator.StringToHash("isDiscover");
    static readonly int isLost = Animator.StringToHash("isLost");
    static readonly int isAttackReady = Animator.StringToHash("isAttackReady");
    static readonly int isAttack = Animator.StringToHash("isAttack");
    static readonly int isAttack2 = Animator.StringToHash("isAttack2");
    static readonly int isAttack3 = Animator.StringToHash("isAttack3");





     enum EnemyState
    {
        Stay,                                              //  待機
        Discover,                                          //  発見
        Move,                                              //  移動
        AttackReady,                                       //  攻撃準備
        Attack,                                            ////攻撃////
        Attack2,                                           //    |
        Attack3,                                           //  攻撃////
        Escape,                                            //  回避
    }

    EnemyState currentState = EnemyState.Stay;
    void Start()
    {
        //animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();             
        timetoattack = TimetoAttack;                       //攻撃時間を指定した時間にリセットする変数に値を代入
        Weapon_Collider.enabled = false;                   //敵の武器の当たり判定をオフ
    }

    // Update is called once per frame
    void Update()
    {
        {
            // 状態ごとの分岐処理
            switch (currentState)
            {
                case EnemyState.Stay:
                    UpdateForStay();
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
                
                case EnemyState.AttackReady:
                    UpdateForAttackReady();
                    break;
                default:
                    break;
            }

        }
        /*索敵範囲と攻撃範囲の中にいるとき攻撃モード
        if (SearchArea && AttackArea)
        {
            Debug.Log("攻撃範囲内");
        }
        //索敵範囲内のみの時追跡モード
        else if (SearchArea)
        {
            Debug.Log("索敵範囲内");
        }
        //索敵範囲外の時Stay
        else if (!SearchArea)
        {
            Debug.Log("索敵範囲外");
        }
        */

        //Debug.Log(TimeCount);

        Debug.Log(currentState);
        //Debug.Log(timetoattack);

        if(StageScene.Instance.Enemyhp == 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void SetStayState()
    {
        currentState = EnemyState.Stay;
        animator.SetTrigger(isLost);
        
    }

    public void SetDiscoverState()
    {
        currentState = EnemyState.Discover;
        animator.SetTrigger(isDiscover);
    }
    public void SetMoveState()
    {
        currentState = EnemyState.Move;
        speed = ChaseSpeed;
    }
    public void SetAttackReadyState()
    {
        currentState = EnemyState.AttackReady;
        speed = 0;                                         //攻撃範囲に入ったら様子見で移動速度を小さくする
        animator.SetTrigger(isAttackReady);
        timetoattack = TimetoAttack;                       ////攻撃までの時間のカウントをリセット

    }
    //Animatorのセットトリガーはしない
    public void SetAttackReady()
    {
        currentState = EnemyState.AttackReady;
        speed = AttackReadySpeed;                                         //攻撃範囲に入ったら様子見で移動速度を小さくする
        timetoattack = TimetoAttack;                       ////攻撃までの時間のカウントをリセット
        
    }
    //ランダムに攻撃する
    public void Attacks()
    {
        float tmp = Random.Range(1.0f, 4.0f);              //1～攻撃種類数の乱数を取得
        int random = (int)tmp;                             //float型の乱数をint型にキャスト
        //SetColliderOn(Weapon_Collider);
        //Debug.Log(random);
        switch (random)
        {
            case 1:currentState = EnemyState.Attack;
                   animator.SetTrigger(isAttack);
                   break;
            case 2:currentState = EnemyState.Attack2;
                   animator.SetTrigger(isAttack2);
                   break;
            case 3:currentState = EnemyState.Attack3;
                   animator.SetTrigger(isAttack3);
                   break;
            default:
                break;
        }
        rigidbody.velocity = Vector3.zero;                       //立ち止まる
        timetoattack = TimetoAttack;                       ////攻撃までの時間のカウントをリセット
        //animator.SetTrigger(isAttack);
        //AttacksCount[0] += 1;

    }
    //当たり判定をONにする関数
    public void SetColliderOn(Collider collider)
    {
        collider.enabled = true;
        Debug.Log("呼ばれた");
    }
    //当たり判定をOFFにする関数
    public void SetColliderOff(Collider collider)
    {
        collider.enabled = false;
    }

    public void SetEscapeState()
    {

    }
    //待機状態のアップデート処理
    void UpdateForStay()
    {
        //Debug.Log("待機中");
    }

    void UpdateForMove()
    {


       
        //ターゲット方向のベクトルを求める
        Vector3 vec = target.position - transform.position;

        // ターゲットの方向を向く
        // Quaternion(回転値)を取得
        Quaternion quaternion = Quaternion.LookRotation(vec);
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(vec.x, 0, vec.z)), rotMax);
        rigidbody.velocity = new Vector3(0, 0, speed);  // 正面方向に移動

        // 算出した回転値をこのゲームオブジェクトのrotationに代入
        this.transform.rotation = quaternion;


    }
    //攻撃範囲にとどまっている時間をカウントして一定時間を超えたらAttackStateに切り替え、カウントを0にリセット
    void UpdateForAttackReady()
    {
         rigidbody.velocity = Vector3.zero;
         Rotate();

        //Debug.Log(timetoattack);
        timetoattack -= Time.deltaTime;
        
        if(0 > timetoattack)                               //攻撃までの時間が0になればステート遷移。カウントをリセットする。
        {
            
            Attacks();
            
        }
    }
    void Rotate()
    {

        //ターゲット方向のベクトルを求める
        Vector3 vec = target.position - transform.position;

        // ターゲットの方向を向く
        // Quaternion(回転値)を取得
        Quaternion quaternion = Quaternion.LookRotation(vec);
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(vec.x, 0, vec.z)), rotMax);
        rigidbody.velocity = new Vector3(0, 0, speed);  // 正面方向に移動

        // 算出した回転値をこのゲームオブジェクトのrotationに代入
        this.transform.rotation = quaternion;


    }
    void UpdateForDiscover()
    {
        speed = ChaseSpeed;
        if(currentState == EnemyState.Discover)
        {
            //ターゲット方向のベクトルを求める
            Vector3 vec = target.position - transform.position;
            // Quaternion(回転値)を取得 回転する度数を取得
            Quaternion quaternion = Quaternion.LookRotation(vec);
            //取得した度数分オブジェクトを回転させる
            transform.rotation = quaternion;
            transform.Translate(Vector3.forward * speed * 0.01f); // 正面方向に移動

        }
    }

    void UpdateForAttack()
    {
        speed = 0;
        //ターゲット方向のベクトルを求める
        Vector3 vec = target.position - transform.position;
        // Quaternion(回転値)を取得 回転する度数を取得
        Quaternion quaternion = Quaternion.LookRotation(vec);
        //取得した度数分オブジェクトを回転させる
        transform.rotation = quaternion;
    }
    //配列の中身の最小値を探す関数:未完成
    /*void CheckAttackCount(int[] atk_array)
    {
        for (int i = 0; i < atk_array.Length; i++)
        {
            if(atk_array[i] )
        }
    }*/

    IEnumerator DelayState()
    {
        yield return new WaitForSeconds(Transition_Time);
    }

    
}






