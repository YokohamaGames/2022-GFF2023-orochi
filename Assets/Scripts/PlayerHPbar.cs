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
    private int playerhp;

    public bool big, med, min;

    void Start()
    {
        playerhp = 6;

        big = true;
        med = false;
        min = false;
    }

    private void Awake()
    {
        Instance = this;
    }

    // Damage‚ªŒÄ‚Ño‚³‚ê‚½‚çHP‚ª1Œ¸‚é
    public void Damage()
    {
        {
            if (playerhp > 0)
            {
                playerhp--;
            }
        }
    }

    // Heal‚ªŒÄ‚Ño‚³‚ê‚½‚çHP‚ª1‘‚¦‚é
    public void Heel()
    {
        if (playerhp < 6)
        {
            playerhp++;
        }
    }

    void Update()
    {
        if (playerhp == 6)
        {
            heartArray[5].gameObject.SetActive(true);
            heartArray[4].gameObject.SetActive(true);
            heartArray[3].gameObject.SetActive(true);
            heartArray[2].gameObject.SetActive(true);
            heartArray[1].gameObject.SetActive(true);
            heartArray[0].gameObject.SetActive(true);
        }
        else if (playerhp == 5)
        {
            heartArray[5].gameObject.SetActive(false);
            heartArray[4].gameObject.SetActive(true);
            heartArray[3].gameObject.SetActive(true);
            heartArray[2].gameObject.SetActive(true);
            heartArray[1].gameObject.SetActive(true);
            heartArray[0].gameObject.SetActive(true);

        }

        else if (playerhp == 4)
        {
            heartArray[5].gameObject.SetActive(false);
            heartArray[4].gameObject.SetActive(false);
            heartArray[3].gameObject.SetActive(true);
            heartArray[2].gameObject.SetActive(true);
            heartArray[1].gameObject.SetActive(true);
            heartArray[0].gameObject.SetActive(true);

            if (big == true)
            {
                MoveBehaviourScript.Instance.Medium();

                big = false;
            }
        }

        else if (playerhp == 3)
        {
            heartArray[5].gameObject.SetActive(false);
            heartArray[4].gameObject.SetActive(false);
            heartArray[3].gameObject.SetActive(false);
            heartArray[2].gameObject.SetActive(true);
            heartArray[1].gameObject.SetActive(true);
            heartArray[0].gameObject.SetActive(true);

            if (min == true)
            {
                MoveBehaviourScript.Instance.Medium();

                med = true;
                min = false;
            }
        }
        else if (playerhp == 2)
        {
            heartArray[5].gameObject.SetActive(false);
            heartArray[4].gameObject.SetActive(false);
            heartArray[3].gameObject.SetActive(false);
            heartArray[2].gameObject.SetActive(false);
            heartArray[1].gameObject.SetActive(true);
            heartArray[0].gameObject.SetActive(true);

            if (med == true)
            {
                MoveBehaviourScript.Instance.Small();

                med = false;
                min = true;

            }
        }

        else if (playerhp == 1)
        {
            heartArray[5].gameObject.SetActive(false);
            heartArray[4].gameObject.SetActive(false);
            heartArray[3].gameObject.SetActive(false);
            heartArray[2].gameObject.SetActive(false);
            heartArray[1].gameObject.SetActive(false);
            heartArray[0].gameObject.SetActive(true);

        }

        else if (playerhp == 0)
        {
            heartArray[5].gameObject.SetActive(false);
            heartArray[4].gameObject.SetActive(false);
            heartArray[3].gameObject.SetActive(false);
            heartArray[2].gameObject.SetActive(false);
            heartArray[1].gameObject.SetActive(false);
            heartArray[0].gameObject.SetActive(false);

            Ui.GameOver();
        }
    }
}