using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading.Tasks;
public class UI : MonoBehaviour
{
	// �|�[�Y���Q��
	[SerializeField]
	private GameObject PauseUI = null;

	// �I�v�V�������Q��
	[SerializeField]
	private GameObject OptionUI = null;

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

	// HP�o�[�̃T�C�Y�ʐ���
	[SerializeField]
	private GameObject[] Size = null;

	Animator animator;

	public GameObject Player;

	// �J�n����UI���\��
	void Start()
	{
		PauseUI.SetActive(false);
		OptionUI.SetActive(false);
		GuideUI	.SetActive(false);
		GameOverUI.SetActive(false);
		StageClearUI.SetActive(false);
		FastButton.Select();

		animator = GetComponent<Animator>();

		animator.SetTrigger("Start");
	}

	// pause��ʂ�\��
	public void Control()
	{
		if (!PauseUI.activeSelf)
        {
			animator.SetTrigger("PauseIn");
			// �|�[�Y�̕\��
			PauseUI.SetActive(true);
			// UI���J���ꂽ�������Đ�
			Se.OpenUI();
			// ��~
			Time.timeScale = 0f;
			
		}
		else if (PauseUI.activeSelf && !OptionUI.activeSelf && !GuideUI.activeSelf)
        {
			// �|�[�Y�̔�\��
			PauseUI.SetActive(false);

			animator.SetTrigger("PauseOut");
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
		else if (PauseUI.activeSelf && GuideUI.activeSelf)
		{
			// Guide�̔�\��
			GuideUI.SetActive(false);
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

	// ��������������ꂽ���ɑ��������ʂ�\��
	public void Guide()
    {
		if (PauseUI.activeInHierarchy)
		{
			GuideUI.SetActive(true);
			// UI���J���ꂽ�������Đ�
			Se.OpenUI();
			// Guide���I�����ꂽ�ꍇ�ɂ��̃{�^����I����Ԃɂ��܂�
			GuideButton.Select();

			animator.SetTrigger("Hide");
		}
	}

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

	// Home�������ꂽ��scene��Title�ֈڍs����
	public void Home()
	{
		 animator.SetTrigger("Transition");
		 SceneManager.LoadScene("Title");
        // �ĊJ
         Time.timeScale = 1f;
	}

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
}
