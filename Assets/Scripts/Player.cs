using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Player : MonoBehaviour
{   //�R���|�[�l���g�����O�Ɏ擾
    MoveBehaviourScript moveBehaviour;
    Rigidbody player_rigidbody;

    
    //Player�̃A�j���[�^�[�̎擾
    Animator animator;

    // Animator�̃p�����[�^�[ID
    static readonly int isAttackId = Animator.StringToHash("isAttack");
    static readonly int isJumpId = Animator.StringToHash("isJump");


    // ���[�U�[����̓���
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

    // ���[�U�[�����Move�A�N�V�����ɑ΂��ČĂяo����܂��B
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    // ���[�U�[�����Move�A�N�V�����ɑ΂��ČĂяo����܂��B
    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();

    }

    // Fire�{�^������������Ăяo����܂�
    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            animator.SetTrigger(isAttackId);
            moveBehaviour.Fire();
        }
    }

    // Jump�{�^������������Ăяo����܂�
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

    // Injury�{�^������������Ăяo����܂�
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


