using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

// 移動するキャラクターを制御します。
public class MoveBehaviourScript : MonoBehaviour
{
    [SerializeField]
    [Tooltip("大・中・小の動くスピードを指定")]
    private float LARGEspeed, MEDIUMspeed, SMALLspeed;

    [SerializeField]
    [Tooltip("大・中・小のジャンプ力を指定")]
    private float LARGEup, MEDIUMup, SMALLup;

    [SerializeField]
    [Tooltip("回避の幅を指定")]
    private float avo;

    private Vector3 com;

    //Playerのアニメーターの取得
    [SerializeField]
    Animator animator;

    [SerializeField]
    [Tooltip("カメラの切り替え")]
    private CinemachineVirtualCamera[] VirtualCamera = null;

    enum BodySize
    {
        Small,
        Medium,
        Large,
    }

    // 現在のキャラクターサイズ
    BodySize currentBodySize = BodySize.Medium;

    [SerializeField]
    [Tooltip("大中小のそれぞれのオブジェクトを指定します。")]
    private GameObject[] bodies = null;

    [SerializeField]
    [Tooltip("変身のクールタイム")]
    float ChangeCoolTime = 10;

    float CoolTime;

    public bool isChange = false;

    // AnimatorのパラメーターID
    static readonly int isAttackId = Animator.StringToHash("isAttack");
    static readonly int isAvoidId = Animator.StringToHash("isAvoid");

    // 現在のAnimator(大中小のいずれか)
    Animator currentAnimator = null;

    // 今のスピードとジャンプ力
    private float speed = 5.0f;
    private float upForce = 100f;

    private bool ButtonEnabled = true;

    // プレイヤーのカメラ
    public Camera playerCamera = null;

    // Avatarオブジェクトへの参照
    public GameObject avatar = null;

    //回復エフェクトの指定
    [SerializeField]
    public GameObject HealObject;

    //サイズ変更エフェクトの指定
    [SerializeField]
    public GameObject ChangeEffect;

    Quaternion EffectAngle = Quaternion.Euler(-90f, 0f, 0f);

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
        currentBodySize = BodySize.Medium;

        VirtualCamera[1].Priority = 100;
        CoolTime = ChangeCoolTime;
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

        // クールタイムの経過時間
        if (CoolTime >= 0)
        {
            CoolTime -= Time.deltaTime;
        }
        if (CoolTime < 0)
        {
            isChange = true;
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
        switch (currentBodySize)
        {
            case BodySize.Small:
                speed = SMALLspeed;
                break;
            case BodySize.Medium:
                speed = MEDIUMspeed;
                break;
            case BodySize.Large:
                speed = LARGEspeed;
                break;
            default:
                break;
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
                avatar.transform.LookAt(transform.position + velocity.normalized, Vector3.up);
                velocity *= speed;

            }
            velocity.y = rigidbody.velocity.y;
            rigidbody.velocity = velocity;
        }
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

                //rigidbody.AddForce(transform.forward * 10, ForceMode.VelocityChange);

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

    void OnTriggerEnter(Collider collision)
    {
        if (currentState == PlayerState.Walk || currentState == PlayerState.Jumping || currentState == PlayerState.Attack)
        {
            if (collision.CompareTag("Enemy_Weapon"))
            {
                StageScene.Instance.Damage();
                SetInvincible();
            }
        }

        if(currentState == PlayerState.Attack)
        { 
            if (collision.CompareTag("enemy"))
            {
                collision.GetComponent<Enemy>().EnemyDamage();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
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
        currentAnimator.SetTrigger(isAttackId);
    }

    public void Avoid()
    {
        if (currentState == PlayerState.Walk)
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.AddForce(avatar.transform.forward * avo, ForceMode.Impulse);

            currentAnimator.SetTrigger(isAvoidId);
            SetInvincible();
        }
    }
    // 大きい時
    public void Large()
    {
        //変身エフェクト
        Instantiate(ChangeEffect, this.transform.position, EffectAngle); //パーティクル用ゲームオブジェクト生成

        Debug.Log("大型");

        upForce = LARGEup;

            bodies[0].SetActive(false);
            bodies[1].SetActive(false);
            bodies[2].SetActive(true);

        VirtualCamera[0].Priority = 10;
        VirtualCamera[1].Priority = 10;
        VirtualCamera[2].Priority = 100;
        currentBodySize = BodySize.Large;
        currentAnimator = bodies[2].GetComponent <Animator>();

        ResetCoolTime();
    }

    // 中型の時
    public void Medium()
    {
        Debug.Log("中型");

        //変身エフェクト
        Instantiate(ChangeEffect, this.transform.position, EffectAngle); //パーティクル用ゲームオブジェクト生成

        upForce = MEDIUMup;

            bodies[0].SetActive(false);
            bodies[1].SetActive(true);
            bodies[2].SetActive(false);

        VirtualCamera[0].Priority = 10;
        VirtualCamera[1].Priority = 100;
        VirtualCamera[2].Priority = 10;
        currentBodySize = BodySize.Medium;
        currentAnimator = bodies[1].GetComponent<Animator>();

        ResetCoolTime();
    }

    // 小さい時
    public void Small()
    {
        Debug.Log("小型");

        //変身エフェクト
        Instantiate(ChangeEffect, this.transform.position, EffectAngle); //パーティクル用ゲームオブジェクト生成

        upForce = SMALLup;

            bodies[0].SetActive(true);
            bodies[1].SetActive(false);
            bodies[2].SetActive(false);

        VirtualCamera[0].Priority = 100;
        VirtualCamera[1].Priority = 10;
        VirtualCamera[2].Priority = 10;
        currentBodySize = BodySize.Small;
        currentAnimator = bodies[0].GetComponent<Animator>();

        ResetCoolTime();
    }

    public void BodyUp()
    {
        switch (currentBodySize)
            {
            case BodySize.Small:
                Medium();
                break;
            case BodySize.Medium:
                Large();
                break;
            case BodySize.Large:
            default:
                break;
        }
    }

    public void BodyDown()
    {
        switch (currentBodySize)
        {
            case BodySize.Large:
                Medium();
                break;
            case BodySize.Medium:
                Small();
                break;
            case BodySize.Small:
            default:
                break;
        }
    }

    //回復中のエフェクト処理
    public void Heal()
    {
        Debug.Log("回復");
        Instantiate(HealObject, this.transform.position, Quaternion.identity); //パーティクル用ゲームオブジェクト生成
        //playerhp += 1;
    }

    // 変身のクールタイムのリセット
    public void ResetCoolTime()
    {
        CoolTime = ChangeCoolTime;
        isChange = false;
    }
}

