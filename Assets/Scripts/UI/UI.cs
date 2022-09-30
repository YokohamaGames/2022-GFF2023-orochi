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

	// SE���Q��
	[SerializeField]
	private SE Se = null;

	// pause���̏����I����Ԃɐݒ肷��{�^�����w�肵�܂�
	[SerializeField]
	private Selectable FastButton = null;

	// �J�n����UI���\��
	void Start()
	{
		PauseUI.SetActive(false);
		OptionUI.SetActive(false);
		FastButton.Select();
	}

	// pause��ʂ�\��
	public void Show()
	{
		// �|�[�Y�̕\��
		PauseUI.SetActive(true);
		// UI���J���ꂽ�������Đ�
		Se.OpenUI();
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
			// UI������ꂽ�������Đ�
			Se.CloseUI();
			// �ĊJ
			Time.timeScale = 1f;
		} 
		else if (Input.GetKeyDown(KeyCode.Escape))
		{
			// option�̔�\��
			OptionUI.SetActive(false);
			// UI������ꂽ�������Đ�
			Se.CloseUI();
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
			// UI���J���ꂽ�������Đ�
			Se.OpenUI();
		} 
	}

	// Home�������ꂽ��scene��Title�ֈڍs����
	public void Home()
	{
		SceneManager.LoadScene("Title");
	}
}
