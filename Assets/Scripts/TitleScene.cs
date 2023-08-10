using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

namespace OROCHI
{
    public class TitleScene : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Titleを参照")]
        private GameObject Title = null;

        [SerializeField]
        [Tooltip("Titleボタンを参照")]
        private Selectable TitleButton = null;

        Animator animator;
        static readonly int FadeOutId = Animator.StringToHash("FadeOut");

        private void Start()
        {
            Title.SetActive(true);
            TitleButton.Select();
            animator = GetComponent<Animator>();
        }

        /// <summary>
        /// Titleを非表示、StageSelectを表示
        /// </summary>
        public void Stageselect()
        {
            Title.SetActive(false);
        }

        /// <summary>
        /// ゲームを終了する
        /// </summary>
        public void Exit()
        {
            StartCoroutine(OnStart());
            Application.Quit();
        }

        /// <summary>
        /// チュートリアルステージに遷移
        /// </summary>
        public void LoadStage()
        {
            StartCoroutine(OnStart());
            SceneManager.LoadScene("Tutorial");
        }

        /// <summary>
        /// フェードアウト用の時間
        /// </summary>
        IEnumerator OnStart()
        {
            animator.SetTrigger(FadeOutId);
            yield return new WaitForSeconds(2);
        }

    }
}