using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// 移動するキャラクターを制御します。
public class MoveBehaviourScript : MonoBehaviour
{
    [SerializeField]
    [Tooltip("大・中・小の動くスピードを指定")]
    private float BIGspeed, MEDIUMspeed, SMALLspeed;

    [SerializeField]
    [Tooltip("大・中・小のジャンプ力を指定")]
    private float BIGup, MEDIUMup, SMALLup;

    [SerializeField]
    private Vector3 com;

    [SerializeField]
    [Tooltip("大中小のそれぞれのオブジェクトを指定します。")]
    private GameObject[] bodies = null;

    //Playerのアニメーターの取得
    [SerializeField]
    Animator animator;

    public static MoveBehaviourScript Instance { get; private set; }

    // AnimatorのパラメーターID
    static readonly int isAttackId = Animator.StringToHash("isAttack");

    // 現在のAnimator(大中小のいずれか)
    Animator currentAnimator = null;

    // 今のスピードとジャンプ力
    private float speed = 5.0f;
    private float upForce = 100f;

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
        // ゲームオーバー
        Dead,
        // ギミック系
        G1, G2, G3,
    }
    // 現在のプレイヤーの状態
    PlayerState currentState = PlayerState.Walk;

    [SerializeField]
    private bool isGrounded = false;
    /* [SerializeField]
     private LayerMask groudLayer;
    */


    // コンポーネントを事前に参照しておく変数
    new Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        
        Time.timeScale = 1;
        isGrounded = true;
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.centerOfMass = com;

        // 初めは大きい状態
        bodies[0].SetActive(false);
        bodies[1].SetActive(false);
        bodies[2].SetActive(true);
        currentAnimator = bodies[1].GetComponent<Animator>();
    }

    private void Awake()
    {
        Instance = this;
    }


    // Update is called once per frame
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
            case PlayerState.Dead:
                UpdateForDeadState();
                break;
        }
    }

    private void FixedUpdate()
    {

    }

    void UpdateForWalkState()
    {
        if (PlayerHPbar.Instance.big)
        {
            speed = BIGspeed;
        }
        else if (PlayerHPbar.Instance.med)
        {
            speed = MEDIUMspeed;
        }
        else if (PlayerHPbar.Instance.min)
        {
            speed = SMALLspeed;
        }
    }

    void UpdateForJumpReadyState()
    {

    }

    void UpdateForJumpingState()
    {
        if (isGrounded == false)
        {
            speed = 5;
        }
    }

    void UpdateForAvoidState()
    {

    }

    void UpdateForAttackState()
    {
            Debug.Log("攻撃");
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

    void UpdateForDeadState()
    {
        Debug.Log("死んだ");
        Time.timeScale = 0;
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
    public void SetBigState()
    {
        currentState = PlayerState.Big;
    }

    // Mediumステートに遷移させます。
    public void SetMediumState()
    {
        currentState = PlayerState.Medium;
    }

    // Smallステートに遷移させます。
    public void SetSmallState()
    {
        currentState = PlayerState.Small;
    }

    public void SetDeadState()
    {
        currentState = PlayerState.Dead;
    }

    // 指定した方向へ移動します。
    public void Move(Vector3 motion)
    {
        // ゲームオーバーの時以外
        //TODO: 今はDead時のみ除外
        if (currentState != PlayerState.Dead)
        {
            // プレイヤーの前後左右の移動
            var velocity = motion;
            // 地上歩行キャラクターを標準とするのでy座標移動は無視
            velocity.y = 0;
            if (velocity.sqrMagnitude >= 0.0001f)
            {
                transform.LookAt(transform.position + velocity.normalized, Vector3.up);
                velocity *= speed;

                Debug.Log(velocity);
            }
            velocity.y = rigidbody.velocity.y;
            rigidbody.velocity = velocity;
        }
    }

    // プレイヤーの方角を回転させます。
    public void Rotate(float deltaAngle)
    {
        transform.Rotate(0, deltaAngle, 0);
    }

    // 攻撃します
    public void Fire()
    {
        if (isGrounded == true)
        {
            SetAttackState();
        }
    }

    // ジャンプします。
    public void Jump()
    {
        if (isGrounded == true)
        {
            // spaceが押されたらジャンプ
            rigidbody.AddForce(transform.up * upForce / 20f, ForceMode.Impulse);
            isGrounded = false;

            SetJumpingState();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (currentState == PlayerState.Walk || currentState == PlayerState.Jumping)
        {
            if (collision.gameObject.tag == "enemy")
            {
                PlayerHPbar.Instance.Damage();
            }
        }
        
        // 接地判定
        if (collision.gameObject.tag == "ground")
        {
            isGrounded = true;
            SetWalkState();
        }
    }

    public IEnumerator DelayCoroutine()
    {
        // 1秒間待つ
        yield return new WaitForSeconds(1); 
    }
    public void Attack()
    {
        animator.SetTrigger(isAttackId);
    }

    // 大きい時
    public void Big()
    {
        speed = BIGspeed;
        upForce = BIGup;

            bodies[0].SetActive(false);
            bodies[1].SetActive(false);
            bodies[2].SetActive(true);
    }

    // 中型の時
    public void Medium()
    {
        speed = MEDIUMspeed;
        upForce = MEDIUMup;

            bodies[0].SetActive(false);
            bodies[1].SetActive(true);
            bodies[2].SetActive(false);
    }

    // 小さい時
    public void Small()
    {
        speed = SMALLspeed;
        upForce = SMALLup;

            bodies[0].SetActive(true);
            bodies[1].SetActive(false);
            bodies[2].SetActive(false);
    }
}

