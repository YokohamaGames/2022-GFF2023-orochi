using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
//�G��Ai�ƃX�e�[�^�X�ݒ�
public class Enemy : MonoBehaviour
{
    //�G�̃X�e�[�^�X�Ɋւ��鐔�l�ݒ�

    //�G�̒ǐՎ����s�X�s�[�h��ݒ肵�܂�
    [SerializeField]
    private float ChaseSpeed;
    [SerializeField]
    private float AttackSpeed = 1;
    //���݂̓G�̕��s�X�s�[�h
    float speed = 1;
    float TimeCount = 0;                        //���Ԃ��J�E���g����ϐ��B�U���͈͂ɂƂǂ܂��Ă��鎞�Ԃ̃J�E���g
    [SerializeField]
    private float TimetoAttack = 2;
    //�ǐՖڕW��ݒ肵�܂�
    [SerializeField]
    private Transform target;

    [SerializeField]
    private Animator animator;


    //�G�̉�]���x��ݒ肵�܂�
    [SerializeField]
    private float rotMax;
    //�q�I�u�W�F�N�g���擾
    [SerializeField]
    private SearchArea searchArea;
    //�q�I�u�W�F�N�g���擾
    [SerializeField]
    private AttackArea attackArea;

    // ���[�U�[����̓���
    Vector2 moveInput = Vector2.zero;
    Vector2 lookInput = Vector2.zero;

    public bool SearchArea = false;

    public bool AttackArea = false;


    // �R���|�[�l���g�����O�ɎQ�Ƃ��Ă����ϐ�
    new Rigidbody rigidbody;
    //Animator animator;
    
    // Animator�̃p�����[�^�[ID
    static readonly int isDiscover = Animator.StringToHash("isDiscover");
    static readonly int isLost = Animator.StringToHash("isLost");
    static readonly int isAttackReady = Animator.StringToHash("isAttackReady");
    static readonly int isAttack = Animator.StringToHash("isAttack");




    enum EnemyState
    {
        //�ҋ@���
        Stay,

        //�������
        Discover,

        //�ړ����
        Move,

        //�U������
        AttackReady,
        //�U�����
        Attack,

        //������
        Escape,


    }

    EnemyState currentState = EnemyState.Stay;
    void Start()
    {
        //animator = GetComponent<Animator>();
        
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
                case EnemyState.Discover:
                    UpDateForDiscover();
                    break;
                case EnemyState.AttackReady:
                    UpdateForAttackReady();
                    break;
                default:
                    break;
            }

        }
        //���G�͈͂ƍU���͈͂̒��ɂ���Ƃ��U�����[�h
        if (SearchArea && AttackArea)
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

        Debug.Log(currentState);
        //Debug.Log(TimeCount);

        

    }

    public void SetStayState()
    {
        currentState = EnemyState.Stay;
        animator.SetTrigger(isLost);
        
    }

    public void SetDiscoverState()
    {
        currentState = EnemyState.Discover;
        animator.SetTrigger(isDiscover);
    }
    public void SetMoveState()
    {
        //animator.SetBool(isDiscover, false);
        currentState = EnemyState.Move;
        speed = ChaseSpeed;
    }
    public void SetAttackReadyState()
    {
        currentState = EnemyState.AttackReady;
        speed = AttackSpeed;                   //�U���͈͂ɓ�������l�q���ňړ����x������������
        animator.SetTrigger(isAttackReady);
        
    }
    public void SetAttackState()
    {
        currentState = EnemyState.Attack;
        speed = 0;
        TimeCount = 0;
        animator.SetTrigger(isAttack);

    }
    public void SetEscapeState()
    {

    }
    //�ҋ@��Ԃ̃A�b�v�f�[�g����
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
    //�U���͈͂ɂƂǂ܂��Ă��鎞�Ԃ��J�E���g���Ĉ�莞�Ԃ𒴂�����AttackState�ɐ؂�ւ��A�J�E���g��0�Ƀ��Z�b�g
    void UpdateForAttackReady()
    {
       // if (TimeCount > 2)
           // TimeCount = 0;
        TimeCount += Time.deltaTime;
        if(TimeCount > TimetoAttack)
        {
            //TimeCount = 0;
            SetAttackState();
        }
    }
    void UpdateForAttack()
    {

        //�^�[�Q�b�g�����̃x�N�g�������߂�
        Vector3 vec = target.position - transform.position;

        // �^�[�Q�b�g�̕���������
        // Quaternion(��]�l)���擾
        Quaternion quaternion = Quaternion.LookRotation(vec);
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(vec.x, 0, vec.z)), rotMax);
        // �Z�o������]�l�����̃Q�[���I�u�W�F�N�g��rotation�ɑ��
        transform.rotation = quaternion;
        transform.Translate(Vector3.forward * speed * 0.01f); // ���ʕ����Ɉړ�

        
        
    }
    void UpDateForDiscover()
    {
        speed = ChaseSpeed;
        if(currentState == EnemyState.Discover)
        {
            //�^�[�Q�b�g�����̃x�N�g�������߂�
            Vector3 vec = target.position - transform.position;
            // Quaternion(��]�l)���擾 ��]����x�����擾
            Quaternion quaternion = Quaternion.LookRotation(vec);
            //�擾�����x�����I�u�W�F�N�g����]������
            transform.rotation = quaternion;
            transform.Translate(Vector3.forward * speed * 0.01f); // ���ʕ����Ɉړ�

        }
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




