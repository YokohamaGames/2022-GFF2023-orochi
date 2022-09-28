using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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


    public bool SearchArea = false;

    public bool AttackArea = false;

    // �R���|�[�l���g�����O�ɎQ�Ƃ��Ă����ϐ�
    Animator animator;
    
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




    }

    public void SetStayState()
    {
        currentState = EnemyState.Stay;
        speed = 0;
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
    {  //�^�[�Q�b�g�����̃x�N�g�������߂�
        Vector3 vec = target.position - transform.position;

        // �^�[�Q�b�g�̕���������
        // Quaternion(��]�l)���擾
        Quaternion quaternion = Quaternion.LookRotation(vec);
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(vec.x, 0, vec.z)), rotMax);
        transform.Translate(Vector3.forward * speed * 0.01f); // ���ʕ����Ɉړ�

        // �Z�o������]�l�����̃Q�[���I�u�W�F�N�g��rotation�ɑ��
        this.transform.rotation = quaternion;


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

    
}


