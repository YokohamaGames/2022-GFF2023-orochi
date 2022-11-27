using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScene : MonoBehaviour
{
    // Titleを参照
    [SerializeField]
    private GameObject Title = null;

    // Titleボタンを参照
    [SerializeField]
    private Selectable TitleButton = null;

    /* StageSelectを参照
    [SerializeField]
    private GameObject StageSelect = null;
       selectボタンを参照
    [SerializeField]
    private Selectable SelectButton = null;
    */

    private void Start()
    {
        Title.SetActive(true);
        // StageSelect.SetActive(false);
        TitleButton.Select();
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
        Application.Quit();
    }

    // Stageを読み込む
    public void Stage1()
    {
        SceneManager.LoadScene("Stage");
    }
}
