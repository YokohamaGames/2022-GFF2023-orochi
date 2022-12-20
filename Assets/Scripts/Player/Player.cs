using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{   //コンポーネントを事前に取得


    [Header("Cinemachine")]
    [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
    public GameObject CinemachineCameraTarget;
   // public GameObject CinemachineCameraTarget;

    [Tooltip("Additional degress to override the camera. Useful for fine tuning camera position when locked")]
    public float CameraAngleOverride = 0.0f;

    [Tooltip("For locking the camera position on all axis")]
    public bool LockCameraPosition = false;

    MoveBehaviourScript MoveBehaviour;

    // ユーザーからの入力
    Vector2 moveInput = Vector2.zero;
    Vector2 lookInput = Vector2.zero;


    [Tooltip("キャラが移動方向に向く時の速さ")]
    [Range(0.0f, 0.3f)]
    public float RotationSmoothTime = 0.12f;


    void Start()
    {
        MoveBehaviour = GetComponent<MoveBehaviourScript>();
    }


    void Update()
    {
        // カメラを基準に移動できるようにする
        var motion = Camera.main.transform.forward * moveInput.y;
        motion += Camera.main.transform.right * moveInput.x;
        MoveBehaviour.Move(motion);
    }

    #region ユーザーのアクションに対して呼び出される

    // Moveアクションに対して呼び出されます。
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    // Lookアクションに対して呼び出されます。
    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }


    // Fireボタンを押したら呼び出されます
    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            MoveBehaviour.Fire();
        }
    }

    // Fire2ボタンを押したら呼び出されます
    public void OnFire2(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            MoveBehaviour.ShotAttack();
        }
    }

    // Jumpボタンを押したら呼び出されます
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            MoveBehaviour.Jump();
        }
    }

    // Pauseボタンを押したら呼び出されます
    public void OnControlPauseUI(InputAction.CallbackContext context)
    {
        StageScene.Instance.ControlPauseUI();
    }

    // Avoidボタンを押したら呼び出されます
    public void OnAvoid(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            MoveBehaviour.Avoid();
        }
    }

    // ChangeBボタンを押したら呼び出されます
    public void OnChangeBig(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && MoveBehaviour.isChange)
        {
            MoveBehaviour.BodyUp();
        }
    }

    // ChangeSボタンを押したら呼び出されます
    public void OnChangeSmall(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && MoveBehaviour.isChange)
        {
            MoveBehaviour.BodyDown();
        }
    }

}

#endregion
