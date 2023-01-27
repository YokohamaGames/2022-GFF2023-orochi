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
        //�G�̃X�e�[�^�X�Ɋւ��鐔�l�ݒ�

        //�G�̒ǐՎ����s�X�s�[�h��ݒ�
        [SerializeField] private float chasespeed;
        //�U���������̕��s�X�s�[�h��ݒ�
        [SerializeField] private float attackreadyspeed = 1;
        //�ǐՖڕW�̐ݒ�
        [SerializeField]
        private Transform target = null;
        //�A�j���[�^�[�̐ݒ�
        [SerializeField] private Animator animator = null;
        //�G�̉�]���x��ݒ肵�܂�
        [SerializeField]
        private float rotmax;
        //�q�I�u�W�F�N�g���擾
        [SerializeField]
        private SearchArea searcharea;
        //�q�I�u�W�F�N�g���擾
        [SerializeField]
        private AttackArea attackArea;
        //�G�̎�����̓����蔻����擾
        [SerializeField]
        private Collider weaponcollider;
        //�X�e�[�g�J�ڂ�x�点�鎞��
        [SerializeField]
        private float transition_time;
        //�΋��̃v���n�u�̎擾
        [SerializeField]
        private GameObject shellprefab;
        //�G���j�G�t�F�N�g
        [SerializeField]
        private GameObject defeateffect;
        //�_���[�W�G�t�F�N�g
        [SerializeField]
        private GameObject damageeffect;
        //�G�̍���̍��W���擾���܂�
        [SerializeField]
        private Transform enemy_l_hand;
        //�G��HpBar���Q��
        [SerializeField]
        public Slider enemyhpbar;
        //����Effect���擾
        [SerializeField]
        private GameObject swordeffect;
        [SerializeField]
        private GameObject HitEffect;
        //�U����������U���܂ł̎��Ԃ̐ݒ�
        [SerializeField] private float timetoattack = 2;
        //�G���j���̓G�̏��ł܂ł̎��Ԃ̐ݒ�
        [SerializeField] private float deleteenemytime;
        //�U���܂ł̑ҋ@���Ԃ�ݒ肵���l�Ƀ��Z�b�g����ϐ�
        float timetoattackreset;
        //HpBar�̎擾
        [SerializeField]
        private Image hp;
        [SerializeField]
        float timefire = 1.5f;
        float timetoatk = 0;

        public bool isSearch = false;
        public bool isAttacks = false;
        public bool isLongAttacks = false;
        public bool isDead = false;

        //���݂̓G�̕��s�X�s�[�h
        float speed = 0;
        // �R���|�[�l���g�����O�ɎQ�Ƃ��Ă����ϐ�
        new Rigidbody rigidbody;

        [Header("SE")]
        [SerializeField] private AudioClip fire;
        [SerializeField] private AudioClip swing;
        [SerializeField] private AudioClip chargedamaged;


        private AudioSource se;


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
            timetoattackreset = timetoattack;                  //�U�����Ԃ��w�肵�����ԂɃ��Z�b�g����ϐ��ɒl����
            weaponcollider.enabled = false;                    //�G�̕���̓����蔻����I�t
            swordeffect.SetActive(false);
            enemyhpbar.value = StageScene.Instance.EnemyHp;    // Slider�̏�����Ԃ�ݒ� 
            se = GetComponent<AudioSource>();
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
                    UpdateForDead();
                    break;
                default:
                    break;
            }
            Debug.Log(currentState);
        }

        IEnumerator Wait()
        {
            yield return null;
        }
        void UpdateForDead()
        {
            StartCoroutine(Wait());
        }
        void UpdateForLongAttack()
        {
            Rotate();
            timetoatk += Time.deltaTime;
            if (timetoatk > timefire)
            {
                timetoatk = 0;
                animator.SetTrigger(isLongAttack);
            }
        }

        //�������U���ɐ؂�ւ�
        public void LongAttack()
        {
            if (currentState == EnemyState.Discover)
            {
                animator.SetTrigger(isLongAttack);
                currentState = EnemyState.LongAttack;
            }
        }

        //���G�͈͊O�ɂł��Ƃ��̏���
        public void SetIdleState()
        {
            speed = 0;
            currentState = EnemyState.Idle;
            animator.SetTrigger(isLost);
            animator.SetFloat(speedId, 0.0f);
        }

        //���G�͈͓��ɓ��������̏���
        public void SetDiscoverState()
        {
            currentState = EnemyState.Discover;
            speed = chasespeed;
        }

        //Move�X�e�[�g�ɕύX
        public void SetMoveState()
        {
            currentState = EnemyState.Move;
            speed = chasespeed;
        }

        //�U���͈͓��ɓ��������ɃX�e�[�g���U�������ɐ؂�ւ�
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

        //�����_���ɍU������B�U�����͈ړ����x��0�ɐݒ�
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

        //�����蔻���ON�ɂ���֐�
        public void SetColliderOn(Collider collider)
        {
            SE.Instance.PlaySound(swing);
            swordeffect.SetActive(true);
            collider.enabled = true;
        }

        //�����蔻���OFF�ɂ���֐�
        public void SetColliderOff(Collider collider)
        {
            swordeffect.SetActive(false);
            collider.enabled = false;
        }

        void UpdateForDiscover()
        {
            UpdateForMove();
        }

        //�ҋ@��Ԃ̏���
        void UpdateForIdle()
        {
            Vector3 vec = Vector3.zero;
            rigidbody.velocity = transform.forward * speed;
        }

        float spd;
        float cnt = 0;
        //�v���C��[�Ɍ������ē�������
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

        //�U���͈͂ɂƂǂ܂��Ă��鎞�Ԃ��J�E���g���Ĉ�莞�Ԃ𒴂�����AttackState�ɐ؂�ւ���
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

        //�v���C���[�̕����ɉ�]����֐�
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
        }

        //Attack�X�e�[�g���̏���
        void UpdateForAttack()
        {
            var stateInfo = animator.GetCurrentAnimatorStateInfo(baseLayerIndex);
            if (stateInfo.fullPathHash == LocomotionHash)
            {
                SetAttackReadyState();
            }
            Rotate();
        }

        void SetDeadState()
        {
            currentState = EnemyState.Dead;
            speed = 0;
            rigidbody.mass = 100;
            animator.SetTrigger(DeadId);
        }

        //�G�̉������U���̃v���n�u�̐���
        public void EnemyShotAttack()
        {
            SE.Instance.PlaySound(fire);
            GameObject shell = Instantiate(shellprefab, enemy_l_hand.transform.position, Quaternion.identity);
            Rigidbody shellRb = shell.GetComponent<Rigidbody>();
            // �e����ݒ�
            shellRb.AddForce(transform.forward * 1500);
            Destroy(shell, 1.0f);
        }

        //�G��HP�o�[�̏���
        public void EnemyDamage(float n)
        {
            StageScene.Instance.EnemyHp -= n;
            n /= 100.0f;
            hp.fillAmount += n;
            SE.Instance.PlaySound(chargedamaged);
            GameObject Hit = Instantiate(HitEffect, this.transform.position, Quaternion.identity);
            Destroy(Hit, 1.5f);

            //HP0�̂Ƃ����j�G�t�F�N�g�̐����ƓG�I�u�W�F�N�g�̍폜
            if (StageScene.Instance.EnemyHp <= 0)
            {
                isDead = true;
                SetDeadState();
                GameObject defeat = Instantiate(defeateffect, this.transform.position, Quaternion.identity);
                Destroy(this.gameObject, deleteenemytime);
            }
        }
    }
}







