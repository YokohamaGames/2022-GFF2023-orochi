using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{   //�R���|�[�l���g�����O�Ɏ擾


    [Header("Cinemachine")]
    [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
    public GameObject CinemachineCameraTarget;
   // public GameObject CinemachineCameraTarget;

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


    //[SerializeField]
    //private GameObject mainCamera;



    [Tooltip("�L�������ړ������Ɍ������̑���")]
    [Range(0.0f, 0.3f)]
    public float RotationSmoothTime = 0.12f;


    void Start()
    {
        moveBehaviour = GetComponent<MoveBehaviourScript>();
        animator = GetComponentInChildren<Animator>();

        // mainCamera = Camera.main;
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


