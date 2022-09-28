using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UI : MonoBehaviour
{
	// ポーズを参照
	[SerializeField]
	private GameObject PauseUI = null;

	// オプションを参照
	[SerializeField]
	private GameObject OptionUI = null;

	// pause時の初期選択状態に設定するボタンを指定します
	[SerializeField]
	private Selectable PauseButton = null;

	void Start()
	{
		PauseUI.SetActive(false);
		OptionUI.SetActive(false);
		PauseButton.Select();
	}

	// pause画面を表示
	public void Show()
	{
		// ポーズの表示
		PauseUI.SetActive(true);
		// 停止
		Time.timeScale = 0f;
	}
	// optionが出ていれば先に隠し、出ていなければpause画面を隠す
	public void Hide()
	{
		if (!OptionUI.activeInHierarchy)
		{
			// ポーズの非表示
			PauseUI.SetActive(false);
			// 再開
			Time.timeScale = 1f;
		} 
		else if (Input.GetKeyDown(KeyCode.Escape))
		{
			// optionの非表示
			OptionUI.SetActive(false);
		}
	}

	// Backが押されたらReStartGameを呼び出す
	public void Back()
	{
		Hide();
	}

	// Optionボタンが押された時のOption画面の表示
	public void Option()
	{
		if (PauseUI.activeInHierarchy) 
		{
			OptionUI.SetActive(true);
		} 
	}

	// Homeが押されたらsceneをTitleへ移行する
	public void Home()
	{
		SceneManager.LoadScene("Title");
	}
}
