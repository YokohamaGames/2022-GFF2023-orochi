using UnityEngine;
using UnityEngine.EventSystems;

public class StageScene : MonoBehaviour
{   
	// ポーズUIを指定します。
	[SerializeField]
	private PauseUI pauseUI = null;

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			//　ポーズUIが表示されてる時は停止
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
