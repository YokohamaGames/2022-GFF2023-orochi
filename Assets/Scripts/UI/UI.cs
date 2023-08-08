using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Threading.Tasks;

namespace OROCHI
{
	public class UI : MonoBehaviour
	{
		// �|�[�Y���Q��
		[SerializeField]
		private GameObject pauseUI = null;

		// �I�v�V�������Q��
		[SerializeField]
		private GameObject optionUI = null;

		// �K�C�h���Q��
		[SerializeField]
		private GameObject GuideUI = null;

		// �Q�[���I�[�o�[���Q��
		[SerializeField]
		private GameObject GameOverUI = null;

		// �X�e�[�W�N���A���Q��
		[SerializeField]
		private GameObject StageClearUI = null;

		// SE���Q��
		[SerializeField]
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

		// pause���̏����I����Ԃɐݒ肷��{�^�����w��
		[SerializeField]
		private Selectable FastButton = null;

		// Option���J���ꂽ���ɑI�������{�^�����w��
		[SerializeField]
		private Selectable OptionButton = null;

		// Guide���J���ꂽ���ɑI�������{�^�����w��
		[SerializeField]
		private Selectable GuideButton = null;

		// GameOver���J���ꂽ���ɑI�������{�^��
		[SerializeField]
		private Selectable GameOverButton = null;

		// StageClear���J���ꂽ���ɑI�������{�^��
		[SerializeField]
		private Selectable StageClearButton = null;

		[SerializeField]
		[Tooltip("�`���[�g���A���̃R���g���[���[���\�����ꂽ�Ƃ��ɑI�������{�^��")]
		private Selectable backTutorialButton = null;

		// HP�o�[�̃T�C�Y�ʐ���
		[SerializeField]
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
        // Option�{�^���������ꂽ����Option��ʂ̕\��
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
        // ��������������ꂽ���ɑ��������ʂ�\��
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
        // GmaeOver���Ăяo���ꂽ��\������
        public void GameOver()
		{
			if (!GameOverUI.activeSelf)
			{
				animator.SetTrigger("GameOver");
				GameOverButton.Select();
			}
		}

		public void StageClear()
		{
			animator.SetTrigger("StageClear");
			StageClearButton.Select();
		}

		public void Retry()
		{
			animator.SetTrigger("Transition");
			SceneManager.LoadScene("Stage");
		}
		#endregion

		// Home�������ꂽ��scene��Title�ֈڍs����
		public void Home()
		{
			animator.SetTrigger("Transition");
			SceneManager.LoadScene("Title");
			// �ĊJ
			Time.timeScale = 1f;
		}

		public async void ClosePrologue()
        {
			PrologueUI.SetActive(false);

			// 2�b��Ɏ��s
			await Task.Delay(2000);
			DisplayController();
        }

		#region �v���C���[�̑傫���ʂɕ\��
		public void ChangeNumber(int n)
		{
			if (n == 0)
			{
				Size[0].SetActive(true);
				Size[1].SetActive(false);
				Size[2].SetActive(false);

			}
			else if (n == 1)
			{
				Size[0].SetActive(false);
				Size[1].SetActive(true);
				Size[2].SetActive(false);
			}
			else if (n == 2)
			{
				Size[0].SetActive(false);
				Size[1].SetActive(false);
				Size[2].SetActive(true);
			}
		}
		#endregion

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

		private void DisplayController()
        {
			backTutorialButton.Select();
			controller.SetActive(true);
			backButton.SetActive(true);
        }

		private void HiddenController()
        {
			controller.SetActive(false);
			backButton.SetActive(false);
		}

		public void DisplayHealExplanation()
        {
			closeTutorialButton.Select();
			healExplanation.SetActive(true);
			closeButton.SetActive(true);
			StageScene.Instance.prologue = true;
        }

		private void HiddenHealExplanation()
        {
			healExplanation.SetActive(false);
			closeButton.SetActive(false);
		}

		public void FinishTutorial()
        {
			HiddenController();
			HiddenHealExplanation();
			StageScene.Instance.prologue = false;
		}
	}
}