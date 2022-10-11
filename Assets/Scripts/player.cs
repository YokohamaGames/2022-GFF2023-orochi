using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private Vector3 velocity;              // 移動方向

    [SerializeField]
    private Vector3 com;

    [SerializeField]
    [Tooltip("動くスピードを指定")]
    private float speed = 10.0f;

    [SerializeField]
    [Tooltip("ジャンプ力を指定")]
    private float upForce = 20f;

    [SerializeField]
    private bool isGrounded = false;
    /* [SerializeField]
     private LayerMask groudLayer;
    */


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

    [SerializeField]
    private Animator animator = null;

    // ユーザーからの入力
    Vector2 moveInput = Vector2.zero;
    Vector2 lookInput = Vector2.zero;

    void Start()
    {
        isGrounded = true;
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.centerOfMass = com;
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

        // 二人分操作するための一時的な移動
        if (Keyboard.current.aKey.isPressed)
        {
            moveInput.x = -1;
        }
        if(Keyboard.current.aKey.wasReleasedThisFrame)
        {
            moveInput.x = 0;
        }
        if (Keyboard.current.dKey.isPressed)
        {
            moveInput.x = 1;
        }
        if (Keyboard.current.dKey.wasReleasedThisFrame)
        {
            moveInput.x = 0;
        }
        if (Keyboard.current.wKey.isPressed)
        {
            moveInput.y = 1;
        }
        if (Keyboard.current.wKey.wasReleasedThisFrame)
        {
            moveInput.y = 0;
        }
        if (Keyboard.current.sKey.isPressed)
        {
            moveInput.y = -1;
        }
        if (Keyboard.current.sKey.wasReleasedThisFrame)
        {
            moveInput.y = 0;
        }
    }

    private void FixedUpdate()
    {

    }

    void UpdateForWalkState()
    {
        // プレイヤーの前後左右の移動
        var velocity = Vector3.zero;
        if (moveInput.y > 0)
        {
            velocity = transform.forward * moveInput.y * speed;
        }
        else if (moveInput.y < 0)
        {
            velocity = transform.forward * moveInput.y * speed;
        }
        velocity += transform.right * moveInput.x * speed;
        velocity.y = rigidbody.velocity.y;
        rigidbody.velocity = velocity;

        // プレイヤーの方角を回転
        transform.Rotate(0, lookInput.x, 0);

        //isGrounded = true;

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

    // ユーザーからのMoveアクションに対して呼び出されます。
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        

        /*
        // アクションが始まった
        if (context.started)
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

    //Fireボタンを押したら呼び出されます
    public void OnFire(InputAction.CallbackContext context)
    {
        //if (isGrounded == true)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                Debug.Log("Fire");                //ジャンプさせます。
                rigidbody.AddForce(transform.up * upForce / 20f, ForceMode.Impulse);
                //isGrounded = false;
            }
        }
    }
    public void OnControlPauseUI(InputAction.CallbackContext context)
    {
        StageScene.Instance.ControlPauseUI();
    }

    void OnCollisionEnter (Collision collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            PlayerHPbar.Instance.Damage();
        }
    }
}
