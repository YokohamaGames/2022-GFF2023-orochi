using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Threading.Tasks;

namespace OROCHI
{
	public class UI : MonoBehaviour
	{
		[SerializeField]
        [Tooltip("ポーズを参照")]
		private GameObject pauseUI = null;

		[SerializeField]
		[Tooltip("オプションを参照")]
		private GameObject optionUI = null;

		[SerializeField]
		[Tooltip("ガイドを参照")]
		private GameObject GuideUI = null;

		[SerializeField]
		[Tooltip("ゲームオーバーを参照")]
		private GameObject GameOverUI = null;

		[SerializeField]
		[Tooltip("ステージクリアを参照")]
		private GameObject StageClearUI = null;

		[SerializeField]
		[Tooltip("SEを参照")]
		private SE Se = null;

		[SerializeField]
		[Tooltip("プロローグUIを参照")]
		private GameObject PrologueUI = null;

		[SerializeField]
		[Tooltip("プロローグで選択されるボタンを指定")]
		private Selectable NextButton = null;

		[SerializeField]
		[Tooltip("回復の説明UIを参照")]
		private GameObject healExplanation = null;

		[SerializeField]
		[Tooltip("回復説明の戻るボタンのオブジェクトを指定")]
		private GameObject closeButton = null;

		[SerializeField]
		[Tooltip("回復説明が表示されたときに選択されるボタン")]
		private Selectable closeTutorialButton = null;

		[SerializeField]
		[Tooltip("pause時の初期選択状態に設定するボタンを指定")]
		private Selectable FastButton = null;

		[SerializeField]
		[Tooltip("Optionが開かれた時に選択されるボタンを指定")]
		private Selectable OptionButton = null;

		[SerializeField]
		[Tooltip("Guideが開かれた時に選択されるボタンを指定")]
		private Selectable GuideButton = null;

		[SerializeField]
		[Tooltip("GameOverが開かれた時に選択されるボタン")]
		private Selectable GameOverButton = null;

		[SerializeField]
		[Tooltip("StageClearが開かれた時に選択されるボタン")]
		private Selectable StageClearButton = null;

		[SerializeField]
		[Tooltip("チュートリアルのコントローラーが表示されたときに選択されるボタン")]
		private Selectable backTutorialButton = null;

		[SerializeField]
		[Tooltip("HPバーのサイズ別数字")]
		private GameObject[] Size = null;

		[SerializeField]
		[Tooltip("チュートリアルのコントローラーimageを指定")]
		private GameObject controller = null;

		[SerializeField]
		[Tooltip("チュートリアルの戻るボタンのオブジェクトを指定")]
		private GameObject backButton = null;

		Animator animator;

		public GameObject Player;

        [Tooltip("クールタイム用のUI")]
		public Image Formicon;

        [Tooltip("変身が可能か判定するフラグ")]
		public bool Changeable;

        [SerializeField]
        [Tooltip("変身のクールタイム")]
		public float CountTime = 5.0f;

		// 開始時にUIを非表示
		void Start()
		{
			pauseUI.SetActive(false);
			optionUI.SetActive(false);
			GuideUI.SetActive(false);
			GameOverUI.SetActive(false);
			StageClearUI.SetActive(false);

			if(NextButton != null)
            {
				NextButton.Select();
			}

			if(controller != null)
            {
				HiddenController();
            }

			if(healExplanation != null)
            {
				HiddenHealExplanation();
            }

			Changeable = true;
			animator = GetComponent<Animator>();
		}

        #region ポーズ画面を表示
        public void Control()
		{
			if (!pauseUI.activeSelf)
			{
				animator.SetTrigger("PauseIn");
				// ポーズの表示
				pauseUI.SetActive(true);
				// 戻るボタンを選択
				FastButton.Select();
				// UIが開かれた音声を再生
				Se.OpenUI();
				// 停止
				Time.timeScale = 0f;

			}
			else if (pauseUI.activeSelf && !optionUI.activeSelf && !GuideUI.activeSelf)
			{
				// ポーズの非表示
				pauseUI.SetActive(false);

				animator.SetTrigger("PauseOut");
				// UIが閉じられた音声を再生
				Se.CloseUI();
				// 再開
				Time.timeScale = 1f;
			}
			else if (pauseUI.activeSelf && optionUI.activeSelf)
			{
				// optionの非表示
				optionUI.SetActive(false);
				// UIが閉じられた音声を再生
				Se.CloseUI();
				FastButton.Select();

				animator.SetTrigger("Show");
			}
			else if (pauseUI.activeSelf && GuideUI.activeSelf)
			{
				// Guideの非表示
				GuideUI.SetActive(false);
				// UIが閉じられた音声を再生
				Se.CloseUI();
				FastButton.Select();

				animator.SetTrigger("Show");
			}
		}
        #endregion

