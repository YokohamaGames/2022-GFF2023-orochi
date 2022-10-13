using UnityEngine;
using UnityEngine.InputSystem;

public class Player2 : MonoBehaviour
{

    MoveBehaviourScript moveBehaviour;


    // ���[�U�[����̓���
    Vector3 moveInput = Vector3.zero;
    Vector2 lookInput = Vector2.zero;

    void Start()
    {
        moveBehaviour = GetComponent<MoveBehaviourScript>();
    }


    void Update()
    {
        moveBehaviour.Move(moveInput);
    }
    private void FixedUpdate()
    {

    }

    // ���[�U�[�����Move�A�N�V�����ɑ΂��ČĂяo����܂��B
    public void OnMove(InputAction.CallbackContext context)
    {
        var moveInput = context.ReadValue<Vector2>();
        this.moveInput.x = -moveInput.x;
        this.moveInput.y = 0;
        this.moveInput.z = -moveInput.y;
    }

    // ���[�U�[�����Move�A�N�V�����ɑ΂��ČĂяo����܂��B
    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();

    }

    //Fire�{�^������������Ăяo����܂�
    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            moveBehaviour.Jump();
        }
    }
}

