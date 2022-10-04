using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Enemy : MonoBehaviour
{


    //敵の追跡時歩行スピードを設定します
    [SerializeField]
    private float ChaseSpeed;
    [SerializeField]
    private float AttackSpeed = 1;
    //現在の敵の歩行スピード
    float speed = 1;
   
    //追跡目標を設定します
    [SerializeField]
    private Transform target;

    [SerializeField]
    private Animation anim;


    //敵の回転速度を設定します
    [SerializeField]
    private float rotMax;

    // ユーザーからの入力
    Vector2 moveInput = Vector2.zero;
    Vector2 lookInput = Vector2.zero;

    public bool SearchArea = false;

    public bool AttackArea = false;

    // コンポーネントを事前に参照しておく変数
    Animator animator;

    // コンポーネントを事前に参照しておく変数
    new Rigidbody rigidbody;

    // AnimatorのパラメーターID
    static readonly int isDiscover = Animator.StringToHash("isDiscover");
    
    enum EnemyState
    {
        //待機状態
        Stay,

        //発見状態
        Discover,

        //移動状態
        Move,

        //攻撃状態
        Attack,

        //回避状態
        Escape,


    }

    EnemyState currentState = EnemyState.Stay;
    void Start()
    {
        animator = GetComponent<Animator>();
        
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
                default:
                    break;
            }



        }
        //索敵範囲と攻撃範囲の中にいるとき攻撃モード
        /*if (SearchArea && AttackArea)
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

        Debug.Log(currentState);*/


        // ゲームパッドが接続されていないとnullになる。
        if (Gamepad.current == null) return;

        if (Gamepad.current.buttonNorth.wasPressedThisFrame)
        {
            Debug.Log("Button Northが押された！");
        }
        if (Gamepad.current.buttonSouth.wasReleasedThisFrame)
        {
            Debug.Log("Button Southが離された！");
        }

    }

    public void SetStayState()
    {
        currentState = EnemyState.Stay;
        
    }

    public void SetDiscoverState()
    {
        currentState = EnemyState.Discover;
        animator.SetBool(isDiscover, true);
    }
    public void SetMoveState()
    {
        animator.SetBool(isDiscover, false);
        currentState = EnemyState.Move;
        speed = ChaseSpeed;
    }
    public void SetAttackState()
    {
        currentState = EnemyState.Attack;
        speed = AttackSpeed;
    }
    public void SetEscapeState()
    {

    }
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

        /*ターゲット方向のベクトルを求める
        Vector3 vec = target.position - transform.position;

        // ターゲットの方向を向く
        // Quaternion(回転値)を取得
        Quaternion quaternion = Quaternion.LookRotation(vec);
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(vec.x, 0, vec.z)), rotMax);
        transform.Translate(Vector3.forward * speed * 0.01f); // 正面方向に移動

        // 算出した回転値をこのゲームオブジェクトのrotationに代入
        this.transform.rotation = quaternion;*/


    }

    void UpdateForAttack()
    {

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




