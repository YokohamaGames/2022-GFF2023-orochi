using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Player : MonoBehaviour
{   //コンポーネントを事前に取得
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

}


