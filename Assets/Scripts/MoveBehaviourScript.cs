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
    [Tooltip("回避の幅を指定")]
    private float avo;

    [SerializeField]
    private Vector3 com;

    [SerializeField]
    [Tooltip("大中小のそれぞれのオブジェクトを指定します。")]
    private GameObject[] bodies = null;

    //Playerのアニメーターの取得
    [SerializeField]
    Animator animator;

    public bool big, medium, small;

    // AnimatorのパラメーターID
    static readonly int isAttackId = Animator.StringToHash("isAttack");

    // 現在のAnimator(大中小のいずれか)
    Animator currentAnimator = null;

    // 今のスピードとジャンプ力
    private float speed = 5.0f;
    private float upForce = 100f;

    private bool ButtonEnabled = true;

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
        // 無敵
        Invincible,
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

        // 初めは普通の状態
        bodies[0].SetActive(false);
        bodies[1].SetActive(true);
        bodies[2].SetActive(false);
        currentAnimator = bodies[1].GetComponent<Animator>();

        big = false;
        medium = true;
        small = false;
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
            case PlayerState.Invincible:
                UpdateForInvincible();
                break;
        }

        // HPが5以上の時
        if (StageScene.Instance.playerhp == 6 || StageScene.Instance.playerhp == 5)
        {
            Big();
        }
        // HPが3～4の時
        if (StageScene.Instance.playerhp == 4 || StageScene.Instance.playerhp == 3)
        {
            Medium();
        }
        // HPが1～2の時
        if (StageScene.Instance.playerhp == 2 || StageScene.Instance.playerhp == 1)
        {
            Small();
        }
        // HPが0の時
        if (StageScene.Instance.playerhp == 0)
        {
            SetDeadState();
        }

    }

    private void FixedUpdate()
    {

    }

    void UpdateForWalkState()
    {
        if (big)
        {
            speed = BIGspeed;
        }
        else if (medium)
        {
            speed = MEDIUMspeed;
        }
        else if (small)
        {
            speed = SMALLspeed;
        }
    }

    void UpdateForJumpReadyState()
    {

    }

    void UpdateForJumpingState()
    {
            speed = 5;
        Debug.Log("ジャンプ");
    }

    void UpdateForAvoidState()
    {

    }

    void UpdateForAttackState()
    {
        StartCoroutine(DelayCoroutine());
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

    void UpdateForInvincible()
    {
        StartCoroutine(DelayCoroutine());
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

    public void SetInvincible()
    {
        currentState = PlayerState.Invincible;
    }

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
                transform.LookAt(transform.position + velocity.normalized, Vector3.up);
                velocity *= speed;

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
            if (ButtonEnabled == false)
            {
                return;
            }
            else
            {
                SetAttackState();

                //Fireボタンで呼び出されるタックル攻撃。addForceを使用せずtransformで代用。向いている方向に座標を+する。
                var player_transform = transform.position;
                player_transform += transform.forward;
                transform.position = player_transform;

                ButtonEnabled = false;

                StartCoroutine(ButtonCoroutine());
            }
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
               　StageScene.Instance.Damage();
                SetInvincible();
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

        SetWalkState();
    }

    public IEnumerator ButtonCoroutine()
    {
        // 2秒待つ
        yield return new WaitForSeconds(2);

        ButtonEnabled = true;
    }
    
    public void Attack()
    {
        animator.SetTrigger(isAttackId);
    }

    public void Avoid()
    {
        if (currentState == PlayerState.Walk)
        {
            rigidbody.AddForce(-transform.forward * avo, ForceMode.VelocityChange);

            SetInvincible();
        }
    }
    // 大きい時
    public void Big()
    {
        Debug.Log("大型");

        upForce = BIGup;

            bodies[0].SetActive(false);
            bodies[1].SetActive(false);
            bodies[2].SetActive(true);

        big = true;
        medium = false;
    }

    // 中型の時
    public void Medium()
    {
        Debug.Log("中型");

        upForce = MEDIUMup;

            bodies[0].SetActive(false);
            bodies[1].SetActive(true);
            bodies[2].SetActive(false);

        big = false;
        medium = true;
        small = false;
    }

    // 小さい時
    public void Small()
    {
        Debug.Log("小型");

        upForce = SMALLup;

            bodies[0].SetActive(true);
            bodies[1].SetActive(false);
            bodies[2].SetActive(false);

        medium = false;
        small = true;
    }
}

