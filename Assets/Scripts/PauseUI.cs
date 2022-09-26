using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
	// �|�[�Y���Q��
	[SerializeField]
	private GameObject pause = null;

	// �����I����Ԃɐݒ肷��{�^�����w�肵�܂��B
	[SerializeField]
	private Selectable PauseButton = null;
	void Start()
	{
		pause.SetActive(false);
		PauseButton.Select();
	}

	// �Q�[�����~�߂�
	public void StopGame()
	{
		// �|�[�Y�̕\��
		pause.SetActive(true);
		// ��~
		Time.timeScale = 0f;
	}
	// �Q�[�����ĊJ������
	public void ReStartGame()
	{
		// �|�[�Y�̔�\��
		pause.SetActive(false);
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
