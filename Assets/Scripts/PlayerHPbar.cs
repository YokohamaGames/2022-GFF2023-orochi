using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHPbar : MonoBehaviour
{
    [SerializeField]
    private UI Ui = null;

    public static PlayerHPbar Instance { get; private set; }

    public GameObject[] heartArray = null;


    void Start()
    {

    }

    private void Awake()
    {
        Instance = this;
    }

    // Damage‚ªŒÄ‚Ño‚³‚ê‚½‚çHP‚ª1Œ¸‚é
    public void Damage()
    {
        {
            if (StageScene.Instance.playerhp > 0)
            {
                StageScene.Instance.playerhp--;
            }
        }
    }

    // Heal‚ªŒÄ‚Ño‚³‚ê‚½‚çHP‚ª1‘‚¦‚é
    public void Heel()
    {
        if (StageScene.Instance.playerhp < 6)
        {
            StageScene.Instance.playerhp++;
        }
    }

    void Update()
    {
        if (StageScene.Instance.playerhp == 6)
        {
            heartArray[5].gameObject.SetActive(true);
            heartArray[4].gameObject.SetActive(true);
            heartArray[3].gameObject.SetActive(true);
            heartArray[2].gameObject.SetActive(true);
            heartArray[1].gameObject.SetActive(true);
            heartArray[0].gameObject.SetActive(true);

        }
        else if (StageScene.Instance.playerhp == 5)
        {
            heartArray[5].gameObject.SetActive(false);
            heartArray[4].gameObject.SetActive(true);
            heartArray[3].gameObject.SetActive(true);
            heartArray[2].gameObject.SetActive(true);
            heartArray[1].gameObject.SetActive(true);
            heartArray[0].gameObject.SetActive(true);

        }

        else if (StageScene.Instance.playerhp == 4)
        {
            heartArray[5].gameObject.SetActive(false);
            heartArray[4].gameObject.SetActive(false);
            heartArray[3].gameObject.SetActive(true);
            heartArray[2].gameObject.SetActive(true);
            heartArray[1].gameObject.SetActive(true);
            heartArray[0].gameObject.SetActive(true);

        }

        else if (StageScene.Instance.playerhp == 3)
        {
            heartArray[5].gameObject.SetActive(false);
            heartArray[4].gameObject.SetActive(false);
            heartArray[3].gameObject.SetActive(false);
            heartArray[2].gameObject.SetActive(true);
            heartArray[1].gameObject.SetActive(true);
            heartArray[0].gameObject.SetActive(true);

        }
        else if (StageScene.Instance.playerhp == 2)
        {
            heartArray[5].gameObject.SetActive(false);
            heartArray[4].gameObject.SetActive(false);
            heartArray[3].gameObject.SetActive(false);
            heartArray[2].gameObject.SetActive(false);
            heartArray[1].gameObject.SetActive(true);
            heartArray[0].gameObject.SetActive(true);

        }

        else if (StageScene.Instance.playerhp == 1)
        {
            heartArray[5].gameObject.SetActive(false);
            heartArray[4].gameObject.SetActive(false);
            heartArray[3].gameObject.SetActive(false);
            heartArray[2].gameObject.SetActive(false);
            heartArray[1].gameObject.SetActive(false);
            heartArray[0].gameObject.SetActive(true);

        }

        else if (StageScene.Instance.playerhp == 0)
        {
            heartArray[5].gameObject.SetActive(false);
            heartArray[4].gameObject.SetActive(false);
            heartArray[3].gameObject.SetActive(false);
            heartArray[2].gameObject.SetActive(false);
            heartArray[1].gameObject.SetActive(false);
            heartArray[0].gameObject.SetActive(false);

        }
    }
}