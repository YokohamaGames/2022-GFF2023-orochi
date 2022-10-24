using UnityEngine;
using UnityEngine.EventSystems;

public class StageScene : MonoBehaviour
{   
	public static StageScene Instance { get; private set; }

	// UIを指定します。
	[SerializeField]
	private UI Ui = null;

    // プレイヤーのHPを指定
    [SerializeField]
    public int playerhp;

    // 敵のHPを指定
    [SerializeField]
    public int Enemyhp;

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

    public void Heal()
    {
        Debug.Log("回復");
        playerhp += 1;
    }

    // Damageが呼び出されたらHPが1減る
    public void Damage()
    {
        if (playerhp > 0)
        {
            playerhp--;
        }
    }

    public void EnemyDamage()
    {
        if (Enemyhp > 0)
        {
            Enemyhp--;
        }
    }
}
