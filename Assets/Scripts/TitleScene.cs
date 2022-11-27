using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScene : MonoBehaviour
{
    // Title���Q��
    [SerializeField]
    private GameObject Title = null;

    // Title�{�^�����Q��
    [SerializeField]
    private Selectable TitleButton = null;

    /* StageSelect���Q��
    [SerializeField]
    private GameObject StageSelect = null;
       select�{�^�����Q��
    [SerializeField]
    private Selectable SelectButton = null;
    */

    private void Start()
    {
        Title.SetActive(true);
        // StageSelect.SetActive(false);
        TitleButton.Select();
    }

    // Title���\���AStageSelect��\��
    public void Stageselect()
    {
        // SelectButton.Select();
        Title.SetActive(false);
        // StageSelect.SetActive(true);
    }

    // �Q�[�����I������
    public void Exit()
    {
        Application.Quit();
    }

    // Stage��ǂݍ���
    public void Stage1()
    {
        SceneManager.LoadScene("Stage");
    }
}
