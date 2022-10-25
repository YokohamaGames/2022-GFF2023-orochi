using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
//�G��Ai�ƃX�e�[�^�X�ݒ�

 //�}�W�b�N�i���o�[���ɖ��O��t����N���X
public static class Define
{
    public const float Number_of_Attack_Type = 4;          //�U����ސ�
}

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
    [SerializeField]
    private Collider Weapon_Collider;                      //�G�̎�����̓����蔻����擾
    [SerializeField]
    private float Transition_Time;                         //�X�e�[�g�J�ڂ�x�点�鎞��
    
    float timetoattack;                                    //�U�����Ԃ�ݒ肵�����ԂɃ��Z�b�g����ϐ�

    [SerializeField]
    int EnemyHp = 2;

    public bool SearchArea = false;

    public bool AttackArea = false;
    float speed = 1;                                       //���݂̓G�̕��s�X�s�[�h

    int[] AttacksCount = { 0, 0, 0 };                      //�U���̎�ޕ��z���p�ӁB�Z���g������J�E���g��+1����B

    
    // �R���|�[�l���g�����O�ɎQ�Ƃ��Ă����ϐ�
    new Rigidbody rigidbody;
    //Animator animator;
    
    // Animator�̃p�����[�^�[ID
    static readonly int isDiscover = Animator.StringToHash("isDiscover");
    static readonly int isLost = Animator.StringToHash("isLost");
    static readonly int isAttackReady = Animator.StringToHash("isAttackReady");
    static readonly int isAttack = Animator.StringToHash("isAttack");
    static readonly int isAttack2 = Animator.StringToHash("isAttack2");
    static readonly int isAttack3 = Animator.StringToHash("isAttack3");





     enum EnemyState
    {
        Stay,                                              //  �ҋ@
        Discover,                                          //  ����
        Move,                                              //  �ړ�
        AttackReady,                                       //  �U������
        Attack,                                            ////�U��////
        Attack2,                                           //    |
        Attack3,                                           //  �U��////
        Escape,                                            //  ���
    }

    EnemyState currentState = EnemyState.Stay;
    void Start()
    {
        //animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();             
        timetoattack = TimetoAttack;                       //�U�����Ԃ��w�肵�����ԂɃ��Z�b�g����ϐ��ɒl����
        Weapon_Collider.enabled = false;                   //�G�̕���̓����蔻����I�t
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
                case EnemyState.Discover:
                    UpdateForDiscover();
                    break;
                case EnemyState.Attack:
                case EnemyState.Attack2:
                case EnemyState.Attack3:
                    UpdateForAttack();
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

        Debug.Log(currentState);
        //Debug.Log(timetoattack);

        if(StageScene.Instance.Enemyhp == 0)
        {
            Destroy(this.gameObject);
        }
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
        timetoattack = TimetoAttack;                       ////�U���܂ł̎��Ԃ̃J�E���g�����Z�b�g

    }
    //Animator�̃Z�b�g�g���K�[�͂��Ȃ�
    public void SetAttackReady()
    {
        currentState = EnemyState.AttackReady;
        speed = AttackReadySpeed;                                         //�U���͈͂ɓ�������l�q���ňړ����x������������
        timetoattack = TimetoAttack;                       ////�U���܂ł̎��Ԃ̃J�E���g�����Z�b�g
        
    }
    //�����_���ɍU������
    public void Attacks()
    {
        float tmp = Random.Range(1.0f, 4.0f);              //1�`�U����ސ��̗������擾
        int random = (int)tmp;                             //float�^�̗�����int�^�ɃL���X�g
        //SetColliderOn(Weapon_Collider);
        //Debug.Log(random);
        switch (random)
        {
            case 1:currentState = EnemyState.Attack;
                   animator.SetTrigger(isAttack);
                   break;
            case 2:currentState = EnemyState.Attack2;
                   animator.SetTrigger(isAttack2);
                   break;
            case 3:currentState = EnemyState.Attack3;
                   animator.SetTrigger(isAttack3);
                   break;
            default:
                break;
        }
        rigidbody.velocity = Vector3.zero;                       //�����~�܂�
        timetoattack = TimetoAttack;                       ////�U���܂ł̎��Ԃ̃J�E���g�����Z�b�g
        //animator.SetTrigger(isAttack);
        //AttacksCount[0] += 1;

    }
    //�����蔻���ON�ɂ���֐�
    public void SetColliderOn(Collider collider)
    {
        collider.enabled = true;
        Debug.Log("�Ă΂ꂽ");
    }
    //�����蔻���OFF�ɂ���֐�
    public void SetColliderOff(Collider collider)
    {
        collider.enabled = false;
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
         rigidbody.velocity = Vector3.zero;
         Rotate();

        //Debug.Log(timetoattack);
        timetoattack -= Time.deltaTime;
        
        if(0 > timetoattack)                               //�U���܂ł̎��Ԃ�0�ɂȂ�΃X�e�[�g�J�ځB�J�E���g�����Z�b�g����B
        {
            
            Attacks();
            
        }
    }
    void Rotate()
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

    void UpdateForAttack()
    {
        speed = 0;
        //�^�[�Q�b�g�����̃x�N�g�������߂�
        Vector3 vec = target.position - transform.position;
        // Quaternion(��]�l)���擾 ��]����x�����擾
        Quaternion quaternion = Quaternion.LookRotation(vec);
        //�擾�����x�����I�u�W�F�N�g����]������
        transform.rotation = quaternion;
    }
    //�z��̒��g�̍ŏ��l��T���֐�:������
    /*void CheckAttackCount(int[] atk_array)
    {
        for (int i = 0; i < atk_array.Length; i++)
        {
            if(atk_array[i] )
        }
    }*/

    IEnumerator DelayState()
    {
        yield return new WaitForSeconds(Transition_Time);
    }

    
}






