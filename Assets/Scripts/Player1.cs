using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    [SerializeField] private Vector3 velocity;              // 移動方向

    [SerializeField]
    [Tooltip("動くスピードを指定")]
    private float speed = 10.0f;

   /* [SerializeField]
    [Tooltip("ジャンプ力を指定")]
    private float upForce = 200f;
   */
    [SerializeField]
    private bool isGrounded = false;
   /* [SerializeField]
    private LayerMask groudLayer;
   */
    private Transform _transform;

    float h = 0f;
    float v = 0f;

    // プレイヤーの状態を表します
    enum PlayerState
    {
        // 歩き
        Walk,
        // ジャンプの予備動作
        JumpReady,
        // ジャンプ中
        Jumping,
        // 回避中
        Avoid,
        // 攻撃
        Attack,
        // 大
        Big,
        // 中
        Medium,
        // 小
        Small,
        // ギミック系
        G1, G2, G3,
    }
    // 現在のプレイヤーの状態
    PlayerState currentState = PlayerState.Walk;

    // コンポーネントを事前に参照しておく変数
    new Rigidbody rigidbody;
    Animator animator;

    void Start()
    {
       // isGrounded = true;
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        _transform = transform;
    }

    // Walkステートに遷移させます。
    void SetWalkState()
    {
        currentState = PlayerState.Walk;
    }

    // JumpReadyステートに遷移させます。
    void SetJumpReadyState()
    {
        currentState = PlayerState.JumpReady;
    }

    // Jumpingステートに遷移させます。
    void SetJumpingState()
    {
        currentState = PlayerState.Jumping;
    }

    // Avoidステートに遷移させます。
    void SetAvoidState()
    {
        currentState = PlayerState.Avoid;
    }

    // Attackステートに遷移させます。
    void SetAttackState()
    {
        currentState = PlayerState.Attack;
    }

    // Bigステートに遷移させます。
    void SetBigState()
    {
        currentState = PlayerState.Big;
    }

    // Mediumステートに遷移させます。
    void SetMediumState()
    {
        currentState = PlayerState.Medium;
    }

    // Smallステートに遷移させます。
    void SetSmallState()
    {
        currentState = PlayerState.Small;
    }

    void Update()
    {
        switch (currentState)
        {
            case PlayerState.Walk:
                UpdateForWalkState();
                break;
            case PlayerState.JumpReady:
                UpdateForJumpReadyState();
                break;
            case PlayerState.Jumping:
                UpdateForJumpingState();
                break;
            case PlayerState.Avoid:
                UpdateForAvoidState();
                break;
            case PlayerState.Attack:
                UpdateForAttackState();
                break;
            case PlayerState.Big:
                UpdateForBigState();
                break;
            case PlayerState.Medium:
                UpdateForMediumState();
                break;
            case PlayerState.Small:
                UpdateForSmallState();
                break;

        }

        h = Input.GetAxis("Horizontal") * speed;
        v = Input.GetAxis("Vertical") * speed;
        Vector3 direction = new Vector3(h, 0, v);


        // if(isGrounded == true)
        // {
        //     if(Input.GetKeyDown("space"))
        //     {
        //         isGrounded = false;
        //         rigidbody.AddForce(new Vector3(0, upForce, 0));
        //     }
        // }
    }

    private void FixedUpdate()
    {
        rigidbody.velocity = new Vector3(h, 0, v);
    }

    void UpdateForWalkState()
    {

    }

    void UpdateForJumpReadyState()
    {

    }

    void UpdateForJumpingState()
    {

    }

    void UpdateForAvoidState()
    {

    }

    void UpdateForAttackState()
    {

    }

    void UpdateForBigState()
    {

    }

    void UpdateForMediumState()
    {

    }

    void UpdateForSmallState()
    {

    }
}
