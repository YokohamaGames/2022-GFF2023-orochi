using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UI : MonoBehaviour
{
	// �|�[�Y���Q��
	[SerializeField]
	private GameObject PauseUI = null;

	// �I�v�V�������Q��
	[SerializeField]
	private GameObject OptionUI = null;

	// �Q�[���I�[�o�[���Q��
	[SerializeField]
	private GameObject GameOverUI = null;

	// �X�e�[�W�N���A���Q��
	[SerializeField]
	private GameObject StageClearUI = null;

	// SE���Q��
	[SerializeField]
	private SE Se = null;

	// pause���̏����I����Ԃɐݒ肷��{�^�����w��
	[SerializeField]
	private Selectable FastButton = null;

	// Option���J���ꂽ���ɑI�������{�^�����w��
	[SerializeField]
	private Selectable OptionButton = null;

	// GameOver���J���ꂽ���ɑI�������{�^��
	[SerializeField]
	private Selectable GameOverButton = null;

	// StageClear���J���ꂽ���ɑI�������{�^��
	[SerializeField]
	private Selectable StageClearButton = null;

	Animator animator;

	// �J�n����UI���\��
	void Start()
	{
		PauseUI.SetActive(false);
		OptionUI.SetActive(false);
		GameOverUI.SetActive(false);
		StageClearUI.SetActive(false);
		FastButton.Select();

		animator = GetComponent<Animator>();
	}

	// pause��ʂ�\��
	public void Control()
	{
		if (!PauseUI.activeSelf)
        {
			// �|�[�Y�̕\��
			PauseUI.SetActive(true);
			// UI���J���ꂽ�������Đ�
			Se.OpenUI();
			// ��~
			Time.timeScale = 0f;
			
		}
		else if (PauseUI.activeSelf && !OptionUI.activeSelf)
        {
			// �|�[�Y�̔�\��
			PauseUI.SetActive(false);
			// UI������ꂽ�������Đ�
			Se.CloseUI();
			// �ĊJ
			Time.timeScale = 1f;
		}
        else if (PauseUI.activeSelf && OptionUI.activeSelf)
		{
			// option�̔�\��
			OptionUI.SetActive(false);
			// UI������ꂽ�������Đ�
			Se.CloseUI();
			FastButton.Select();

			animator.SetTrigger("Show");
		}
	}

	// Option�{�^���������ꂽ����Option��ʂ̕\��
	public void Option()
	{
		if (PauseUI.activeInHierarchy) 
		{
			OptionUI.SetActive(true);
			// UI���J���ꂽ�������Đ�
			Se.OpenUI();
			// Option���I�����ꂽ�ꍇ�ɂ��̃{�^����I����Ԃɂ��܂�
			OptionButton.Select();

			animator.SetTrigger("Hide");
		} 
	}

	// GmaeOver���Ăяo���ꂽ��\������
	public void GameOver()
	{
		if (!GameOverUI.activeSelf)
		{
			GameOverUI.SetActive(true);
			GameOverButton.Select();
		}
	}

	public void StageClear()
    {
		StageClearUI.SetActive(true);
		StageClearButton.Select();
    }

	public void Retry()
    {
		SceneManager.LoadScene("Stage");
    }

	// Home�������ꂽ��scene��Title�ֈڍs����
	public void Home()
	{
		SceneManager.LoadScene("Title");
	}
}
