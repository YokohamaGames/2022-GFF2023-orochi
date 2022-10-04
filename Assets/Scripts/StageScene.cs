using UnityEngine;
using UnityEngine.EventSystems;

public class StageScene : MonoBehaviour
{   
	public static StageScene Instance { get; private set; }

	// UI‚ðŽw’è‚µ‚Ü‚·B
	[SerializeField]
	private UI Ui = null;

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
}