        #region オプション画面の表示
        /// <summary>
		/// Optionボタンが押された時のOption画面の表示
		/// </summary>
        public void Option()
		{
			if (pauseUI.activeInHierarchy)
			{
				optionUI.SetActive(true);
				// UIが開かれた音声を再生
				Se.OpenUI();
				// Optionが選択された場合にこのボタンを選択状態にします
				OptionButton.Select();

				animator.SetTrigger("Hide");
			}
		}
        #endregion

        #region 操作説明画面を表示
        /// <summary>
		/// 操作説明が押された時に操作説明画面を表示
		/// </summary>
        public void Guide()
		{
			if (pauseUI.activeInHierarchy)
			{
				GuideUI.SetActive(true);
				// UIが開かれた音声を再生
				Se.OpenUI();
				// Guideが選択された場合にこのボタンを選択状態にします
				GuideButton.Select();

				animator.SetTrigger("Hide");
			}
		}
        #endregion

        #region ゲームオーバー画面を表示
        /// <summary>
		/// GmaeOverが呼び出されたら表示する
		/// </summary>
        public void GameOver()
		{
			if (!GameOverUI.activeSelf)
			{
				animator.SetTrigger("GameOver");
				GameOverButton.Select();
			}
		}

		/// <summary>
		/// ステージクリア時の処理
		/// </summary>
		public void StageClear()
		{
			animator.SetTrigger("StageClear");
			StageClearButton.Select();
		}

		/// <summary>
		/// ステージリトライ時の処理
		/// </summary>
		public void Retry()
		{
			animator.SetTrigger("Transition");
			SceneManager.LoadScene("Stage");
		}
		#endregion

		/// <summary>
		/// Homeが押されたらsceneをTitleへ移行する
		/// </summary>
		public void Home()
		{
			animator.SetTrigger("Transition");
			SceneManager.LoadScene("Title");
			// 再開
			Time.timeScale = 1f;
		}

		/// <summary>
		/// プロローグを終了する処理
		/// </summary>
		public async void ClosePrologue()
        {
			PrologueUI.SetActive(false);

			// 2秒後に実行
			await Task.Delay(2000);
			DisplayController();
        }

		#region プレイヤーの大きさ別に表示
		/// <summary>
		/// プレイヤーの大きさに該当する数字を表示
		/// </summary>
		/// <param name="n">表示したい大きさに該当する数字</param>
		public void ChangeNumber(int n)
		{
			// 小さい状態
			if (n == 0)
			{
				Size[0].SetActive(true);
				Size[1].SetActive(false);
				Size[2].SetActive(false);

			}
			// 通常時の状態
			else if (n == 1)
			{
				Size[0].SetActive(false);
				Size[1].SetActive(true);
				Size[2].SetActive(false);
			}
			// 大きい時の状態
			else if (n == 2)
			{
				Size[0].SetActive(false);
				Size[1].SetActive(false);
				Size[2].SetActive(true);
			}
		}
		#endregion

		/// <summary>
		/// クールタイムを切り替え
		/// </summary>
		public void ChangeCooltime()
		{
			if (Changeable)
			{
				Formicon.fillAmount = 0;
				StartCoroutine(loop());
			}
		}

		private IEnumerator loop()
        {
			while(Formicon.fillAmount < 1)
            {
				yield return null;
				Formicon.fillAmount += 1.0f / CountTime * Time.deltaTime;
			}
		}

		/// <summary>
		/// 操作説明画面の表示
		/// </summary>
		private void DisplayController()
        {
			backTutorialButton.Select();
			controller.SetActive(true);
			backButton.SetActive(true);
        }

		/// <summary>
		/// 操作説明画面の非表示
		/// </summary>
		private void HiddenController()
        {
			controller.SetActive(false);
			backButton.SetActive(false);
		}

		/// <summary>
		/// 回復説明の表示
		/// </summary>
		public void DisplayHealExplanation()
        {
			closeTutorialButton.Select();
			healExplanation.SetActive(true);
			closeButton.SetActive(true);
			StageScene.Instance.prologue = true;
        }

		/// <summary>
		/// 回復説明の非表示
		/// </summary>
		private void HiddenHealExplanation()
        {
			healExplanation.SetActive(false);
			closeButton.SetActive(false);
		}

		/// <summary>
		/// チュートリアル終了の処理
		/// </summary>
		public void FinishTutorial()
        {
			HiddenController();
			HiddenHealExplanation();
			StageScene.Instance.prologue = false;
		}
	}
}