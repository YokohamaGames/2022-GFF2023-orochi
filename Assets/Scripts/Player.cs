using UnityEngine;
using UnityEngine.InputSystem;

// 
public class Player : MonoBehaviour
{
    MoveBehaviour moveBehaviour;

    // ユーザーからの入力
    Vector2 moveInput = Vector2.zero;
    Vector2 lookInput = Vector2.zero;

    void Start()
    {
        moveBehaviour = GetComponent<MoveBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        moveBehaviour.Move(new Vector3(moveInput.x, 0, moveInput.y));
        moveBehaviour.Rotate(lookInput.x);
    }

    // ユーザーからのMoveアクションに対して呼び出されます。
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    // ユーザーからのMoveアクションに対して呼び出されます。
    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    //Fireボタンを押したら呼び出されます
    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            moveBehaviour.Fire();
        }
    }

    public void OnControlPauseUI(InputAction.CallbackContext context)
    {
        StageScene.Instance.ControlPauseUI();
    }
}
