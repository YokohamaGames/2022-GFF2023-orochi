using UnityEngine;
using UnityEngine.EventSystems;

public class StageScene : MonoBehaviour
{   
	public static StageScene Instance { get; private set; }

	// UIを指定します。
	[SerializeField]
	private UI Ui = null;

    // プレイヤーのHPを指定
    public int playerhp;
    private void Awake()
    {
        Instance = this;
    }

    public void ControlPauseUI()
    {
        if (Time.timeScale > 0)
        {
            Ui.Control();
        }
    }

    public void Update()
    {
        if (playerhp == 0)
        {
            Ui.GameOver();
        }
    }
}
