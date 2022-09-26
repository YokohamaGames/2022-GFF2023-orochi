using UnityEngine;
using UnityEngine.EventSystems;

public class StageScene : MonoBehaviour
{   
	// �|�[�YUI���w�肵�܂��B
	[SerializeField]
	private PauseUI pauseUI = null;

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			//�@�|�[�YUI���\������Ă鎞�͒�~
			if (Mathf.Approximately(Time.timeScale, 1f))
			{
				pauseUI.StopGame();
			}
			else
			{
				pauseUI.ReStartGame();
			}
		}
	}
}
