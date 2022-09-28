using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
	// ポーズを参照
	[SerializeField]
	private GameObject Panel = null;

	// 初期選択状態に設定するボタンを指定します。
	[SerializeField]
	private Selectable PauseButton = null;
	void Start()
	{
		Panel.SetActive(false);
		PauseButton.Select();
	}

	// ゲームを止める
	public void StopGame()
	{
		// ポーズの表示
		Panel.SetActive(true);
		// 停止
		Time.timeScale = 0f;
	}
	// ゲームを再開させる
	public void ReStartGame()
	{
		// ポーズの非表示
		Panel.SetActive(false);
		// 再開
		Time.timeScale = 1f;
	}

	// Backが押されたらReStartGameを呼び出す
	public void Back()
	{
		ReStartGame();
	}

	// Homeが押されたらsceneをTitleへ移行する
	public void Home()
	{
		SceneManager.LoadScene("Title");
	}
}