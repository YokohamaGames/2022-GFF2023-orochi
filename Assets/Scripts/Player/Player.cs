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

    MoveBehaviourScript MoveBehaviour;

    // ���[�U�[����̓���
    Vector2 moveInput = Vector2.zero;
    Vector2 lookInput = Vector2.zero;


    [Tooltip("�L�������ړ������Ɍ������̑���")]
    [Range(0.0f, 0.3f)]
    public float RotationSmoothTime = 0.12f;


    void Start()
    {
        MoveBehaviour = GetComponent<MoveBehaviourScript>();
    }


    void Update()
    {
        // �J��������Ɉړ��ł���悤�ɂ���
        var motion = Camera.main.transform.forward * moveInput.y;
        motion += Camera.main.transform.right * moveInput.x;
        MoveBehaviour.Move(motion);
    }

    #region ���[�U�[�̃A�N�V�����ɑ΂��ČĂяo�����

    // Move�A�N�V�����ɑ΂��ČĂяo����܂��B
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    // Look�A�N�V�����ɑ΂��ČĂяo����܂��B
    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }


    // Fire�{�^������������Ăяo����܂�
    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            MoveBehaviour.Fire();
        }
    }

    // Fire2�{�^������������Ăяo����܂�
    public void OnFire2(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            MoveBehaviour.ShotAttack();
        }
    }

    // Jump�{�^������������Ăяo����܂�
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            MoveBehaviour.Jump();
        }
    }

    // Pause�{�^������������Ăяo����܂�
    public void OnControlPauseUI(InputAction.CallbackContext context)
    {
        StageScene.Instance.ControlPauseUI();
    }

    // Avoid�{�^������������Ăяo����܂�
    public void OnAvoid(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            MoveBehaviour.Avoid();
        }
    }

    // ChangeB�{�^������������Ăяo����܂�
    public void OnChangeBig(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && MoveBehaviour.isChange)
        {
            MoveBehaviour.BodyUp();
        }
    }

    // ChangeS�{�^������������Ăяo����܂�
    public void OnChangeSmall(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && MoveBehaviour.isChange)
        {
            MoveBehaviour.BodyDown();
        }
    }

}

#endregion
