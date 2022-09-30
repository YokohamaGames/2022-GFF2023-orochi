using UnityEngine;
using UnityEngine.EventSystems;

public class StageScene : MonoBehaviour
{   
	// UI���w�肵�܂��B
	[SerializeField]
	private UI Ui = null;

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			//�@�|�[�YUI���\������Ă鎞�͒�~
			if (Mathf.Approximately(Time.timeScale, 1f))
			{
				Ui.Show();
			}
			else
			{
				Ui.Hide();
			}
		}
	}
}
