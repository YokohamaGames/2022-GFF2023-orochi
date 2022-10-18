using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;


public class Player2 : MonoBehaviour
{
    //スクリプトの取得
    MoveBehaviourScript moveBehaviour;
    PlayerAnimation player_animation;
    

    // ユーザーからの入力
    Vector3 moveInput = Vector3.zero;
    Vector2 lookInput = Vector2.zero;

    
    void Start()
    {
        moveBehaviour = GetComponent<MoveBehaviourScript>();
        //animator = GetComponent<Animator>();
        player_animation = GetComponent<PlayerAnimation>();
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
        this.moveInput.x = -moveInput.x;
        this.moveInput.y = 0;
        this.moveInput.z = -moveInput.y;
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

            moveBehaviour.Attack();
            moveBehaviour.Fire();
        }
    }
}

