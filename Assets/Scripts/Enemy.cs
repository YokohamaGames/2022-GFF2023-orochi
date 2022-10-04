using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Enemy : MonoBehaviour
{


    //�G�̒ǐՎ����s�X�s�[�h��ݒ肵�܂�
    [SerializeField]
    private float ChaseSpeed;
    [SerializeField]
    private float AttackSpeed = 1;
    //���݂̓G�̕��s�X�s�[�h
    float speed = 1;
   
    //�ǐՖڕW��ݒ肵�܂�
    [SerializeField]
    private Transform target;

    [SerializeField]
    private Animation anim;


    //�G�̉�]���x��ݒ肵�܂�
    [SerializeField]
    private float rotMax;

    // ���[�U�[����̓���
    Vector2 moveInput = Vector2.zero;
    Vector2 lookInput = Vector2.zero;

    public bool SearchArea = false;

    public bool AttackArea = false;

    // �R���|�[�l���g�����O�ɎQ�Ƃ��Ă����ϐ�
    Animator animator;

    // �R���|�[�l���g�����O�ɎQ�Ƃ��Ă����ϐ�
    new Rigidbody rigidbody;

    // Animator�̃p�����[�^�[ID
    static readonly int isDiscover = Animator.StringToHash("isDiscover");
    
    enum EnemyState
    {
        //�ҋ@���
        Stay,

        //�������
        Discover,

        //�ړ����
        Move,

        //�U�����
        Attack,

        //������
        Escape,


    }

    EnemyState currentState = EnemyState.Stay;
    void Start()
    {
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        {
            // ��Ԃ��Ƃ̕��򏈗�
            switch (currentState)
            {
                case EnemyState.Stay:
                    UpdateForStay();
                    break;
                case EnemyState.Move:
                    UpdateForMove();
                    break;
                case EnemyState.Attack:
                    UpdateForAttack();
                    break;
                default:
                    break;
            }



        }
        //���G�͈͂ƍU���͈͂̒��ɂ���Ƃ��U�����[�h
        /*if (SearchArea && AttackArea)
        {
            Debug.Log("�U���͈͓�");
        }
        //���G�͈͓��݂̂̎��ǐՃ��[�h
        else if (SearchArea)
        {
            Debug.Log("���G�͈͓�");
        }
        //���G�͈͊O�̎�Stay
        else if (!SearchArea)
        {
            Debug.Log("���G�͈͊O");
        }

        Debug.Log(currentState);*/


        // �Q�[���p�b�h���ڑ�����Ă��Ȃ���null�ɂȂ�B
        if (Gamepad.current == null) return;

        if (Gamepad.current.buttonNorth.wasPressedThisFrame)
        {
            Debug.Log("Button North�������ꂽ�I");
        }
        if (Gamepad.current.buttonSouth.wasReleasedThisFrame)
        {
            Debug.Log("Button South�������ꂽ�I");
        }

    }

    public void SetStayState()
    {
        currentState = EnemyState.Stay;
        
    }

    public void SetDiscoverState()
    {
        currentState = EnemyState.Discover;
        animator.SetBool(isDiscover, true);
    }
    public void SetMoveState()
    {
        animator.SetBool(isDiscover, false);
        currentState = EnemyState.Move;
        speed = ChaseSpeed;
    }
    public void SetAttackState()
    {
        currentState = EnemyState.Attack;
        speed = AttackSpeed;
    }
    public void SetEscapeState()
    {

    }
    void UpdateForStay()
    {
        //Debug.Log("�ҋ@��");
    }

    void UpdateForMove()
    {


        // �v���C���[�̑O�㍶�E�̈ړ�
        var velocity = Vector3.zero;
        velocity = transform.forward * moveInput.y * speed;
        velocity += transform.right * moveInput.x * speed;
        velocity.y = rigidbody.velocity.y;
        rigidbody.velocity = velocity;

        // �v���C���[�̕��p����]
        transform.Rotate(0, lookInput.x, 0);

        /*�^�[�Q�b�g�����̃x�N�g�������߂�
        Vector3 vec = target.position - transform.position;

        // �^�[�Q�b�g�̕���������
        // Quaternion(��]�l)���擾
        Quaternion quaternion = Quaternion.LookRotation(vec);
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(vec.x, 0, vec.z)), rotMax);
        transform.Translate(Vector3.forward * speed * 0.01f); // ���ʕ����Ɉړ�

        // �Z�o������]�l�����̃Q�[���I�u�W�F�N�g��rotation�ɑ��
        this.transform.rotation = quaternion;*/


    }

    void UpdateForAttack()
    {

        //�^�[�Q�b�g�����̃x�N�g�������߂�
        Vector3 vec = target.position - transform.position;

        // �^�[�Q�b�g�̕���������
        // Quaternion(��]�l)���擾
        Quaternion quaternion = Quaternion.LookRotation(vec);
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(vec.x, 0, vec.z)), rotMax);
        transform.Translate(Vector3.forward * speed * 0.01f); // ���ʕ����Ɉړ�

        // �Z�o������]�l�����̃Q�[���I�u�W�F�N�g��rotation�ɑ��
        this.transform.rotation = quaternion;
        
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        // �A�N�V�������n�܂���
        /*if (context.started)
        {
            Debug.Log($"started : {moveInput}");
        }
        // �A�N�V�������p����
        else if (context.performed)
        {
            Debug.Log($"performed : {moveInput}");
        }
        // �A�N�V�������I��
        else if (context.canceled)
        {
            Debug.Log($"canceled : {moveInput}");
        }*/
    }

    // ���[�U�[�����Move�A�N�V�����ɑ΂��ČĂяo����܂��B
    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();

        // �A�N�V�������n�܂���
        /*if (context.started)
        {
            Debug.Log($"started : {lookInput}");
        }
        // �A�N�V�������p����
        else if (context.performed)
        {
            Debug.Log($"performed : {lookInput}");
        }
        // �A�N�V�������I��
        else if (context.canceled)
        {
            Debug.Log($"canceled : {lookInput}");
        }*/
    }

}




