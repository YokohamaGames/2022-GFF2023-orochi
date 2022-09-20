using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
   
   
    //�G�̒ǐՎ����s�X�s�[�h��ݒ肵�܂�
    [SerializeField]
    private float ChaseSpeed = 5;
    [SerializeField]
    private float AttackSpeed = 1;
    //���݂̓G�̕��s�X�s�[�h
    float speed;
    //�ǐՋ@�\���������܂�
    [SerializeField]
    private NavMeshAgent enemy;
    //�ǐՖڕW��ݒ肵�܂�
    [SerializeField]
    private GameObject target;
    

    public bool SearchArea = false;

    public bool AttackArea = false;

    enum EnemyState
    {
        //�ҋ@���
        Stay,

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

            //���G�͈͊O�Ȃ痧���~�܂�
            if (!SearchArea)
            {
                enemy.updatePosition = false;                                  //�������~�߂�
                enemy.updateRotation = false;                                  //��]���~�߂�
                enemy.destination = enemy.transform.position;                  //�ڕW�n�_���������g�ɐݒ肵�u�Ԉړ��̖h�~
            }
            //���G�͈͓��Ȃ�^�[�Q�b�g��ǐՂ���
            else if (target != null && SearchArea)
            {
                enemy.updatePosition = true;                                   //�ڕW�n�_�֓������n�߂�
                enemy.updateRotation = true;                                   //�ڕW�n�_�։�]���n�߂�
                enemy.destination = target.transform.position;                 //�ڕW�n�_���^�[�Q�b�g�ɐݒ�
            }
            
        }
        //���G�͈͂ƍU���͈͂̒��ɂ���Ƃ��U�����[�h
        if (SearchArea && AttackArea)
        {
            Debug.Log("�U���͈͓�");
        }
        //���G�͈͓��݂̂̎��ǐՃ��[�h
        else if(SearchArea)
        {
            Debug.Log("���G�͈͓�");
        }
        //���G�͈͊O�̎�Stay
        else if (!SearchArea)
        {
            Debug.Log("���G�͈͊O");
        }
        
        



        
    }

    public void SetStayState()
    {
        currentState = EnemyState.Stay;
        speed = 0;
    }

    public void SetMoveState()
    {
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

    }

    void UpdateForAttack()
    {
        enemy.updatePosition = true;
        enemy.updateRotation = true;
        enemy.destination = target.transform.position;
    }

   /* private void OnTriggerExit(Collider colision)
    {
        if (colision.CompareTag("Player"))
        {
            //Debug.Log("���G�͈͊O");
            SearchArea = false;
        }
    }*/
}


