using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
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

	// HpBar���Q��
	[SerializeField]
	private GameObject HpBar = null;

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
			// �|�[�Y�̕\��
			PauseUI.SetActive(true);
			// HpBar�̔�\��
			HpBar.SetActive(false);
			// UI���J���ꂽ�������Đ�
			Se.OpenUI();
			// ��~
			Time.timeScale = 0f;
			
		}
		else if (PauseUI.activeSelf && !OptionUI.activeSelf && !GuideUI.activeSelf)
        {
			Player.SetActive(false);

			// �|�[�Y�̔�\��
			PauseUI.SetActive(false);
			// HpBar�̕\��
			HpBar.SetActive(true);
			// UI������ꂽ�������Đ�
			Se.CloseUI();
			// �ĊJ
			Time.timeScale = 1f;
		}
        else if (PauseUI.activeSelf && OptionUI.activeSelf)
		{
			Player.SetActive(true);

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
			StartCoroutine(DelayClear());
			animator.SetTrigger("GameOver");
			GameOverButton.Select();

		}
	}

	IEnumerator DelayClear()
    {
		yield return new WaitForSeconds(8);
    }

	public void StageClear()
    {

		StageClearUI.SetActive(true);
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
        SceneManager.LoadScene("Title");
        // �ĊJ
        Time.timeScale = 1f;
	}
}
