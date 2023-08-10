using UnityEngine;
using UnityEngine.EventSystems;
using System.Threading.Tasks;

namespace OROCHI
{
    public class StageScene : MonoBehaviour
    {
        // �C���X�^���X��
        public static StageScene Instance { get; private set; }

        [SerializeField]
        [Tooltip("UI�X�N���v�g���w��")]
        private UI ui = null;

        [Tooltip("�v���C���[��HP���w��")]
        public int playerhp;

        // �v���C���[�̗͍̑ő�l
        private int MaxHP = 6;

        [Tooltip("�񕜃G�t�F�N�g�̎w��")]
        public GameObject healObject;

        [Tooltip("�G��HP��ݒ�")]
        public float enemyHp;

        [SerializeField]
        [Tooltip("�v���C���[�I�u�W�F�N�g���w��")]
        public GameObject player;

        [SerializeField]
        [Tooltip("�X�T�m�I�I�u�W�F�N�g���w��")]
        public GameObject enemy;

        // �A�j���[�^�[
        private Animator animator;

        [Tooltip("�v�����[�O���̃t���O")]
        public bool prologue = true;

        // �N���A���̃t���O
        private bool clear = false;

        // �v�����[�O�p�̕ϐ�
        int page = 0;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        /// <summary>
        /// �|�[�Y��ʂ�\��
        /// </summary>
        public void ControlPauseUI()
        {
            if (Time.timeScale > 0)
            {
                ui.Control();
            }
        }

        private void Update()
        {
            // �v���C���[�̗̑͂�0�Ȃ�
            if (playerhp == 0)
            {
                ui.GameOver();
            }

            // �G�̗̑͂�0�Ȃ�
            if (enemyHp <= 0)
            {
                Clear();
            }
        }

        /// <summary>
        /// �v���C���[�̗̑͂��񕜂����鏈��
        /// </summary>
        /// <param name="EffectTransform">�񕜎��̃G�t�F�N�g�̌���</param>
        public void Heal(Vector3 EffectTransform)
        {
            // �񕜃G�t�F�N�g�𐶐�
            Instantiate(healObject, EffectTransform, Quaternion.identity); //�p�[�e�B�N���p�Q�[���I�u�W�F�N�g����
            if(playerhp < MaxHP)
            {
                playerhp += 1;
            }
        }

        /// <summary>
        /// Damage���Ăяo���ꂽ��v���C���[��HP��1����
        /// </summary>
        public void Damage()
        {
            if (playerhp > 0)
            {
                playerhp--;
            }
        }

        /// <summary>
        /// �v���C���[�̑傫����ω��������̏���
        /// </summary>
        public void Change()
        {
            ui.ChangeCooltime();
        }

        /// <summary>
        /// �v�����[�O�Ŏ��̃y�[�W��\������
        /// </summary>
        public void NextPage()
        {
            animator.SetTrigger("Next");

            if(page == 1)
            {
                // �v�����[�O���I���������
                ui.ClosePrologue();
            }
            page++;
        }

        /// <summary>
        /// �񕜂̐����pUI��\��
        /// </summary>
        public void OnHealExplanation()
        {
            ui.DisplayHealExplanation();
        }

        /// <summary>
        /// �G��|�������̏���
        /// </summary>
        public async void Clear()
        {
            if(!clear)
            {
                clear = true;
                // 5�b�Ԃ̃A�j���[�V�����̌�
                await Task.Delay(5000);
                ui.StageClear();
                player.GetComponent<MoveBehaviourScript>().SetClearState();
            }
        }

        /// <summary>
        /// �q�b�g�X�g�b�v��\��
        /// </summary>
        /// <param name="f">�q�b�g�X�g�b�v���̎��Ԃ̑���</param>
        /// <param name="i">�q�b�g�X�g�b�v���̎��Ԃ̒���</param>
        public async void HitStop(float f, int i)
        {
            Time.timeScale = f;
            await Task.Delay(i);
            Time.timeScale = 1.0f;
        }
    }
}
