using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
	// �|�[�Y���Q��
	[SerializeField]
	private GameObject Panel = null;

	// �����I����Ԃɐݒ肷��{�^�����w�肵�܂��B
	[SerializeField]
	private Selectable PauseButton = null;
	void Start()
	{
		Panel.SetActive(false);
		PauseButton.Select();
	}

	// �Q�[�����~�߂�
	public void StopGame()
	{
		// �|�[�Y�̕\��
		Panel.SetActive(true);
		// ��~
		Time.timeScale = 0f;
	}
	// �Q�[�����ĊJ������
	public void ReStartGame()
	{
		// �|�[�Y�̔�\��
		Panel.SetActive(false);
		// �ĊJ
		Time.timeScale = 1f;
	}

	// Back�������ꂽ��ReStartGame���Ăяo��
	public void Back()
	{
		ReStartGame();
	}

	// Home�������ꂽ��scene��Title�ֈڍs����
	public void Home()
	{
		SceneManager.LoadScene("Title");
	}
}