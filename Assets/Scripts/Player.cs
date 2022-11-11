using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Player : MonoBehaviour
{   //�R���|�[�l���g�����O�Ɏ擾

    
    [Header("Cinemachine")]
    [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
    public GameObject CinemachineCameraTarget;

    [Tooltip("How far in degrees can you move the camera up")]
    public float TopClamp = 70.0f;

    [Tooltip("How far in degrees can you move the camera down")]
    public float BottomClamp = -30.0f;

    [Tooltip("Additional degress to override the camera. Useful for fine tuning camera position when locked")]
    public float CameraAngleOverride = 0.0f;

    [Tooltip("For locking the camera position on all axis")]
    public bool LockCameraPosition = false;

    [SerializeField]
    public GameObject avatar = null;
    

    MoveBehaviourScript moveBehaviour;

    
    //Player�̃A�j���[�^�[�̎擾
    Animator animator;

    // Animator�̃p�����[�^�[ID
    //static readonly int isAttackId = Animator.StringToHash("isAttack");
    //static readonly int isJumpId = Animator.StringToHash("isJump");


    // ���[�U�[����̓���
    Vector2 moveInput = Vector2.zero;
    Vector2 lookInput = Vector2.zero;

    /*
    // cinemachine
    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;
    */
    // player
    private float speed;
    private float rotationVelocity;
    private float targetRotation = 0.0f;
    private float verticalVelocity;

    private GameObject mainCamera;
    private CharacterController controller;


    [Tooltip("�L�������ړ������Ɍ������̑���")]
    [Range(0.0f, 0.3f)]
    public float RotationSmoothTime = 0.12f;

    
    void Start()
    {
        moveBehaviour = GetComponent<MoveBehaviourScript>();
        animator = GetComponentInChildren<Animator>();
    }


    void Update()
    {
        var motion = Camera.main.transform.forward * moveInput.y;
        motion += Camera.main.transform.right * moveInput.x;
        moveBehaviour.Move(motion);
    }
    private void FixedUpdate()
    {

    }

    // ���[�U�[�����Move�A�N�V�����ɑ΂��ČĂяo����܂��B
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

       Vector3 inputDirection = new Vector3(moveInput.x, 0.0f, moveInput.y).normalized;

        if (moveInput != Vector2.zero)
        {
            targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                              mainCamera.transform.eulerAngles.y;

            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationVelocity,
                RotationSmoothTime);

            // �J�����ʒu����ɓ��͕����ɉ�]
            avatar.transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }

        Vector3 targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;


        controller.Move(targetDirection.normalized * (speed * Time.deltaTime) +
                         new Vector3(0.0f, verticalVelocity, 0.0f) * Time.deltaTime);
    }

    // ���[�U�[�����Look�A�N�V�����ɑ΂��ČĂяo����܂��B
    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();


    }
   

    // Fire�{�^������������Ăяo����܂�
    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            animator.SetTrigger("isAttack");
            moveBehaviour.Fire();
        }
    }

    // Jump�{�^������������Ăяo����܂�
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            animator.SetTrigger("isJump");
            moveBehaviour.Jump();
        }
    }
    public void OnControlPauseUI(InputAction.CallbackContext context)
    {
        StageScene.Instance.ControlPauseUI();
        //this.tag = "Untagged";
    }

    // Injury�{�^������������Ăяo����܂�

    public void OnAvoid(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            moveBehaviour.Avoid();
        }
    }

    public void OnChangeBig(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && moveBehaviour.isChange)
        {
            moveBehaviour.BodyUp();
        }
    }
    public void OnChangeSmall(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && moveBehaviour.isChange)
        {
            moveBehaviour.BodyDown();
        }
    }

}


