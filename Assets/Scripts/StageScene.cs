using UnityEngine;
using UnityEngine.EventSystems;

public class StageScene : MonoBehaviour
{   
	// UIを指定します。
	[SerializeField]
	private UI Ui = null;

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			//　ポーズUIが表示されてる時は停止
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
