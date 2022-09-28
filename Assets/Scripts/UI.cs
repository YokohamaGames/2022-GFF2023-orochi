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

	// pause���̏����I����Ԃɐݒ肷��{�^�����w�肵�܂�
	[SerializeField]
	private Selectable PauseButton = null;

	void Start()
	{
		PauseUI.SetActive(false);
		OptionUI.SetActive(false);
		PauseButton.Select();
	}

	// pause��ʂ�\��
	public void Show()
	{
		// �|�[�Y�̕\��
		PauseUI.SetActive(true);
		// ��~
		Time.timeScale = 0f;
	}
	// option���o�Ă���ΐ�ɉB���A�o�Ă��Ȃ����pause��ʂ��B��
	public void Hide()
	{
		if (!OptionUI.activeInHierarchy)
		{
			// �|�[�Y�̔�\��
			PauseUI.SetActive(false);
			// �ĊJ
			Time.timeScale = 1f;
		} 
		else if (Input.GetKeyDown(KeyCode.Escape))
		{
			// option�̔�\��
			OptionUI.SetActive(false);
		}
	}

	// Back�������ꂽ��ReStartGame���Ăяo��
	public void Back()
	{
		Hide();
	}

	// Option�{�^���������ꂽ����Option��ʂ̕\��
	public void Option()
	{
		if (PauseUI.activeInHierarchy) 
		{
			OptionUI.SetActive(true);
		} 
	}

	// Home�������ꂽ��scene��Title�ֈڍs����
	public void Home()
	{
		SceneManager.LoadScene("Title");
	}
}
