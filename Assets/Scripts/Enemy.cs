using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
//敵のAiとステータス設定
public class Enemy : MonoBehaviour
{
    //敵のステータスに関する数値設定

    //敵の追跡時歩行スピードを設定します
    [SerializeField]
    private float ChaseSpeed;
    [SerializeField]
    private float AttackSpeed = 1;
    //現在の敵の歩行スピード
    float speed = 1;
    float TimeCount = 0;                        //時間をカウントする変数。攻撃範囲にとどまっている時間のカウント
    [SerializeField]
    private float TimetoAttack = 2;
    //追跡目標を設定します
    [SerializeField]
    private Transform target;

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

    // ユーザーからの入力
    Vector2 moveInput = Vector2.zero;
    Vector2 lookInput = Vector2.zero;

    public bool SearchArea = false;

    public bool AttackArea = false;


    // コンポーネントを事前に参照しておく変数
    new Rigidbody rigidbody;
    //Animator animator;
    
    // AnimatorのパラメーターID
    static readonly int isDiscover = Animator.StringToHash("isDiscover");
    static readonly int isLost = Animator.StringToHash("isLost");
    static readonly int isAttackReady = Animator.StringToHash("isAttackReady");
    static readonly int isAttack = Animator.StringToHash("isAttack");




    enum EnemyState
    {
        //待機状態
        Stay,

        //発見状態
        Discover,

        //移動状態
        Move,

        //攻撃準備
        AttackReady,
        //攻撃状態
        Attack,

        //回避状態
        Escape,


    }

    EnemyState currentState = EnemyState.Stay;
    void Start()
    {
        //animator = GetComponent<Animator>();
        
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
                case EnemyState.Attack:
                    UpdateForAttack();
                    break;
                case EnemyState.Discover:
                    UpDateForDiscover();
                    break;
                case EnemyState.AttackReady:
                    UpdateForAttackReady();
                    break;
                default:
                    break;
            }

        }
        //索敵範囲と攻撃範囲の中にいるとき攻撃モード
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

        Debug.Log(currentState);
        //Debug.Log(TimeCount);

        

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
        //animator.SetBool(isDiscover, false);
        currentState = EnemyState.Move;
        speed = ChaseSpeed;
    }
    public void SetAttackReadyState()
    {
        currentState = EnemyState.AttackReady;
        speed = AttackSpeed;                   //攻撃範囲に入ったら様子見で移動速度を小さくする
        animator.SetTrigger(isAttackReady);
        
    }
    public void SetAttackState()
    {
        currentState = EnemyState.Attack;
        speed = 0;
        TimeCount = 0;
        animator.SetTrigger(isAttack);

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


        // プレイヤーの前後左右の移動
        var velocity = Vector3.zero;
        velocity = transform.forward * moveInput.y * speed;
        velocity += transform.right * moveInput.x * speed;
        velocity.y = rigidbody.velocity.y;
        rigidbody.velocity = velocity;

        // プレイヤーの方角を回転
        transform.Rotate(0, lookInput.x, 0);

        //ターゲット方向のベクトルを求める
        Vector3 vec = target.position - transform.position;

        // ターゲットの方向を向く
        // Quaternion(回転値)を取得
        Quaternion quaternion = Quaternion.LookRotation(vec);
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(vec.x, 0, vec.z)), rotMax);
        transform.Translate(Vector3.forward * speed * 0.01f); // 正面方向に移動

        // 算出した回転値をこのゲームオブジェクトのrotationに代入
        this.transform.rotation = quaternion;


    }
    //攻撃範囲にとどまっている時間をカウントして一定時間を超えたらAttackStateに切り替え、カウントを0にリセット
    void UpdateForAttackReady()
    {
       // if (TimeCount > 2)
           // TimeCount = 0;
        TimeCount += Time.deltaTime;
        if(TimeCount > TimetoAttack)
        {
            //TimeCount = 0;
            SetAttackState();
        }
    }
    void UpdateForAttack()
    {

        //ターゲット方向のベクトルを求める
        Vector3 vec = target.position - transform.position;

        // ターゲットの方向を向く
        // Quaternion(回転値)を取得
        Quaternion quaternion = Quaternion.LookRotation(vec);
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(vec.x, 0, vec.z)), rotMax);
        // 算出した回転値をこのゲームオブジェクトのrotationに代入
        transform.rotation = quaternion;
        transform.Translate(Vector3.forward * speed * 0.01f); // 正面方向に移動

        
        
    }
    void UpDateForDiscover()
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

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        // アクションが始まった
        /*if (context.started)
        {
            Debug.Log($"started : {moveInput}");
        }
        // アクションが継続中
        else if (context.performed)
        {
            Debug.Log($"performed : {moveInput}");
        }
        // アクションが終了
        else if (context.canceled)
        {
            Debug.Log($"canceled : {moveInput}");
        }*/
    }

    // ユーザーからのMoveアクションに対して呼び出されます。
    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();

        // アクションが始まった
        /*if (context.started)
        {
            Debug.Log($"started : {lookInput}");
        }
        // アクションが継続中
        else if (context.performed)
        {
            Debug.Log($"performed : {lookInput}");
        }
        // アクションが終了
        else if (context.canceled)
        {
            Debug.Log($"canceled : {lookInput}");
        }*/
    }


}




