using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Threading.Tasks;

namespace OROCHI
{
	public class UI : MonoBehaviour
	{
		[SerializeField]
        [Tooltip("�|�[�Y���Q��")]
		private GameObject pauseUI = null;

		[SerializeField]
		[Tooltip("�I�v�V�������Q��")]
		private GameObject optionUI = null;

		[SerializeField]
		[Tooltip("�K�C�h���Q��")]
		private GameObject GuideUI = null;

		[SerializeField]
		[Tooltip("�Q�[���I�[�o�[���Q��")]
		private GameObject GameOverUI = null;

		[SerializeField]
		[Tooltip("�X�e�[�W�N���A���Q��")]
		private GameObject StageClearUI = null;

		[SerializeField]
		[Tooltip("SE���Q��")]
		private SE Se = null;

		[SerializeField]
		[Tooltip("�v�����[�OUI���Q��")]
		private GameObject PrologueUI = null;

		[SerializeField]
		[Tooltip("�v�����[�O�őI�������{�^�����w��")]
		private Selectable NextButton = null;

		[SerializeField]
		[Tooltip("�񕜂̐���UI���Q��")]
		private GameObject healExplanation = null;

		[SerializeField]
		[Tooltip("�񕜐����̖߂�{�^���̃I�u�W�F�N�g���w��")]
		private GameObject closeButton = null;

		[SerializeField]
		[Tooltip("�񕜐������\�����ꂽ�Ƃ��ɑI�������{�^��")]
		private Selectable closeTutorialButton = null;

		[SerializeField]
		[Tooltip("pause���̏����I����Ԃɐݒ肷��{�^�����w��")]
		private Selectable FastButton = null;

		[SerializeField]
		[Tooltip("Option���J���ꂽ���ɑI�������{�^�����w��")]
		private Selectable OptionButton = null;

		[SerializeField]
		[Tooltip("Guide���J���ꂽ���ɑI�������{�^�����w��")]
		private Selectable GuideButton = null;

		[SerializeField]
		[Tooltip("GameOver���J���ꂽ���ɑI�������{�^��")]
		private Selectable GameOverButton = null;

		[SerializeField]
		[Tooltip("StageClear���J���ꂽ���ɑI�������{�^��")]
		private Selectable StageClearButton = null;

		[SerializeField]
		[Tooltip("�`���[�g���A���̃R���g���[���[���\�����ꂽ�Ƃ��ɑI�������{�^��")]
		private Selectable backTutorialButton = null;

		[SerializeField]
		[Tooltip("HP�o�[�̃T�C�Y�ʐ���")]
		private GameObject[] Size = null;

		[SerializeField]
		[Tooltip("�`���[�g���A���̃R���g���[���[image���w��")]
		private GameObject controller = null;

		[SerializeField]
		[Tooltip("�`���[�g���A���̖߂�{�^���̃I�u�W�F�N�g���w��")]
		private GameObject backButton = null;

		Animator animator;

		public GameObject Player;

        [Tooltip("�N�[���^�C���p��UI")]
		public Image Formicon;

        [Tooltip("�ϐg���\�����肷��t���O")]
		public bool Changeable;

        [SerializeField]
        [Tooltip("�ϐg�̃N�[���^�C��")]
		public float CountTime = 5.0f;

		// �J�n����UI���\��
		void Start()
		{
			pauseUI.SetActive(false);
			optionUI.SetActive(false);
			GuideUI.SetActive(false);
			GameOverUI.SetActive(false);
			StageClearUI.SetActive(false);

			if(NextButton != null)
            {
				NextButton.Select();
			}

			if(controller != null)
            {
				HiddenController();
            }

			if(healExplanation != null)
            {
				HiddenHealExplanation();
            }

			Changeable = true;
			animator = GetComponent<Animator>();
		}

        #region �|�[�Y��ʂ�\��
        public void Control()
		{
			if (!pauseUI.activeSelf)
			{
				animator.SetTrigger("PauseIn");
				// �|�[�Y�̕\��
				pauseUI.SetActive(true);
				// �߂�{�^����I��
				FastButton.Select();
				// UI���J���ꂽ�������Đ�
				Se.OpenUI();
				// ��~
				Time.timeScale = 0f;

			}
			else if (pauseUI.activeSelf && !optionUI.activeSelf && !GuideUI.activeSelf)
			{
				// �|�[�Y�̔�\��
				pauseUI.SetActive(false);

				animator.SetTrigger("PauseOut");
				// UI������ꂽ�������Đ�
				Se.CloseUI();
				// �ĊJ
				Time.timeScale = 1f;
			}
			else if (pauseUI.activeSelf && optionUI.activeSelf)
			{
				// option�̔�\��
				optionUI.SetActive(false);
				// UI������ꂽ�������Đ�
				Se.CloseUI();
				FastButton.Select();

				animator.SetTrigger("Show");
			}
			else if (pauseUI.activeSelf && GuideUI.activeSelf)
			{
				// Guide�̔�\��
				GuideUI.SetActive(false);
				// UI������ꂽ�������Đ�
				Se.CloseUI();
				FastButton.Select();

				animator.SetTrigger("Show");
			}
		}
        #endregion

