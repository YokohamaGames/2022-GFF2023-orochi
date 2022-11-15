using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

// �ړ�����L�����N�^�[�𐧌䂵�܂��B
public class MoveBehaviourScript : MonoBehaviour
{
    [SerializeField]
    [Tooltip("��E���E���̓����X�s�[�h���w��")]
    private float LARGEspeed, MEDIUMspeed, SMALLspeed;

    [SerializeField]
    [Tooltip("��E���E���̃W�����v�͂��w��")]
    private float LARGEup, MEDIUMup, SMALLup;

    [SerializeField]
    [Tooltip("����̕����w��")]
    private float avo;

    private Vector3 com;

    //Player�̃A�j���[�^�[�̎擾
    [SerializeField]
    Animator animator;

    [SerializeField]
    [Tooltip("�J�����̐؂�ւ�")]
    private CinemachineVirtualCamera[] VirtualCamera = null;

    enum BodySize
    {
        Small,
        Medium,
        Large,
    }

    // ���݂̃L�����N�^�[�T�C�Y
    BodySize currentBodySize = BodySize.Medium;

    [SerializeField]
    [Tooltip("�咆���̂��ꂼ��̃I�u�W�F�N�g���w�肵�܂��B")]
    private GameObject[] bodies = null;

    [SerializeField]
    [Tooltip("�ϐg�̃N�[���^�C��")]
    float ChangeCoolTime = 10;

    float CoolTime;

    public bool isChange = false;

    // Animator�̃p�����[�^�[ID
    static readonly int isAttackId = Animator.StringToHash("isAttack");
    static readonly int isAvoidId = Animator.StringToHash("isAvoid");

    // ���݂�Animator(�咆���̂����ꂩ)
    Animator currentAnimator = null;

    // ���̃X�s�[�h�ƃW�����v��
    private float speed = 5.0f;
    private float upForce = 100f;

    private bool ButtonEnabled = true;

    // �v���C���[�̃J����
    public Camera playerCamera = null;

    // Avatar�I�u�W�F�N�g�ւ̎Q��
    public GameObject avatar = null;

    //�񕜃G�t�F�N�g�̎w��
    [SerializeField]
    public GameObject HealObject;

    //�T�C�Y�ύX�G�t�F�N�g�̎w��
    [SerializeField]
    public GameObject ChangeEffect;

    Quaternion EffectAngle = Quaternion.Euler(-90f, 0f, 0f);

    // �v���C���[�̏�Ԃ�\���܂�
    enum PlayerState
    {
        // ����
        Walk,
        // �W�����v�̗\������
        JumpReady,
        // �W�����v��
        Jumping,
        // ���
        Avoid,
        // �U��
        Attack,
        // ��
        Big,
        // ��
        Medium,
        // ��
        Small,
        // �Q�[���I�[�o�[
        Dead,
        // ���G
        Invincible,
        // �M�~�b�N�n
        G1, G2, G3,
    }
    // ���݂̃v���C���[�̏��
    PlayerState currentState = PlayerState.Walk;

    [SerializeField]
    private bool isGrounded = false;
    /* [SerializeField]
     private LayerMask groudLayer;
    */


    // �R���|�[�l���g�����O�ɎQ�Ƃ��Ă����ϐ�
    new Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        
        Time.timeScale = 1;
        isGrounded = true;
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.centerOfMass = com;

        // ���߂͕��ʂ̏��
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

        // �N�[���^�C���̌o�ߎ���
        if (CoolTime >= 0)
        {
            CoolTime -= Time.deltaTime;
        }
        if (CoolTime < 0)
        {
            isChange = true;
        }

        // HP��0�̎�
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
        Debug.Log("�W�����v");
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
        Debug.Log("����");
        Time.timeScale = 0;
    }

    void UpdateForInvincible()
    {
        StartCoroutine(DelayCoroutine());
    }

    // Walk�X�e�[�g�ɑJ�ڂ����܂��B
    void SetWalkState()
    {
        currentState = PlayerState.Walk;
    }

    // JumpReady�X�e�[�g�ɑJ�ڂ����܂��B
    void SetJumpReadyState()
    {
        currentState = PlayerState.JumpReady;
    }

    // Jumping�X�e�[�g�ɑJ�ڂ����܂��B
    void SetJumpingState()
    {
        currentState = PlayerState.Jumping;
    }

    // Avoid�X�e�[�g�ɑJ�ڂ����܂��B
    void SetAvoidState()
    {
        currentState = PlayerState.Avoid;
    }

    // Attack�X�e�[�g�ɑJ�ڂ����܂��B
    void SetAttackState()
    {
        currentState = PlayerState.Attack;
    }

    // Big�X�e�[�g�ɑJ�ڂ����܂��B
    public void SetBigState()
    {
        currentState = PlayerState.Big;
    }

    // Medium�X�e�[�g�ɑJ�ڂ����܂��B
    public void SetMediumState()
    {
        currentState = PlayerState.Medium;
    }

    // Small�X�e�[�g�ɑJ�ڂ����܂��B
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

    // �w�肵�������ֈړ����܂��B
    public void Move(Vector3 motion)
    {
        // Walk��Jumping�̎�����
        if (currentState == PlayerState.Walk || currentState == PlayerState.Jumping)
        {
            // �v���C���[�̑O�㍶�E�̈ړ�
            var velocity = motion;
            // �n����s�L�����N�^�[��W���Ƃ���̂�y���W�ړ��͖���
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


    // �U�����܂�
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

    // �W�����v���܂��B
    public void Jump()
    {
        if (isGrounded == true)
        {
            // space�������ꂽ��W�����v
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
        // �ڒn����
        if (collision.gameObject.tag == "ground")
        {
            isGrounded = true;
            SetWalkState();
        }
    }
       

    

    public IEnumerator DelayCoroutine()
    {
        // 1�b�ԑ҂�
        yield return new WaitForSeconds(1);

        SetWalkState();
    }

    public IEnumerator ButtonCoroutine()
    {
        // 2�b�҂�
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
    // �傫����
    public void Large()
    {
        //�ϐg�G�t�F�N�g
        Instantiate(ChangeEffect, this.transform.position, EffectAngle); //�p�[�e�B�N���p�Q�[���I�u�W�F�N�g����

        Debug.Log("��^");

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

    // ���^�̎�
    public void Medium()
    {
        Debug.Log("���^");

        //�ϐg�G�t�F�N�g
        Instantiate(ChangeEffect, this.transform.position, EffectAngle); //�p�[�e�B�N���p�Q�[���I�u�W�F�N�g����

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

    // ��������
    public void Small()
    {
        Debug.Log("���^");

        //�ϐg�G�t�F�N�g
        Instantiate(ChangeEffect, this.transform.position, EffectAngle); //�p�[�e�B�N���p�Q�[���I�u�W�F�N�g����

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

    //�񕜒��̃G�t�F�N�g����
    public void Heal()
    {
        Debug.Log("��");
        Instantiate(HealObject, this.transform.position, Quaternion.identity); //�p�[�e�B�N���p�Q�[���I�u�W�F�N�g����
        //playerhp += 1;
    }

    // �ϐg�̃N�[���^�C���̃��Z�b�g
    public void ResetCoolTime()
    {
        CoolTime = ChangeCoolTime;
        isChange = false;
    }
}

