using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// 移動するキャラクターを制御します。
public class MoveBehaviourScript : MonoBehaviour
{
    [SerializeField]
    [Tooltip("動くスピードを指定")]
    private float speed = 10.0f;

    [SerializeField]
    [Tooltip("ジャンプ力を指定")]
    private float upForce = 20f;

    [SerializeField]
    private Vector3 com;

    [SerializeField]
    [Tooltip("大中小のそれぞれのオブジェクトを指定します。")]
    private GameObject[] bodies = null;

    // 現在のAnimator(大中小のいずれか)
    Animator currentAnimator = null;

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
        isGrounded = true;
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.centerOfMass = com;

        bodies[0].SetActive(false);
        bodies[1].SetActive(true);
        bodies[2].SetActive(false);
        currentAnimator = bodies[1].GetComponent<Animator>();
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
        }
    }

    private void FixedUpdate()
    {

    }

    void UpdateForWalkState()
    {
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
            Debug.Log("攻撃");
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

    // 指定した方向へ移動します。
    public void Move(Vector3 motion)
    {
        // 攻撃している時以
        //TODO: 今はAttack時のみ除外
        if (currentState != PlayerState.Attack)
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
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (currentState == PlayerState.Walk)
        {
            if (collision.gameObject.tag == "enemy")
            {
                PlayerHPbar.Instance.Damage();
            }
        }
        
        if (collision.gameObject.tag == "ground")
        {
            isGrounded = true;
        }
    }

    public IEnumerator DelayCoroutine()
    {
        speed = 0;
        // 1秒間待つ
        yield return new WaitForSeconds(1);

        // StateをWalkに戻す
        SetWalkState();
    }
}

