using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
//�G��Ai�ƃX�e�[�^�X�ݒ�
public class Enemy : MonoBehaviour
{
    //�G�̃X�e�[�^�X�Ɋւ��鐔�l�ݒ�

    
    [SerializeField]
    private float ChaseSpeed;                              //�G�̒ǐՎ����s�X�s�[�h��ݒ�

    [SerializeField]
    private float AttackReadySpeed = 1;                    //�U���������̕��s�X�s�[�h��ݒ�  

    [SerializeField]
    private float TimetoAttack = 2;                        //�U����������U���܂ł̎��Ԃ̐ݒ�

    [SerializeField]
    private Transform target;                              //�ǐՖڕW�̐ݒ�

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

    float timetoattack;                                    //�U�����Ԃ�ݒ肵�����ԂɃ��Z�b�g����ϐ�
    

    public bool SearchArea = false;

    public bool AttackArea = false;
    float speed = 1;                                       //���݂̓G�̕��s�X�s�[�h


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
        rigidbody = GetComponent<Rigidbody>();             
        timetoattack = TimetoAttack;                       //�U�����Ԃ��w�肵�����ԂɃ��Z�b�g����ϐ��ɒl����
        
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
                    UpdateForDiscover();
                    break;
                case EnemyState.AttackReady:
                    UpdateForAttackReady();
                    break;
                default:
                    break;
            }

        }
        /*���G�͈͂ƍU���͈͂̒��ɂ���Ƃ��U�����[�h
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
        */
        
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
        currentState = EnemyState.Move;
        speed = ChaseSpeed;
    }
    public void SetAttackReadyState()
    {
        currentState = EnemyState.AttackReady;
        speed = 0;                                         //�U���͈͂ɓ�������l�q���ňړ����x������������
        animator.SetTrigger(isAttackReady);
        
    }
    public void SetAttackState()
    {
        currentState = EnemyState.Attack;
        speed = 0;
        timetoattack = TimetoAttack;                       ////�U���܂ł̎��Ԃ̃J�E���g�����Z�b�g

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


       
        //�^�[�Q�b�g�����̃x�N�g�������߂�
        Vector3 vec = target.position - transform.position;

        // �^�[�Q�b�g�̕���������
        // Quaternion(��]�l)���擾
        Quaternion quaternion = Quaternion.LookRotation(vec);
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(vec.x, 0, vec.z)), rotMax);
        rigidbody.velocity = new Vector3(0, 0, speed);  // ���ʕ����Ɉړ�

        // �Z�o������]�l�����̃Q�[���I�u�W�F�N�g��rotation�ɑ��
        this.transform.rotation = quaternion;


    }
    //�U���͈͂ɂƂǂ܂��Ă��鎞�Ԃ��J�E���g���Ĉ�莞�Ԃ𒴂�����AttackState�ɐ؂�ւ��A�J�E���g��0�Ƀ��Z�b�g
    void UpdateForAttackReady()
    {
        
        Debug.Log(timetoattack);
        timetoattack -= Time.deltaTime;
        
        if(0 > timetoattack)                               //�U���܂ł̎��Ԃ�0�ɂȂ�΃X�e�[�g�J�ځB�J�E���g�����Z�b�g����B
        {
            
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
        rigidbody.velocity = new Vector3(0, 0, speed);  // ���ʕ����Ɉړ�

        // �Z�o������]�l�����̃Q�[���I�u�W�F�N�g��rotation�ɑ��
        this.transform.rotation = quaternion;


    }
    void UpdateForDiscover()
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
}






