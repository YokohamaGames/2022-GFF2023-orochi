using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

namespace OROCHI
{
    public class TitleScene : MonoBehaviour
    {
        // Titleを参照
        [SerializeField]
        private GameObject Title = null;

        // Titleボタンを参照
        [SerializeField]
        private Selectable TitleButton = null;

        // 次のシーンを読み込み可能な場合はtrue、それ以外はfalse
        bool isLoadable = false;

        Animator animator;
        static readonly int FadeOutId = Animator.StringToHash("FadeOut");

        private void Start()
        {
            Title.SetActive(true);
            // StageSelect.SetActive(false);
            TitleButton.Select();
            animator = GetComponent<Animator>();
        }

        // Titleを非表示、StageSelectを表示
        public void Stageselect()
        {
            // SelectButton.Select();
            Title.SetActive(false);
            // StageSelect.SetActive(true);
        }

        // ゲームを終了する
        public void Exit()
        {
            StartCoroutine(OnStart());
            Application.Quit();
        }

        // Stageを読み込む
        public void LoadStage()
        {
            StartCoroutine(OnStart());
            SceneManager.LoadScene("Tutorial");

        }

        IEnumerator OnStart()
        {
            animator.SetTrigger(FadeOutId);
            yield return new WaitForSeconds(2);
            isLoadable = true;
        }

    }
}