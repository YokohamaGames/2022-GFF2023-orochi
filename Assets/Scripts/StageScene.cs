using UnityEngine;
using UnityEngine.EventSystems;

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
