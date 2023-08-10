using System.Collections;
using UnityEngine;
using Cinemachine;
using System.Threading.Tasks;

namespace OROCHI
{
    // �ړ�����L�����N�^�[�𐧌䂵�܂��B
    public class MoveBehaviourScript : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("��E���E���̓����X�s�[�h���w��")]
        private float LARGEspeed, MEDIUMspeed, SMALLspeed;

        [SerializeField]
        [Tooltip("��E���E���̃W�����v�͂��w��")]
        private float LARGEup, MEDIUMup, SMALLup;

        [SerializeField]
        [Tooltip("�咆���̂��ꂼ��̃I�u�W�F�N�g���w�肵�܂��B")]
        private GameObject[] bodies = null;
        [SerializeField]
        private GameObject[] attackareas = null;

        [SerializeField]
        [Tooltip("��E���E���̎���")]
        public float[] mass = null;

        // �v���C���[�̑傫���X�e�[�g
        enum BodySize
        {
            Small,
            Medium,
            Large,
        }

        // ���݂̃L�����N�^�[�T�C�Y
        BodySize currentBodySize = BodySize.Medium;

        [SerializeField]
        [Tooltip("����̕����w��(�ォ�珬�E���E��)")]
        private float[] AvoidPower = new float[3];

        // avoidPower�����ƂɎ��ۂɉ���ɓ�����
        private float AvoidSpeed;

        // ��𒆂��ǂ����̃t���O
        private bool Avoiding = false;

        // �������
        private float avoidTime;

        // �A�j���[�V�����p�̈ړ����͒l
        private float walkSpeed;

        [SerializeField]
        [Tooltip("�΋��̃v���n�u�̎擾")]
        private GameObject shellPrefab;

        [SerializeField]
        [Tooltip("�J�����̐؂�ւ�")]
        private CinemachineVirtualCamera[] VirtualCamera = null;

        [SerializeField]
        [Tooltip("�I���`���f���̓���")]
        private Transform Orochihead = null;

        [SerializeField]
        [Tooltip("�ϐg�̃N�[���^�C�����w��")]
        float ChangeCoolTime = 5;

        // ���ۂ̃N�[���^�C��
        float CoolTime;

        [SerializeField]
        [Tooltip("�΋��̃N�[���^�C�����w��")]
        float shotCoolTime = 0;

        // ���ۂ̃N�[���^�C��
        float ShotCoolTime;

        // �`�F���W���\�ɂ���g���K�[
        public bool isChange = false;
        // �΋��𔭎˂ł���g���K�[
        public bool shot = false;

        // Animator�̃p�����[�^�[ID
        static readonly int isAvoidId = Animator.StringToHash("isAvoid");

        // ���݂�Animator(�咆���̂����ꂩ)
        Animator currentAnimator = null;

        // �U���{�^���������Ă��邩�ǂ����̃t���O
        private bool ButtonEnabled = true;

        [SerializeField]
        [Tooltip("UI�̎w��")]
        public UI ui = null;

        [Tooltip("Avatar�I�u�W�F�N�g�ւ̎Q��")]
        public GameObject avatar = null;

        [Tooltip("Rigidbody�̎Q��")]
        public BoxCollider boxCol;

        [SerializeField]
        [Tooltip("�񕜃G�t�F�N�g�̎w��")]
        public GameObject HealObject;

        [SerializeField]
        [Tooltip("�����G�t�F�N�g�̎w��")]
        public GameObject RunEffect;

        [SerializeField]
        [Tooltip("�Ђ������G�t�F�N�g�̎w��")]
        public GameObject ClawEffect;

        [SerializeField]
        [Tooltip("�����Ȃ��ǂɓ������Ă��鎞�̃G�t�F�N�g�̎w��")]
        public GameObject WallEffect;

        [SerializeField]
        [Tooltip("�T�C�Y�ύX�G�t�F�N�g�̎w��")]
        public GameObject ChangeEffect;

        // �Ђ������G�t�F�N�g�̃I�u�W�F�N�g
        GameObject claw;

        // �ϐg�G�t�F�N�g�̌���
        Quaternion EffectAngle = Quaternion.Euler(-90f, 0f, 0f);

        // �v���C���[�̏�Ԃ�\���܂�
        enum PlayerState
        {
            // ����
            Walk,
            // �W�����v��
            Jumping,
            // ���
            Avoid,
            // �U��
            Attack,
            // �Q�[���I�[�o�[
            Dead,
            // ���G
            Invincible,
            // �N���A��
            Clear,
        }
        // ���݂̃v���C���[�̏��
        PlayerState currentState = PlayerState.Walk;


        [SerializeField]
        [Tooltip("�ڒn����")]
        private bool isGrounded = false;

        [SerializeField]
        [Tooltip("��_���[�W�G�t�F�N�g�i�Ήԁj")]
        private GameObject damagefire;

        [SerializeField]
        [Tooltip("��_���[�W�G�t�F�N�g")]
        private GameObject damaged;

        // �R���|�[�l���g�����O�ɎQ�Ƃ��Ă����ϐ�
        new Rigidbody rigidbody;

        // ���̃X�s�[�h�ƃW�����v��
        private float speed = 5.0f;
        private float upforce = 100f;

        // Start is called before the first frame update
        void Start()
        {
            Time.timeScale = 1;
            isGrounded = true;
            isChange = true;
            shot = true;
            rigidbody = GetComponent<Rigidbody>();
            rigidbody.mass = 10;
            boxCol = GetComponent<BoxCollider>();

            // ���߂͕��ʂ̏��
            bodies[0].SetActive(false);
            bodies[1].SetActive(true);
            bodies[2].SetActive(false);
            attackareas[0].SetActive(false);
            attackareas[1].SetActive(false);
            attackareas[2].SetActive(false);
            currentAnimator = bodies[1].GetComponent<Animator>();
            currentBodySize = BodySize.Medium;

            VirtualCamera[1].Priority = 100;
            CoolTime = ChangeCoolTime;
            ShotCoolTime = shotCoolTime;

            currentAnimator.SetBool("isWalk", false);

            boxCol.center = new Vector3(0f, 1f, 0f);
            boxCol.size = new Vector3(1.5f, 2f, 3f);
        }



        // Update is called once per frame
        void Update()
        {
            switch (currentState)
            {
                case PlayerState.Walk:
                    UpdateForWalkState();
                    break;
                case PlayerState.Jumping:
                    UpdateForJumpingState();
                    break;
                case PlayerState.Avoid:
                    break;
                case PlayerState.Attack:
                    break;
                case PlayerState.Clear:
                    UpdateForClearState();
                    break;
                case PlayerState.Invincible:
                    break;
            }

            // �N�[���^�C���̌o�ߎ���
            if (CoolTime >= 0)
            {
                CoolTime -= Time.deltaTime;
            }
            if (CoolTime < 0)
            {
                isChange = true;
            }

            // �΋��̃N�[���^�C���̌o�ߎ���
            if (ShotCoolTime >= 0)
            {
                ShotCoolTime -= Time.deltaTime;
            }
            if (ShotCoolTime < 0)
            {
                shot = true;
            }

            // HP��0�̎�
            if (StageScene.Instance.playerhp == 0)
            {
                SetDeadState();
            }
        }

        void UpdateForWalkState()
        {
            switch (currentBodySize)
            {
                case BodySize.Small:
                    speed = SMALLspeed;
                    break;
                case BodySize.Medium:
                    speed = MEDIUMspeed;
                    break;
                case BodySize.Large:
                    speed = LARGEspeed;
                    break;
                default:
                    break;
            }
            RunEffect.SetActive(true);
        }

        /// <summary>
        /// �W�����v�����̏���
        /// </summary>
        void UpdateForJumpingState()
        {
            speed = 5;
            RunEffect.SetActive(false);
        }

        /// <summary>
        /// �N���A���̏���
        /// </summary>
        void UpdateForClearState()
        {
            currentAnimator.SetBool("isWalk", false);
        }

        // Walk�X�e�[�g�ɑJ�ڂ����܂��B
        public void SetWalkState()
        {
            if (currentBodySize == BodySize.Small)
            {
                boxCol.center = new Vector3(0f, 1f, 0f);
                boxCol.size = new Vector3(1f, 2f, 1.5f);
            }
            else if (currentBodySize == BodySize.Medium)
            {
                boxCol.center = new Vector3(0f, 1f, 0f);
                boxCol.size = new Vector3(1.5f, 2f, 3f);
            }
            else if (currentBodySize == BodySize.Large)
            {
                boxCol.center = new Vector3(0f, 1.8f, 0f);
                boxCol.size = new Vector3(2f, 3.6f, 4.5f);
            }

            attackareas[0].SetActive(false);
            attackareas[1].SetActive(false);
            attackareas[2].SetActive(false);

            currentState = PlayerState.Walk;
        }

        #region �X�e�[�g���ɑJ�ڂ�����
        // Jumping�X�e�[�g�ɑJ�ڂ����܂��B
        public void SetJumpingState()
        {
            currentState = PlayerState.Jumping;
        }

        // Attack�X�e�[�g�ɑJ�ڂ����܂��B
        public void SetAttackState()
        {
            currentState = PlayerState.Attack;
        }

        public void SetDeadState()
        {
            currentState = PlayerState.Dead;
            Time.timeScale = 0;
        }

        public void SetInvincible()
        {
            currentState = PlayerState.Invincible;
        }

        public void SetClearState()
        {
            // �N���A���o��ɓ������~�߂�
            currentAnimator.SetFloat("WalkSpeed", 0);
            currentState = PlayerState.Clear;
        }
        #endregion

        // �w�肵�������ֈړ����܂��B
        public void Move(Vector3 motion)
        {
            // Walk��Jumping�̎�����
            if (currentState == PlayerState.Walk || currentState == PlayerState.Jumping)
            {
                // �v���C���[�̑O�㍶�E�̈ړ�
                var velocity = motion;
                // �n����s�L�����N�^�[��W���Ƃ���̂�y���W�ړ��͖���
                velocity.y = 0;

                // ���͒l�̑傫�������A�j���[�V�����̃X�s�[�h�ɑ��
                walkSpeed = Mathf.Max(Mathf.Abs(velocity.x),Mathf.Abs(velocity.z));

                if (velocity.sqrMagnitude >= 0.0001f)
                {
                    // �v���C���[�̌�������]
                    avatar.transform.LookAt(transform.position + velocity.normalized, Vector3.up);
                    velocity *= speed;
                    // �����A�j���[�V�������Đ�
                    currentAnimator.SetBool("isWalk", true);
                }
                else if (velocity.sqrMagnitude <= 0)
                {
                    // �����A�j���[�V�����̒�~
                    currentAnimator.SetBool("isWalk", false);
                }

                currentAnimator.SetFloat("WalkSpeed", walkSpeed);

                // velocity�Ɉړ��ʂ���
                velocity.y = rigidbody.velocity.y;
                rigidbody.velocity = velocity;
            }
        }

        /// <summary>
        /// ����s��
        /// </summary>
        public void Avoid()
        {
            if (!Avoiding)
            {
                Avoiding = true;
                currentAnimator.SetTrigger(isAvoidId);
                // ���G���
                SetInvincible();
               
                // �R���C�_�[�����������Ă��蔲����\��
                boxCol.center = new Vector3(0, 0.25f, 0);
                boxCol.size = new Vector3(1f, 0.5f, 1f);

                if (currentBodySize == BodySize.Small)
                {
                    AvoidSpeed = AvoidPower[0];
                    avoidTime = 0.3f;
                }
                else if(currentBodySize == BodySize.Medium)
                {
                    AvoidSpeed = AvoidPower[1];
                    avoidTime = 0.5f;
                }
                else if(currentBodySize == BodySize.Large)
                {
                    AvoidSpeed = AvoidPower[2];
                    avoidTime = 0.8f;
                }

                // 
                StartCoroutine(DelayCoroutine(avoidTime));
                rigidbody.AddForce(avatar.transform.forward * AvoidSpeed, ForceMode.Impulse);
            }
        }


        /// <summary>
        /// �W�����v����
        /// </summary>
        public void Jump()
        {
            if (isGrounded == true)
            {
                if (currentState != PlayerState.Clear)
                {
                    // space�������ꂽ��W�����v
                    rigidbody.AddForce(transform.up * upforce / 2, ForceMode.Impulse);
                    isGrounded = false;

                    currentAnimator.SetTrigger("isJump");

                    SetJumpingState();
                }
            }
        }


        /// <summary>
        /// �U������
        /// </summary>
        public async void Fire()
        {
            // �ڒn��ԂȂ�
            if (isGrounded == true)
            {
                // �U���{�^���������ĂȂ����
                if(ButtonEnabled == true)
                {
                    if (currentState != PlayerState.Clear)
                    {
                        // �U����ԂɈڍs
                        SetAttackState();

                        currentAnimator.SetTrigger("isAttack");

                        ButtonEnabled = false;

                        // �U�����ɏ����O�i
                        rigidbody.AddForce(avatar.transform.forward * 10, ForceMode.Impulse);
                        if (currentBodySize == BodySize.Large)
                        {
                            // 0.7�b��
                            await Task.Delay(700);

                            // �Ђ������G�t�F�N�g�𐶐�
                            claw = Instantiate(ClawEffect, attackareas[2].transform.position, attackareas[2].transform.rotation);
                            claw.transform.localScale = new Vector3(2.5f, 1f, 1f);

                            // �傫�����̍U���͈͂��A�N�e�B�u��
                            attackareas[0].SetActive(false);
                            attackareas[1].SetActive(false);
                            attackareas[2].SetActive(true);
                        }
                        else if (currentBodySize == BodySize.Medium)
                        {
                            // 0.7�b��
                            await Task.Delay(700);

                            // �Ђ������G�t�F�N�g�𐶐�
                            claw = Instantiate(ClawEffect, attackareas[1].transform.position, attackareas[1].transform.rotation); 

                            // �ʏ펞�̍U���͈͂��A�N�e�B�u��
                            attackareas[0].SetActive(false);
                            attackareas[1].SetActive(true);
                            attackareas[2].SetActive(false);
                        }
                        else if (currentBodySize == BodySize.Small)
                        {
                            // 0.3�b��
                            await Task.Delay(300);

                            // �Ђ������G�t�F�N�g�𐶐�
                            claw = Instantiate(ClawEffect, attackareas[0].transform.position, attackareas[0].transform.rotation);

                            // ���������̍U���͈͂��A�N�e�B�u��
                            attackareas[0].SetActive(true);
                            attackareas[1].SetActive(false);
                            attackareas[2].SetActive(false);
                        }

                        // �U���̌㏈��
                        StartCoroutine(ButtonCoroutine());
                    }

                }
            }
        }

        /// <summary>
        /// �΋��U��
        /// </summary>
        public async void ShotAttack()
        {
            if (shot == true)
            {
                // �傫����Ԃ̂�
                if (currentBodySize == BodySize.Large)
                {
                    currentAnimator.SetTrigger("isBeam");
                    shot = false;
                    // �΋��̃N�[���^�C�������Z�b�g
                    ShotCoolTime = shotCoolTime;
                    await Task.Delay(800);
                    // �A�j���[�V�����ɍ��킹�ĉ����Đ�
                    SE.Instance.FireSE();
                    // �΋��I�u�W�F�N�g�𐶐�
                    GameObject shell = Instantiate(shellPrefab, Orochihead.transform.position, Quaternion.identity);
                    Rigidbody shellRb = shell.GetComponent<Rigidbody>();
                    // �e����ݒ�
                    shellRb.AddForce(Orochihead.transform.forward * 1500);
                    // 1�b��ɔj��
                    Destroy(shell, 1.0f);
                }
            }
        }

        // �Փ˔���
        void OnTriggerEnter(Collider collision)
        {
            if (currentState == PlayerState.Walk || currentState == PlayerState.Jumping || currentState == PlayerState.Attack)
            {
                // �G�̕���ɓ���������
                if (collision.CompareTag("Enemy_Weapon"))
                {
                    SE.Instance.Damaged();
                    // ��_���G�t�F�N�g�𐶐�
                    GameObject effectplay = Instantiate(damaged, this.transform.position, Quaternion.identity);
                    Destroy(effectplay, 1.5f);
                    StageScene.Instance.Damage();
                }
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            // �ڒn����
            if (collision.gameObject.tag == "ground")
            {
                isGrounded = true;
                if(currentState != PlayerState.Clear)
                {
                    SetWalkState();
                }
            }

            if (currentState == PlayerState.Walk || currentState == PlayerState.Jumping || currentState == PlayerState.Attack)
            {
                // �G�̉΋��ɓ���������
                if (collision.gameObject.tag == "Fire")
                {
                    StageScene.Instance.Damage();
                    // ��_���G�t�F�N�g�𐶐�
                    GameObject fire = Instantiate(damagefire, this.transform.position, Quaternion.identity);
                    Destroy(fire, 2.0f);
                }
            }

            // �����̕ǂɓ���������
            if (collision.gameObject.tag == "Wall")
            {
                // �Փ˂������W���擾
                Vector3 hitPos = collision.contacts[0].point;
                Vector3 effectVec = this.transform.position - hitPos;
                Vector3 rotation = Vector3.zero - hitPos;
                // �v���C���[�ƕǂ̊ԂŃG�t�F�N�g�̌����𒲐�
                Quaternion quaternion = Quaternion.LookRotation(rotation);
                // �ǂƃv���C���[�̊ԂɃG�t�F�N�g�𐶐�
                GameObject wallEffect = Instantiate(WallEffect, hitPos, quaternion);
                // 2�b��ɔj��
                Destroy(wallEffect, 2.0f);
            }
        }

        /// <summary>
        /// ��莞�Ԓx�点��
        /// </summary>
        /// <param name="time">�x�点��������</param>
        /// <returns></returns>
        public IEnumerator DelayCoroutine(float time)
        {
            // 1�b�ԑ҂�
            yield return new WaitForSeconds(time);
            Avoiding = false;
            SetWalkState();
        }

        /// <summary>
        /// �U���̌㏈��
        /// </summary>
        /// <returns></returns>
        public IEnumerator ButtonCoroutine()
        {
            // 0.4�b�҂�
            yield return new WaitForSeconds(0.4f);
            SetWalkState();
            // �U���{�^�����������ԂɕύX
            ButtonEnabled = true;
            // �Ђ������G�t�F�N�g��j��
            Destroy(claw, 2f);
        }


        #region �v���C���[�̑傫����ό`
        /// <summary>
        /// �傫�����
        /// </summary>
        public void Large()
        {
            //�ϐg�G�t�F�N�g
            Instantiate(ChangeEffect, this.transform.position, EffectAngle); //�p�[�e�B�N���p�Q�[���I�u�W�F�N�g����
            SE.Instance.Change();
            // �傫����Ԃ̃R���C�_�[�ɕύX
            boxCol.center = new Vector3(0f, 1.8f, 0f);
            boxCol.size = new Vector3(2f, 3.6f, 4.5f);
            // �W�����v�͂�ύX
            upforce = LARGEup;
            // �d�ʊ���ύX
            rigidbody.mass = mass[0];

            // �傫����Ԃ̃��f����\��
            bodies[0].SetActive(false);
            bodies[1].SetActive(false);
            bodies[2].SetActive(true);

            // �傫�����p�̃J�������g�p
            VirtualCamera[0].Priority = 10;
            VirtualCamera[1].Priority = 10;
            VirtualCamera[2].Priority = 100;

            // �傫����ԂɕύX
            currentBodySize = BodySize.Large;
            currentAnimator = bodies[2].GetComponent<Animator>();

            // UI�ő傫����Ԃ�\��
            ui.ChangeNumber(2);

            // �ϐg�p�̃N�[���^�C���������̐��l�Ƀ��Z�b�g
            ResetCoolTime();
        }

        /// <summary>
        /// �ʏ���
        /// </summary>
        public void Medium()
        {
            //�ϐg�G�t�F�N�g
            Instantiate(ChangeEffect, this.transform.position, EffectAngle); //�p�[�e�B�N���p�Q�[���I�u�W�F�N�g����
            SE.Instance.Change();

            // �ʏ��Ԃ̃R���C�_�[�ɕύX
            boxCol.center = new Vector3(0f, 1f, 0f);
            boxCol.size = new Vector3(1.5f, 2f, 3f);

            // �W�����v�͂�ύX
            upforce = MEDIUMup;
            // �d�ʊ���ύX
            rigidbody.mass = mass[1];

            // �ʏ��Ԃ̃��f����\��
            bodies[0].SetActive(false);
            bodies[1].SetActive(true);
            bodies[2].SetActive(false);

            // �ʏ��Ԃ̃J�������g�p
            VirtualCamera[0].Priority = 10;
            VirtualCamera[1].Priority = 100;
            VirtualCamera[2].Priority = 10;

            // �ʏ��ԂɕύX
            currentBodySize = BodySize.Medium;
            currentAnimator = bodies[1].GetComponent<Animator>();

            // UI�Œʏ��Ԃ�\��
            ui.ChangeNumber(1);

            // �ϐg�p�̃N�[���^�C���������̐��l�Ƀ��Z�b�g
            ResetCoolTime();
        }

        /// <summary>
        /// ���������
        /// </summary>
        public void Small()
        {
            Debug.Log("���^");

            //�ϐg�G�t�F�N�g
            Instantiate(ChangeEffect, this.transform.position, EffectAngle);
            SE.Instance.Change();

            // ��������Ԃ̃R���C�_�[�ɕύX
            boxCol.center = new Vector3(0f, 1f, 0f);
            boxCol.size = new Vector3(1f, 2f, 1.5f);

            // �W�����v�͂�ύX
            upforce = SMALLup;
            // �d�ʊ���ύX
            rigidbody.mass = mass[2];

            // ��������Ԃ̃��f����\��
            bodies[0].SetActive(true);
            bodies[1].SetActive(false);
            bodies[2].SetActive(false);

            // ��������Ԃ̃J�������g�p
            VirtualCamera[0].Priority = 100;
            VirtualCamera[1].Priority = 10;
            VirtualCamera[2].Priority = 10;

            // ��������ԂɕύX
            currentBodySize = BodySize.Small;
            currentAnimator = bodies[0].GetComponent<Animator>();

            // UI�ŏ�������Ԃ�\��
            ui.ChangeNumber(0);

            // �ϐg�p�̃N�[���^�C���������̐��l�Ƀ��Z�b�g
            ResetCoolTime();
        }
        #endregion

        /// <summary>
        /// ��i�K�傫���Ȃ鏈��
        /// </summary>
        public void BodyUp()
        {
            switch (currentBodySize)
            {
                case BodySize.Small:
                    StageScene.Instance.Change();
                    Medium();
                    break;
                case BodySize.Medium:
                    StageScene.Instance.Change();
                    Large();
                    break;
                case BodySize.Large:
                default:
                    break;
            }
        }

        /// <summary>
        /// ��i�K�������Ȃ鏈��
        /// </summary>
        public void BodyDown()
        {
            switch (currentBodySize)
            {
                case BodySize.Large:
                    StageScene.Instance.Change();
                    Medium();
                    break;
                case BodySize.Medium:
                    StageScene.Instance.Change();
                    Small();
                    break;
                case BodySize.Small:
                default:
                    break;
            }
        }

        /// <summary>
        /// �񕜒��̃G�t�F�N�g����
        /// </summary>
        public void Heal()
        {
            Debug.Log("��");
            Instantiate(HealObject, this.transform.position, Quaternion.identity);
        }

        /// <summary>
        /// �ϐg�̃N�[���^�C���̃��Z�b�g
        /// </summary>
        public void ResetCoolTime()
        {
            CoolTime = ChangeCoolTime;
            isChange = false;
        }
    }
}
