using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEditor;

namespace OROCHI
{
    //�G��AI�ƃX�e�[�^�X�ݒ�
    public class Enemy : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("�G�̒ǐՎ����s�X�s�[�h��ݒ�")]
        private float chasespeed;

        [SerializeField]
        [Tooltip("�U���������̕��s�X�s�[�h��ݒ�")]
        private float attackreadyspeed = 1;

        [SerializeField]
        [Tooltip("�ǐՖڕW�̐ݒ�")]
        private Transform target = null;

        [SerializeField]
        [Tooltip("�A�j���[�^�[�̐ݒ�")] 
        private Animator animator = null;

        [SerializeField]
        [Tooltip("�G�̉�]���x��ݒ肵�܂�")]
        private float rotmax;

        [SerializeField]
        [Tooltip("�q�I�u�W�F�N�g���擾")]
        private SearchArea searcharea;

        [SerializeField]
        [Tooltip("�q�I�u�W�F�N�g���擾")]
        private AttackArea attackArea;

        [SerializeField]
        [Tooltip("�G�̎�����̓����蔻����擾")]
        private Collider weaponcollider;

        [SerializeField]
        [Tooltip("�X�e�[�g�J�ڂ�x�点�鎞��")]
        private float transition_time;

        [SerializeField]
        [Tooltip("�΋��̃v���n�u�̎擾")]
        private GameObject shellprefab;

        [SerializeField]
        [Tooltip("�G���j�G�t�F�N�g")]
        private GameObject defeateffect;

        [SerializeField]
        [Tooltip("�_���[�W�G�t�F�N�g")]
        private GameObject damageeffect;

        [SerializeField]
        [Tooltip("�G�̍���̍��W���擾���܂�")]
        private Transform enemy_l_hand;

        [SerializeField]
        [Tooltip("�G��HpBar���Q��")]
        public Slider enemyhpbar;

        [SerializeField]
        [Tooltip("����Effect���擾")]
        private GameObject swordeffect;

        [SerializeField]
        [Tooltip("��_���G�t�F�N�g")]
        private GameObject HitEffect;

        [SerializeField]
        [Tooltip("�U����������U���܂ł̎��Ԃ̐ݒ�")]
        private float timetoattack = 2;

        [SerializeField]
        [Tooltip("�G���j���̓G�̏��ł܂ł̎��Ԃ̐ݒ�")]
        private float deleteenemytime;

        //�U���܂ł̑ҋ@���Ԃ�ݒ肵���l�Ƀ��Z�b�g����ϐ�
        float timetoattackreset;

        [SerializeField]
        [Tooltip("HpBar�̎擾")]
        private Image hp;

        [SerializeField]
        [Tooltip("�΋��̃N�[���^�C��")]
        float timefire = 1.5f;
        float timetoatk = 0;

        // Search��Ԃ̃t���O
        public bool isSearch = false;
        // �U����Ԃ̃t���O
        public bool isAttacks = false;
        // �������U����Ԃ̃t���O
        public bool isLongAttacks = false;
        // �̗͂�0�ɂȂ�����Ԃ̃t���O
        public bool isDead = false;
        // �ړ����x�p�̕ϐ�
        float speed = 0;
        // �R���|�[�l���g�����O�ɎQ�Ƃ��Ă����ϐ�
        new Rigidbody rigidbody;

        [Header("SE")]
        [SerializeField] private AudioClip fire;
        [SerializeField] private AudioClip swing;
        [SerializeField] private AudioClip chargedamaged;

        float sp = 1.0f;
        float spd;
        float cnt = 0;

        // Animator�̃p�����[�^�[ID
        int baseLayerIndex = -1;
        static readonly int LocomotionHash = Animator.StringToHash("Base Layer.Locomotion");
        static readonly int attackReadyHash = Animator.StringToHash("Base Layer.AttackReady");
        static readonly int isDiscover = Animator.StringToHash("isDiscover");
        static readonly int isLost = Animator.StringToHash("isLost");
        static readonly int isAttackReady = Animator.StringToHash("isAttackReady");
        static readonly int isAttack = Animator.StringToHash("isAttack");
        static readonly int isAttack2 = Animator.StringToHash("isAttack2");
        static readonly int isAttack3 = Animator.StringToHash("isAttack3");
        static readonly int isLongAttack = Animator.StringToHash("isLongAttack");
        static readonly int speedId = Animator.StringToHash("Speed");
        static readonly int DeadId = Animator.StringToHash("Dead");

        //�G�̃X�e�[�g�p�^�[��
        enum EnemyState
        {
            Idle,                  //  �ҋ@
            Discover,              //  ����
            Move,                  //  �ړ�
            AttackReady,           //  �U������
            Attack,                //  �U��1
            Attack2,               //  �U��2
            Attack3,               //  �U��3
            LongAttack,            //  �������U��
            Dead,                  //  ���S
        }

        // �A�C�h����Ԃ��Z�b�g���Ă���
        EnemyState currentState = EnemyState.Idle;

        void Start()
        {
            baseLayerIndex = animator.GetLayerIndex("Base Layer");
            rigidbody = GetComponent<Rigidbody>();
            timetoattackreset = timetoattack;                  //�U�����Ԃ��w�肵�����ԂɃ��Z�b�g����ϐ��ɒl����
            weaponcollider.enabled = false;                    //�G�̕���̓����蔻����I�t
            swordeffect.SetActive(false);
            enemyhpbar.value = StageScene.Instance.enemyHp;    // Slider�̏�����Ԃ�ݒ� 
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
                    UpdateForDiscover();
                    break;
                case EnemyState.Attack:
                case EnemyState.Attack2:
                case EnemyState.Attack3:
                    UpdateForAttack();
                    break;
                case EnemyState.LongAttack:
                    UpdateForLongAttack();
                    break;
                case EnemyState.AttackReady:
                    UpdateForAttackReady();
                    break;
                case EnemyState.Dead:
                    break;
                default:
                    break;
            }
        }

        void UpdateForLongAttack()
        {
            //�^�[�Q�b�g�����̃x�N�g�������߂�
            Vector3 vec = target.position - transform.position;

            // �^�[�Q�b�g�̕���������
            // Quaternion(��]�l)���擾
            Quaternion quaternion = Quaternion.LookRotation(vec);
            transform.rotation = quaternion;

            timetoatk += Time.deltaTime;
            // �΋��̃N�[���^�C�����o�߂��Ă�����
            if (timetoatk > timefire)
            {
                timetoatk = 0;
                animator.SetTrigger(isLongAttack);
            }
        }

        /// <summary>
        /// �������U���ɐ؂�ւ�
        /// </summary>
        public void LongAttack()
        {
            if (currentState == EnemyState.Discover)
            {
                animator.SetTrigger(isLongAttack);
                currentState = EnemyState.LongAttack;
            }
        }

        /// <summary>
        /// ���G�͈͊O�ɂł��Ƃ��̏���
        /// </summary>
        public void SetIdleState()
        {
            speed = 0;
            currentState = EnemyState.Idle;
            animator.SetTrigger(isLost);
            animator.SetFloat(speedId, 0.0f);
        }

        /// <summary>
        /// ���G�͈͓��ɓ��������̏���
        /// </summary>
        public void SetDiscoverState()
        {
            currentState = EnemyState.Discover;
            speed = chasespeed;
        }

        /// <summary>
        /// Move�X�e�[�g�ɕύX
        /// </summary>
        public void SetMoveState()
        {
            currentState = EnemyState.Move;
            speed = chasespeed;
        }

        /// <summary>
        /// �U���͈͓��ɓ��������ɃX�e�[�g���U�������ɐ؂�ւ�
        /// </summary>
        public void SetAttackReadyState()
        {
            if (!isDead)
            {
                currentState = EnemyState.AttackReady;
                speed = attackreadyspeed;                          //�U���͈͂ɓ�������l�q���ňړ����x������������
                animator.SetTrigger(isAttackReady);
                timetoattackreset = timetoattack;                       ////�U���܂ł̎��Ԃ̃J�E���g�����Z�b�g
            }
        }

        /// <summary>
        /// �����_���ɍU������B�U�����͈ړ����x��0�ɐݒ�
        /// </summary>
        public void Attacks()
        {
            float tmp = Random.Range(1.0f, 4.0f);              //1�`�U����ސ��̗������擾
            int random = (int)tmp;                             //float�^�̗�����int�^�ɃL���X�g
                                                               
            if (!isDead)
            {
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
            }
            rigidbody.velocity = Vector3.zero;                       //�����~�܂�
            timetoattackreset = timetoattack;                          //�U���܂ł̎��Ԃ̃J�E���g�����Z�b�g
        }

        /// <summary>
        /// �U�����Move��ԂɈڍs
        /// </summary>
        public void AfterAttack()
        {
            SetMoveState();
        }

        /// <summary>
        /// ���̓����蔻���ON�ɂ���֐�
        /// </summary>
        public void SetColliderOn(Collider collider)
        {
            SE.Instance.PlaySound(swing);
            // ���̋O�ՃG�t�F�N�g��\��
            swordeffect.SetActive(true);
            collider.enabled = true;
        }

        /// <summary>
        /// ���̓����蔻���OFF�ɂ���֐�
        /// </summary>
        public void SetColliderOff(Collider collider)
        {
            // ���̋O�ՃG�t�F�N�g���\��
            swordeffect.SetActive(false);
            collider.enabled = false;
        }

        /// <summary>
        /// ����������Ԃ̏���
        /// </summary>
        void UpdateForDiscover()
        {
            UpdateForMove();
        }

        /// <summary>
        /// �ҋ@��Ԃ̏���
        /// </summary>
        void UpdateForIdle()
        {
            Vector3 vec = Vector3.zero;
            rigidbody.velocity = transform.forward * speed;
        }

        /// <summary>
        /// �v���C��[�Ɍ������ē�������
        /// </summary>
        void UpdateForMove()
        {
            if (!isDead)
            {
                //�^�[�Q�b�g�����̃x�N�g�������߂�
                Vector3 vec = target.position - transform.position;
                
                // �^�[�Q�b�g�̕���������
                // Quaternion(��]�l)���擾
                Quaternion quaternion = Quaternion.LookRotation(vec);
                // �Z�o������]�l�����̃Q�[���I�u�W�F�N�g��rotation�ɑ��
                transform.rotation = quaternion;
                rigidbody.velocity = transform.forward * speed;// ���ʕ����Ɉړ�

                if (currentState == EnemyState.Discover && spd <= 2.00f || currentState == EnemyState.Move && spd <= 2.00f)
                {
                    spd += sp * Time.deltaTime;
                }

                if (speed <= chasespeed)
                {
                    speed += (chasespeed * Time.deltaTime) / 2;
                }
                animator.SetFloat(speedId, spd);

                if (!isAttacks && isLongAttacks)
                {
                    cnt += Time.deltaTime;
                    if (cnt > 3)
                    {
                        cnt = 0;
                        LongAttack();
                    }
                }
            }
        }

        /// <summary>
        /// �U���͈͂ɂƂǂ܂��Ă��鎞�Ԃ��J�E���g���Ĉ�莞�Ԃ𒴂�����AttackState�ɐ؂�ւ���
        /// </summary>
        void UpdateForAttackReady()
        {
            //�ړ����x��0�ɂ��v���C���[�̌����ɉ�]����B
            //rigidbody.velocity = Vector3.zero;
            Rotate();
            timetoattackreset -= Time.deltaTime;

            if (0 > timetoattackreset && isAttacks)                               //�U���܂ł̎��Ԃ�0�ɂȂ�΃X�e�[�g�J�ځB�J�E���g�����Z�b�g����B
            {
                //�����_���ȍU��
                Attacks();
            }
        }

        /// <summary>
        /// �v���C���[�̕����ɉ�]����֐�
        /// </summary>
        void Rotate()
        {
            Quaternion rot = this.transform.rotation;
            //�^�[�Q�b�g�����̃x�N�g�������߂�
            Vector3 vec = target.position - transform.position;
            // �^�[�Q�b�g�̕���������
            // Quaternion(��]�l)���擾
            Quaternion quaternion = Quaternion.LookRotation(vec);
            // �Z�o������]�l�����̃Q�[���I�u�W�F�N�g��rotation�ɑ��
            rot = quaternion;
            rot.x = 0;
            this.transform.rotation = rot;
        }

        /// <summary>
        /// Attack�X�e�[�g���̏���
        /// </summary>
        void UpdateForAttack()
        {
            var stateInfo = animator.GetCurrentAnimatorStateInfo(baseLayerIndex);
            if (stateInfo.fullPathHash == LocomotionHash)
            {
                SetAttackReadyState();
            }
            Rotate();
        }

        /// <summary>
        /// ���S��ԂɈڍs
        /// </summary>
        void SetDeadState()
        {
            currentState = EnemyState.Dead;
            speed = 0;
            rigidbody.mass = 100;
            animator.SetTrigger(DeadId);
        }

        /// <summary>
        /// �G�̉������U���̃v���n�u�̐���
        /// </summary>
        public void EnemyShotAttack()
        {
            SE.Instance.PlaySound(fire);
            GameObject shell = Instantiate(shellprefab, enemy_l_hand.transform.position, Quaternion.identity);
            Rigidbody shellRb = shell.GetComponent<Rigidbody>();
            // �e����ݒ�
            shellRb.AddForce(transform.forward * 1500);
            Destroy(shell, 1.0f);
        }

        /// <summary>
        /// �G��HP�o�[�̏���
        /// </summary>
        /// <param name="n"></param>
        public void EnemyDamage(float n)
        {
            if (currentState != EnemyState.Dead)
            {
                StageScene.Instance.enemyHp -= n;
                n /= 100.0f;
                hp.fillAmount += n;
                SE.Instance.PlaySound(chargedamaged);
                GameObject Hit = Instantiate(HitEffect, this.transform.position, Quaternion.identity);
                Destroy(Hit, 1.5f);

                //HP0�̂Ƃ����j�G�t�F�N�g�̐����ƓG�I�u�W�F�N�g�̍폜
                if (StageScene.Instance.enemyHp <= 0)
                {
                    isDead = true;
                    SetDeadState();
                    GameObject defeat = Instantiate(defeateffect, this.transform.position, Quaternion.identity);
                    Destroy(this.gameObject, deleteenemytime);
                }
            }
        }
    }
}







