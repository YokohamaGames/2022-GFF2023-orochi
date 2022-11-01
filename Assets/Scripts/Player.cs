using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Player : MonoBehaviour
{   //コンポーネントを事前に取得

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


    MoveBehaviourScript moveBehaviour;
    Rigidbody player_rigidbody;

    
    //Playerのアニメーターの取得
    Animator animator;

    // AnimatorのパラメーターID
    static readonly int isAttackId = Animator.StringToHash("isAttack");
    static readonly int isJumpId = Animator.StringToHash("isJump");


    // ユーザーからの入力
    Vector2 moveInput = Vector2.zero;
    Vector2 lookInput = Vector2.zero;

    // cinemachine
    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;

    // player
    private float _speed;
    private float _rotationVelocity;
    private float _targetRotation = 0.0f;
    private float _verticalVelocity;

    private GameObject _mainCamera;
    private CharacterController _controller;

    [Tooltip("キャラが移動方向に向く時の速さ")]
    [Range(0.0f, 0.3f)]
    public float RotationSmoothTime = 0.12f;

    void Start()
    {
        moveBehaviour = GetComponent<MoveBehaviourScript>();
        animator = GetComponent<Animator>();
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

    // ユーザーからのMoveアクションに対して呼び出されます。
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

       Vector3 inputDirection = new Vector3(moveInput.x, 0.0f, moveInput.y).normalized;

        if (moveInput != Vector2.zero)
        {
            _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                              _mainCamera.transform.eulerAngles.y;

            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
                RotationSmoothTime);

            // カメラ位置を基準に入力方向に回転
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }

        Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;


        _controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) +
                         new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
    }

    // ユーザーからのMoveアクションに対して呼び出されます。
    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();

    }

    // Fireボタンを押したら呼び出されます
    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            animator.SetTrigger(isAttackId);
            moveBehaviour.Fire();
        }
    }

    // Jumpボタンを押したら呼び出されます
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            animator.SetTrigger(isJumpId);
            moveBehaviour.Jump();
        }
    }
    public void OnControlPauseUI(InputAction.CallbackContext context)
    {
        StageScene.Instance.ControlPauseUI();
    }

    // Injuryボタンを押したら呼び出されます
    public void OnInjury(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            StageScene.Instance.Damage();
        }
    }

    public void OnAvoid(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            moveBehaviour.Avoid();
        }
    }

    public void OnChangeBig(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            moveBehaviour.ChangeR();
        }
    }

    public void OnChangeSmall(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            moveBehaviour.ChangeL();
        }
    }
}


