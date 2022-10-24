using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// �ړ�����L�����N�^�[�𐧌䂵�܂��B
public class MoveBehaviourScript : MonoBehaviour
{
    [SerializeField]
    [Tooltip("��E���E���̓����X�s�[�h���w��")]
    private float BIGspeed, MEDIUMspeed, SMALLspeed;

    [SerializeField]
    [Tooltip("��E���E���̃W�����v�͂��w��")]
    private float BIGup, MEDIUMup, SMALLup;

    [SerializeField]
    [Tooltip("����̕����w��")]
    private float avo;

    [SerializeField]
    private Vector3 com;

    [SerializeField]
    [Tooltip("�咆���̂��ꂼ��̃I�u�W�F�N�g���w�肵�܂��B")]
    private GameObject[] bodies = null;

    //Player�̃A�j���[�^�[�̎擾
    [SerializeField]
    Animator animator;

    public bool big, medium, small;

    // Animator�̃p�����[�^�[ID
    static readonly int isAttackId = Animator.StringToHash("isAttack");

    // ���݂�Animator(�咆���̂����ꂩ)
    Animator currentAnimator = null;

    // ���̃X�s�[�h�ƃW�����v��
    private float speed = 5.0f;
    private float upForce = 100f;

    private bool ButtonEnabled = true;

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

        // HP��5�ȏ�̎�
        if (StageScene.Instance.playerhp == 6 || StageScene.Instance.playerhp == 5)
        {
            Big();
        }
        // HP��3�`4�̎�
        if (StageScene.Instance.playerhp == 4 || StageScene.Instance.playerhp == 3)
        {
            Medium();
        }
        // HP��1�`2�̎�
        if (StageScene.Instance.playerhp == 2 || StageScene.Instance.playerhp == 1)
        {
            Small();
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
                transform.LookAt(transform.position + velocity.normalized, Vector3.up);
                velocity *= speed;

            }
            velocity.y = rigidbody.velocity.y;
            rigidbody.velocity = velocity;
        }
    }

    // �v���C���[�̕��p����]�����܂��B
    public void Rotate(float deltaAngle)
    {
        transform.Rotate(0, deltaAngle, 0);
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

                //Fire�{�^���ŌĂяo�����^�b�N���U���BaddForce���g�p����transform�ő�p�B�����Ă�������ɍ��W��+����B
                var player_transform = transform.position;
                player_transform += transform.forward;
                transform.position = player_transform;

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

    void OnCollisionEnter(Collision collision)
    {
        if (currentState == PlayerState.Walk || currentState == PlayerState.Jumping)
        {
            if (collision.gameObject.tag == "enemy")
            {
               �@StageScene.Instance.Damage();
                SetInvincible();
            }
        }
        
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
    // �傫����
    public void Big()
    {
        Debug.Log("��^");

        upForce = BIGup;

            bodies[0].SetActive(false);
            bodies[1].SetActive(false);
            bodies[2].SetActive(true);

        big = true;
        medium = false;
    }

    // ���^�̎�
    public void Medium()
    {
        Debug.Log("���^");

        upForce = MEDIUMup;

            bodies[0].SetActive(false);
            bodies[1].SetActive(true);
            bodies[2].SetActive(false);

        big = false;
        medium = true;
        small = false;
    }

    // ��������
    public void Small()
    {
        Debug.Log("���^");

        upForce = SMALLup;

            bodies[0].SetActive(true);
            bodies[1].SetActive(false);
            bodies[2].SetActive(false);

        medium = false;
        small = true;
    }
}