        #region �I�v�V������ʂ̕\��
        /// <summary>
		/// Option�{�^���������ꂽ����Option��ʂ̕\��
		/// </summary>
        public void Option()
		{
			if (pauseUI.activeInHierarchy)
			{
				optionUI.SetActive(true);
				// UI���J���ꂽ�������Đ�
				Se.OpenUI();
				// Option���I�����ꂽ�ꍇ�ɂ��̃{�^����I����Ԃɂ��܂�
				OptionButton.Select();

				animator.SetTrigger("Hide");
			}
		}
        #endregion

        #region ���������ʂ�\��
        /// <summary>
		/// ��������������ꂽ���ɑ��������ʂ�\��
		/// </summary>
        public void Guide()
		{
			if (pauseUI.activeInHierarchy)
			{
				GuideUI.SetActive(true);
				// UI���J���ꂽ�������Đ�
				Se.OpenUI();
				// Guide���I�����ꂽ�ꍇ�ɂ��̃{�^����I����Ԃɂ��܂�
				GuideButton.Select();

				animator.SetTrigger("Hide");
			}
		}
        #endregion

        #region �Q�[���I�[�o�[��ʂ�\��
        /// <summary>
		/// GmaeOver���Ăяo���ꂽ��\������
		/// </summary>
        public void GameOver()
		{
			if (!GameOverUI.activeSelf)
			{
				animator.SetTrigger("GameOver");
				GameOverButton.Select();
			}
		}

		/// <summary>
		/// �X�e�[�W�N���A���̏���
		/// </summary>
		public void StageClear()
		{
			animator.SetTrigger("StageClear");
			StageClearButton.Select();
		}

		/// <summary>
		/// �X�e�[�W���g���C���̏���
		/// </summary>
		public void Retry()
		{
			animator.SetTrigger("Transition");
			SceneManager.LoadScene("Stage");
		}
		#endregion

		/// <summary>
		/// Home�������ꂽ��scene��Title�ֈڍs����
		/// </summary>
		public void Home()
		{
			animator.SetTrigger("Transition");
			SceneManager.LoadScene("Title");
			// �ĊJ
			Time.timeScale = 1f;
		}

		/// <summary>
		/// �v�����[�O���I�����鏈��
		/// </summary>
		public async void ClosePrologue()
        {
			PrologueUI.SetActive(false);

			// 2�b��Ɏ��s
			await Task.Delay(2000);
			DisplayController();
        }

		#region �v���C���[�̑傫���ʂɕ\��
		/// <summary>
		/// �v���C���[�̑傫���ɊY�����鐔����\��
		/// </summary>
		/// <param name="n">�\���������傫���ɊY�����鐔��</param>
		public void ChangeNumber(int n)
		{
			// ���������
			if (n == 0)
			{
				Size[0].SetActive(true);
				Size[1].SetActive(false);
				Size[2].SetActive(false);

			}
			// �ʏ펞�̏��
			else if (n == 1)
			{
				Size[0].SetActive(false);
				Size[1].SetActive(true);
				Size[2].SetActive(false);
			}
			// �傫�����̏��
			else if (n == 2)
			{
				Size[0].SetActive(false);
				Size[1].SetActive(false);
				Size[2].SetActive(true);
			}
		}
		#endregion

		/// <summary>
		/// �N�[���^�C����؂�ւ�
		/// </summary>
		public void ChangeCooltime()
		{
			if (Changeable)
			{
				Formicon.fillAmount = 0;
				StartCoroutine(loop());
			}
		}

		private IEnumerator loop()
        {
			while(Formicon.fillAmount < 1)
            {
				yield return null;
				Formicon.fillAmount += 1.0f / CountTime * Time.deltaTime;
			}
		}

		/// <summary>
		/// ���������ʂ̕\��
		/// </summary>
		private void DisplayController()
        {
			backTutorialButton.Select();
			controller.SetActive(true);
			backButton.SetActive(true);
        }

		/// <summary>
		/// ���������ʂ̔�\��
		/// </summary>
		private void HiddenController()
        {
			controller.SetActive(false);
			backButton.SetActive(false);
		}

		/// <summary>
		/// �񕜐����̕\��
		/// </summary>
		public void DisplayHealExplanation()
        {
			closeTutorialButton.Select();
			healExplanation.SetActive(true);
			closeButton.SetActive(true);
			StageScene.Instance.prologue = true;
        }

		/// <summary>
		/// �񕜐����̔�\��
		/// </summary>
		private void HiddenHealExplanation()
        {
			healExplanation.SetActive(false);
			closeButton.SetActive(false);
		}

		/// <summary>
		/// �`���[�g���A���I���̏���
		/// </summary>
		public void FinishTutorial()
        {
			HiddenController();
			HiddenHealExplanation();
			StageScene.Instance.prologue = false;
		}
	}
}