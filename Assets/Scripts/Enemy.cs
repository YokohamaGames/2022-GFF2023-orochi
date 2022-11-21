using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.UI;

//�G��AI�ƃX�e�[�^�X�ݒ�
public class Enemy : MonoBehaviour
{
    //�G�̃X�e�[�^�X�Ɋւ��鐔�l�ݒ�

    //�G�̒ǐՎ����s�X�s�[�h��ݒ�
    [SerializeField] private float ChaseSpeed;
    //�U���������̕��s�X�s�[�h��ݒ�
    [SerializeField] private float AttackReadySpeed =1;
    //�ǐՖڕW�̐ݒ�
    [SerializeField]
    private Transform target = null;
    //�A�j���[�^�[�̐ݒ�
    [SerializeField] private Animator animator = null;
    //�G�̉�]���x��ݒ肵�܂�
    [SerializeField]
    private float rotMax;
    //�q�I�u�W�F�N�g���擾
    [SerializeField]
    private SearchArea searchArea;
    //�q�I�u�W�F�N�g���擾
    [SerializeField]
    private AttackArea attackArea;
    //�G�̎�����̓����蔻����擾
    [SerializeField]
    private Collider Weapon_Collider;
    //�X�e�[�g�J�ڂ�x�点�鎞��
    [SerializeField]
    private float Transition_Time;                         
    //�΋��̃v���n�u�̎擾
    [SerializeField]
    private GameObject shellPrefab;
    //�G���j�G�t�F�N�g
    [SerializeField]
    private GameObject defeateffect;
    [SerializeField]
    private Transform Enemy_L_Hand;                        //�G�̍���̍��W���擾���܂�
    //�G��HP��ݒ�
    [SerializeField]
    int EnemyHp;
    //�G��HpBar���Q��
    [SerializeField]
    public Slider EnemyHpBar;                              
    //����Effect���擾
    [SerializeField]
    private GameObject SwordEffect;
    //�U����������U���܂ł̎��Ԃ̐ݒ�
    [SerializeField] private float TimetoAttack = 2;
    //�G���j���̓G�̏��ł܂ł̎��Ԃ̐ݒ�
    [SerializeField] private float DeleteEnemyTime;
    //�U���܂ł̑ҋ@���Ԃ�ݒ肵���l�Ƀ��Z�b�g����ϐ�
    float timetoattack;                                    

    public bool SearchArea = false;

    public bool AttackArea = false;

    public bool LongAttackArea = false;
    //���݂̓G�̕��s�X�s�[�h
    float speed = 5;                                       
    // �R���|�[�l���g�����O�ɎQ�Ƃ��Ă����ϐ�
    new Rigidbody rigidbody;

    // Animator�̃p�����[�^�[ID
    int baseLayerIndex = -1;
    static readonly int attackReadyHash = Animator.StringToHash("Base Layer.AttackReady");
    static readonly int isDiscover = Animator.StringToHash("isDiscover");
    static readonly int isLost = Animator.StringToHash("isLost");
    static readonly int isAttackReady = Animator.StringToHash("isAttackReady");
    static readonly int isAttack = Animator.StringToHash("isAttack");
    static readonly int isAttack2 = Animator.StringToHash("isAttack2");
    static readonly int isAttack3 = Animator.StringToHash("isAttack3");
    static readonly int isLongAttack = Animator.StringToHash("isLongAttack");
    static readonly int speedId = Animator.StringToHash("Speed");

    //�G�̃X�e�[�g�p�^�[��
    enum EnemyState
    {
        Idle,                                              //  �ҋ@
        Discover,                                          //  ����
        Move,                                              //  �ړ�
        AttackReady,                                       //  �U������
        Attack,                                            ////�U��////
        Attack2,                                           //    |
        Attack3,                                           //    |
        LongAttack,                                        //  �U��////
        Dead,                                              //  ���S
    }
    EnemyState currentState = EnemyState.Idle;
    float sp = 1.0f;
    void Start()
    {
        baseLayerIndex = animator.GetLayerIndex("Base Layer");
        rigidbody = GetComponent<Rigidbody>();             
        timetoattack = TimetoAttack;                       //�U�����Ԃ��w�肵�����ԂɃ��Z�b�g����ϐ��ɒl����
        Weapon_Collider.enabled = false;                   //�G�̕���̓����蔻����I�t
        SwordEffect.SetActive(false);
        EnemyHpBar.value = EnemyHp;                        // Slider�̏�����Ԃ�ݒ� 
    }

