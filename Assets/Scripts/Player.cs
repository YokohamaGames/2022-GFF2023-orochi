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
    Vector3 moveInput = Vector3.zero;
    Vector2 lookInput = Vector2.zero;

    void Start()
    {
        moveBehaviour = GetComponent<MoveBehaviourScript>();
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        moveBehaviour.Move(moveInput);
    }
    private void FixedUpdate()
    {

    }

    // ユーザーからのMoveアクションに対して呼び出されます。
    public void OnMove(InputAction.CallbackContext context)
    {
        var moveInput = context.ReadValue<Vector2>();
        this.moveInput.x = moveInput.x;
        this.moveInput.y = 0;
        this.moveInput.z = moveInput.y;
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
            Tackle();
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

    public void OnInjury(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            PlayerHPbar.Instance.Damage();
        }
    }

    //Fireボタンで呼び出されるタックル攻撃。addForceを使用せずtransformで代用。向いている方向に座標を+する。
    void Tackle()
    {
        var player_transform = transform.position;
        player_transform += transform.forward;
        transform.position = player_transform;
    
    }

    
}


