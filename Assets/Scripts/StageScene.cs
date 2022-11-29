using UnityEngine;
using UnityEngine.EventSystems;
using System.Threading.Tasks;

public class StageScene : MonoBehaviour
{   
	public static StageScene Instance { get; private set; }

	// UIを指定します。
	[SerializeField]
	private UI Ui = null;

    // プレイヤーのHPを指定
    [SerializeField]
    public int playerhp;

    //回復エフェクトの指定
    [SerializeField]
    public GameObject HealObject;

    //敵のHPを設定
    [SerializeField]
    public int EnemyHp;

    public GameObject Player;

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

    public async Task Update()
    {
        if (playerhp == 0)
        {
            Ui.GameOver();
        }

        if (EnemyHp <= 0)
        {  
            await Task.Delay(5000);
            Ui.StageClear();
            Player.GetComponent<MoveBehaviourScript>().ClearState();
        }
    }

    public void Heal(Vector3 EffectTransform)
    {
        Debug.Log("回復");
        Instantiate(HealObject, EffectTransform, Quaternion.identity); //パーティクル用ゲームオブジェクト生成
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
    
}
