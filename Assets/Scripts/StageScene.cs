using UnityEngine;
using UnityEngine.EventSystems;
using System.Threading.Tasks;

public class StageScene : MonoBehaviour
{   
	public static StageScene Instance { get; private set; }

	// UI���w�肵�܂��B
	[SerializeField]
	private UI Ui = null;

    // �v���C���[��HP���w��
    [SerializeField]
    public int playerhp;

    //�񕜃G�t�F�N�g�̎w��
    [SerializeField]
    public GameObject HealObject;

    //�G��HP��ݒ�
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
        Debug.Log("��");
        Instantiate(HealObject, EffectTransform, Quaternion.identity); //�p�[�e�B�N���p�Q�[���I�u�W�F�N�g����
        playerhp += 1;
    }

    // Damage���Ăяo���ꂽ��HP��1����
    public void Damage()
    {
        if (playerhp > 0)
        {
            playerhp--;
        }
    }
    
}
