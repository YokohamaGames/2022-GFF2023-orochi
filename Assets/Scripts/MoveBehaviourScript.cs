using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// �ړ�����L�����N�^�[�𐧌䂵�܂��B
public class MoveBehaviourScript : MonoBehaviour
{
    [SerializeField]
    [Tooltip("�����X�s�[�h���w��")]
    private float speed = 10.0f;

    [SerializeField]
    [Tooltip("�W�����v�͂��w��")]
    private float upForce = 20f;

    [SerializeField]
    private Vector3 com;

    [SerializeField]
    [Tooltip("�咆���̂��ꂼ��̃I�u�W�F�N�g���w�肵�܂��B")]
    private GameObject[] bodies = null;

    // ���݂�Animator(�咆���̂����ꂩ)
    Animator currentAnimator = null;

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
            Debug.Log("�U��");
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
    void SetBigState()
    {
        currentState = PlayerState.Big;
    }

    // Medium�X�e�[�g�ɑJ�ڂ����܂��B
    void SetMediumState()
    {
        currentState = PlayerState.Medium;
    }

    // Small�X�e�[�g�ɑJ�ڂ����܂��B
    void SetSmallState()
    {
        currentState = PlayerState.Small;
    }

    // �w�肵�������ֈړ����܂��B
    public void Move(Vector3 motion)
    {
        // �U�����Ă��鎞��
        //TODO: ����Attack���̂ݏ��O
        if (currentState != PlayerState.Attack)
        {
            // �v���C���[�̑O�㍶�E�̈ړ�
            var velocity = motion;
            // �n����s�L�����N�^�[��W���Ƃ���̂�y���W�ړ��͖���
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
            SetAttackState();
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
        // 1�b�ԑ҂�
        yield return new WaitForSeconds(1);

        // State��Walk�ɖ߂�
        SetWalkState();
    }
}