    // Update is called once per frame
    void Update()
    {
        
            // ��Ԃ��Ƃ̕��򏈗�
            switch (currentState)
            {
                case EnemyState.Idle:
                    UpdateForIdle();
                    break;
                case EnemyState.Move:
                    UpdateForMove();
                    break;
                case EnemyState.Discover:
                    UpdateForMove();
                    break;
                case EnemyState.Attack:
                case EnemyState.Attack2:
                case EnemyState.Attack3:
                    UpdateForAttack();
                    break;
                case EnemyState.LongAttack:
                case EnemyState.AttackReady:
                    UpdateForAttackReady();
                    break;
                default:
                    break;
            }
        //animator.SetFloat("Speed", 0.01f);
        
        Debug.Log(currentState);

    }
    //�������U���ɐ؂�ւ�
    public void LongAttack()
    {
        if (currentState == EnemyState.Move)
        {
            animator.SetTrigger(isLongAttack);
            currentState = EnemyState.LongAttack;
        }
    }

    //���G�͈͊O�ɂł��Ƃ��̏���
    public void SetIdleState()
    {
        currentState = EnemyState.Idle;
        animator.SetTrigger(isLost);
    }

    //���G�͈͓��ɓ��������̏���
    public void SetDiscoverState()
    {
        currentState = EnemyState.Discover;
        speed = ChaseSpeed;
    }
   
    //Move�X�e�[�g�ɕύX
    public void SetMoveState()
    {
        currentState = EnemyState.Move;
        //speed = ChaseSpeed;
    }

    //�U���͈͓��ɓ��������ɃX�e�[�g���U�������ɐ؂�ւ�
    public void SetAttackReadyState()
    {
        currentState = EnemyState.AttackReady;
        speed = AttackReadySpeed;                          //�U���͈͂ɓ�������l�q���ňړ����x������������
        animator.SetTrigger(isAttackReady);
        timetoattack = TimetoAttack;                       ////�U���܂ł̎��Ԃ̃J�E���g�����Z�b�g
    }
    
    public void SetAttackReady()
    {
        currentState = EnemyState.AttackReady;
        speed = AttackReadySpeed;                          //�U���͈͂ɓ�������l�q���ňړ����x������������
        timetoattack = TimetoAttack;                       //�U���܂ł̎��Ԃ̃J�E���g�����Z�b�g
        
    }
   
    //�����_���ɍU������B�U�����͈ړ����x��0�ɐݒ�
    public void Attacks()
    {
        float tmp = Random.Range(1.0f, 4.0f);              //1�`�U����ސ��̗������擾
        int random = (int)tmp;                             //float�^�̗�����int�^�ɃL���X�g
        //SetColliderOn(Weapon_Collider);
        //Debug.Log(random);
        switch (random)
        {
                case 1:
                   currentState = EnemyState.Attack;
                   animator.SetTrigger(isAttack);
                   break;
                case 2:
                   currentState = EnemyState.Attack2;
                   animator.SetTrigger(isAttack2);
                   break;
                case 3:
                   currentState = EnemyState.Attack3;
                   animator.SetTrigger(isAttack3);
                   break;
                default:
                break;
        }
        rigidbody.velocity = Vector3.zero;                       //�����~�܂�
        timetoattack = TimetoAttack;                          //�U���܂ł̎��Ԃ̃J�E���g�����Z�b�g

    }
    
    //�����蔻���ON�ɂ���֐�
    public void SetColliderOn(Collider collider)
    {
        SwordEffect.SetActive(true);
        collider.enabled = true;
        Debug.Log("�Ă΂ꂽ");
    }
   
