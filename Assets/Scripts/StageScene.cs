using UnityEngine;
using UnityEngine.EventSystems;
using System.Threading.Tasks;

namespace OROCHI
{
    public class StageScene : MonoBehaviour
    {
        public static StageScene Instance { get; private set; }

        // UI���w�肵�܂��B
        [SerializeField]
        private UI ui = null;

        // �v���C���[��HP���w��
        [SerializeField]
        public int playerhp;

        //�񕜃G�t�F�N�g�̎w��
        [SerializeField]
        public GameObject healObject;

        //�G��HP��ݒ�
        [SerializeField]
        public float enemyHp;

        [SerializeField]
        [Tooltip("�v���C���[�I�u�W�F�N�g���w��")]
        public GameObject player;

        // �v���C���[�̃|�W�V����
        private Vector3 playerPosition;

        [SerializeField]
        [Tooltip("�X�T�m�I�I�u�W�F�N�g���w��")]
        public GameObject enemy;

        // �X�T�m�I�̃|�W�V����
        private Vector3 enemyPosition;

        // �A�j���[�^�[
        private Animator animator;

        // �v�����[�O���̓I��
        public bool prologue = true;

        private bool clear = false;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        public void ControlPauseUI()
        {
            if (Time.timeScale > 0)
            {
                ui.Control();
            }
        }

        private void Update()
        {
            if (playerhp == 0)
            {
                ui.GameOver();
            }

            if (enemyHp <= 0)
            {
                Clear();
            }
        }

        private void FixedUpdate()
        {
            /*
            if(enemy != null)
            {
                playerPosition = player.GetComponent<Transform>().position;
                enemyPosition = enemy.GetComponent<Transform>().position;

                Vector3 difference = playerPosition - enemyPosition;
                float differenceX = difference.x;
                float differenceY = difference.y;

                //if(Mathf.Abs(differenceX) = )
            }
            */
        }

        public void Heal(Vector3 EffectTransform)
        {
            Instantiate(healObject, EffectTransform, Quaternion.identity); //�p�[�e�B�N���p�Q�[���I�u�W�F�N�g����
            if(playerhp < 6)
            {
                playerhp += 1;
            }
        }

        // Damage���Ăяo���ꂽ��HP��1����
        public void Damage()
        {
            if (playerhp > 0)
            {
                playerhp--;
            }
        }

        public void Change()
        {
            ui.ChangeCooltime();
        }

        int page = 0;

        public void NextPage()
        {
            animator.SetTrigger("Next");

            if(page == 1)
            {
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

        public async void Clear()
        {
            if(!clear)
            {
                clear = true;
                await Task.Delay(5000);
                ui.StageClear();
                player.GetComponent<MoveBehaviourScript>().SetClearState();
                Debug.Log("�N���A");
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
