using UnityEngine;
using UnityEngine.InputSystem;

public class Player1 : MonoBehaviour
{
    [SerializeField] private Vector3 velocity;              // �ړ�����

    [SerializeField]
    private Vector3 com;

    [SerializeField]
    [Tooltip("�����X�s�[�h���w��")]
    private float speed = 10.0f;

    /* [SerializeField]
     [Tooltip("�W�����v�͂��w��")]
     private float upForce = 200f;
    */
    [SerializeField]
    private bool isGrounded = false;
    /* [SerializeField]
     private LayerMask groudLayer;
    */


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

    // �R���|�[�l���g�����O�ɎQ�Ƃ��Ă����ϐ�
    new Rigidbody rigidbody;

    [SerializeField]
    private Animator animator = null;

    // ���[�U�[����̓���
    Vector2 moveInput = Vector2.zero;
    Vector2 lookInput = Vector2.zero;

    void Start()
    {
        // isGrounded = true;
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.centerOfMass = com;
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
        // �v���C���[�̑O�㍶�E�̈ړ�
        var velocity = Vector3.zero;
        velocity = transform.forward * moveInput.y * speed;
        velocity += transform.right * moveInput.x * speed;
        velocity.y = rigidbody.velocity.y;
        rigidbody.velocity = velocity;

        // �v���C���[�̕��p����]
        transform.Rotate(0, lookInput.x, 0);



        // if(isGrounded == true)
        // {
        //     if(Input.GetKeyDown("space"))
        //     {
        //         isGrounded = false;
        //         rigidbody.AddForce(new Vector3(0, upForce, 0));
        //     }
        // }
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

    // ���[�U�[�����Move�A�N�V�����ɑ΂��ČĂяo����܂��B
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        // �A�N�V�������n�܂���
        /*if (context.started)
        {
            Debug.Log($"started : {moveInput}");
        }
        // �A�N�V�������p����
        else if (context.performed)
        {
            Debug.Log($"performed : {moveInput}");
        }
        // �A�N�V�������I��
        else if (context.canceled)
        {
            Debug.Log($"canceled : {moveInput}");
        }*/
    }

    // ���[�U�[�����Move�A�N�V�����ɑ΂��ČĂяo����܂��B
    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();

        // �A�N�V�������n�܂���
        /*if (context.started)
        {
            Debug.Log($"started : {lookInput}");
        }
        // �A�N�V�������p����
        else if (context.performed)
        {
            Debug.Log($"performed : {lookInput}");
        }
        // �A�N�V�������I��
        else if (context.canceled)
        {
            Debug.Log($"canceled : {lookInput}");
        }*/
    }
}
