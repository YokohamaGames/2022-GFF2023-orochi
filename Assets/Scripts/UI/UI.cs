using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading.Tasks;
public class UI : MonoBehaviour
{
	// ポーズを参照
	[SerializeField]
	private GameObject PauseUI = null;

	// オプションを参照
	[SerializeField]
	private GameObject OptionUI = null;

	// ガイドを参照
	[SerializeField]
	private GameObject GuideUI = null;

	// ゲームオーバーを参照
	[SerializeField]
	private GameObject GameOverUI = null;

	// ステージクリアを参照
	[SerializeField]
	private GameObject StageClearUI = null;

	// SEを参照
	[SerializeField]
	private SE Se = null;

	// pause時の初期選択状態に設定するボタンを指定
	[SerializeField]
	private Selectable FastButton = null;

	// Optionが開かれた時に選択されるボタンを指定
	[SerializeField]
	private Selectable OptionButton = null;

	// Guideが開かれた時に選択されるボタンを指定
	[SerializeField]
	private Selectable GuideButton = null;

	// GameOverが開かれた時に選択されるボタン
	[SerializeField]
	private Selectable GameOverButton = null;

	// StageClearが開かれた時に選択されるボタン
	[SerializeField]
	private Selectable StageClearButton = null;

	// HPバーのサイズ別数字
	[SerializeField]
	private GameObject[] Size = null;

	Animator animator;

	public GameObject Player;

	// 開始時にUIを非表示
	void Start()
	{
		PauseUI.SetActive(false);
		OptionUI.SetActive(false);
		GuideUI	.SetActive(false);
		GameOverUI.SetActive(false);
		StageClearUI.SetActive(false);
		FastButton.Select();

		animator = GetComponent<Animator>();

		animator.SetTrigger("Start");
	}

	// pause画面を表示
	public void Control()
	{
		if (!PauseUI.activeSelf)
        {
			animator.SetTrigger("PauseIn");
			// ポーズの表示
			PauseUI.SetActive(true);
			// UIが開かれた音声を再生
			Se.OpenUI();
			// 停止
			Time.timeScale = 0f;
			
		}
		else if (PauseUI.activeSelf && !OptionUI.activeSelf && !GuideUI.activeSelf)
        {
			// ポーズの非表示
			PauseUI.SetActive(false);

			animator.SetTrigger("PauseOut");
			// UIが閉じられた音声を再生
			Se.CloseUI();
			// 再開
			Time.timeScale = 1f;
		}
        else if (PauseUI.activeSelf && OptionUI.activeSelf)
		{
			// optionの非表示
			OptionUI.SetActive(false);
			// UIが閉じられた音声を再生
			Se.CloseUI();
			FastButton.Select();

			animator.SetTrigger("Show");
		}
		else if (PauseUI.activeSelf && GuideUI.activeSelf)
		{
			// Guideの非表示
			GuideUI.SetActive(false);
			// UIが閉じられた音声を再生
			Se.CloseUI();
			FastButton.Select();

			animator.SetTrigger("Show");
		}
	}

	// Optionボタンが押された時のOption画面の表示
	public void Option()
	{
		if (PauseUI.activeInHierarchy) 
		{
			OptionUI.SetActive(true);
			// UIが開かれた音声を再生
			Se.OpenUI();
			// Optionが選択された場合にこのボタンを選択状態にします
			OptionButton.Select();

			animator.SetTrigger("Hide");
		} 
	}

	// 操作説明が押された時に操作説明画面を表示
	public void Guide()
    {
		if (PauseUI.activeInHierarchy)
		{
			GuideUI.SetActive(true);
			// UIが開かれた音声を再生
			Se.OpenUI();
			// Guideが選択された場合にこのボタンを選択状態にします
			GuideButton.Select();

			animator.SetTrigger("Hide");
		}
	}

	// GmaeOverが呼び出されたら表示する
	public void GameOver()
	{
		if (!GameOverUI.activeSelf)
		{
			animator.SetTrigger("GameOver");
			GameOverButton.Select();

		}
	}

	public void StageClear()
    {
		animator.SetTrigger("StageClear");
		StageClearButton.Select();
    }

	public void Retry()
    {
		animator.SetTrigger("Transition");
		SceneManager.LoadScene("Stage");
    }

	// Homeが押されたらsceneをTitleへ移行する
	public void Home()
	{
		 animator.SetTrigger("Transition");
		 SceneManager.LoadScene("Title");
        // 再開
         Time.timeScale = 1f;
	}

	public void ChangeNumber(int n)
    {
		if (n == 0)
        {
			Size[0].SetActive(true);
			Size[1].SetActive(false);
			Size[2].SetActive(false);

		}
		else if (n == 1)
        {
			Size[0].SetActive(false);
			Size[1].SetActive(true);
			Size[2].SetActive(false);
		}
		else if (n == 2)
        {
			Size[0].SetActive(false);
			Size[1].SetActive(false);
			Size[2].SetActive(true);
		}
	}
}