    //�����蔻���OFF�ɂ���֐�
    public void SetColliderOff(Collider collider)
    {
        SwordEffect.SetActive(false);
        collider.enabled = false;
    }

   
    //�ҋ@��Ԃ̏���
    void UpdateForIdle()
    {
        //Debug.Log("�ҋ@��");
    }

    float spd;
    //�v���C��[�Ɍ������ē�������
    void UpdateForMove()
    {
        //�^�[�Q�b�g�����̃x�N�g�������߂�
        Vector3 vec = target.position - transform.position;
        vec.y = 0;
        // �^�[�Q�b�g�̕���������
        // Quaternion(��]�l)���擾
        Quaternion quaternion = Quaternion.LookRotation(vec);
        
        // �Z�o������]�l�����̃Q�[���I�u�W�F�N�g��rotation�ɑ��
        transform.rotation = quaternion;
        rigidbody.velocity = transform.forward * speed;// ���ʕ����Ɉړ�

        if (currentState == EnemyState.Discover && spd <= 2.00f)
        {
            spd += sp * Time.deltaTime;
        }

        if (speed <= ChaseSpeed)
        {
            speed += (ChaseSpeed * Time.deltaTime) /2;
        }
        animator.SetFloat(speedId, spd);


    }

    //�U���͈͂ɂƂǂ܂��Ă��鎞�Ԃ��J�E���g���Ĉ�莞�Ԃ𒴂�����AttackState�ɐ؂�ւ���
    void UpdateForAttackReady()
    {
        //�ړ����x��0�ɂ��v���C���[�̌����ɉ�]����B
        //rigidbody.velocity = Vector3.zero;
        Rotate();
        timetoattack -= Time.deltaTime;
        
        if(0 > timetoattack)                               //�U���܂ł̎��Ԃ�0�ɂȂ�΃X�e�[�g�J�ځB�J�E���g�����Z�b�g����B
        {
            //�����_���ȍU��
            Attacks();  
        }
    }

    //�v���C���[�̕����ɉ�]����֐�
    void Rotate()
    {
        //�^�[�Q�b�g�����̃x�N�g�������߂�
        Vector3 vec = target.position - transform.position;

        // �^�[�Q�b�g�̕���������
        // Quaternion(��]�l)���擾
        Quaternion quaternion = Quaternion.LookRotation(vec);
        // �Z�o������]�l�����̃Q�[���I�u�W�F�N�g��rotation�ɑ��
        this.transform.rotation = quaternion;
    }
    
    //Attack�X�e�[�g���̏���
    void UpdateForAttack()
    {
        var stateInfo = animator.GetCurrentAnimatorStateInfo(baseLayerIndex);
        if (stateInfo.fullPathHash == attackReadyHash)
        {
            SetAttackReady();
        }
        Rotate();
    }
    
    //�X�e�[�g�J�ڂ�x�点��֐��@���g�p
    IEnumerator DelayState()
    {
        yield return new WaitForSeconds(Transition_Time);
    }

    //�G�̉������U���̃v���n�u�̐���
    public void EnemyShotAttack()
    {
        GameObject shell = Instantiate(shellPrefab, Enemy_L_Hand.transform.position, Quaternion.identity);
        Rigidbody shellRb = shell.GetComponent<Rigidbody>();
        // �e����ݒ�
        shellRb.AddForce(transform.forward * 1500);
        Destroy(shell, 1.0f);
    }

    //�G��HP�o�[�̏���
    public void EnemyDamage(int playeratk)
    {
        EnemyHp -= playeratk;
        EnemyHpBar.value = EnemyHp;

        //HP0�̂Ƃ����j�G�t�F�N�g�̐����ƓG�I�u�W�F�N�g�̍폜
        if (EnemyHp <= 0)
        {
            GameObject defeat = Instantiate(defeateffect, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject,DeleteEnemyTime);
            Destroy(defeat, 6.0f);
        }
    }

  
}






