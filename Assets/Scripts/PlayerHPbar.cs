using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHPbar : MonoBehaviour
{
    [SerializeField]
    private GameObject GameOverUI = null;

    public static PlayerHPbar Instance { get; private set; }

    public GameObject[] heartArray = new GameObject[6];
    private int playerhp;

    void Start()
    {
        playerhp = 6;
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
        if (playerhp == 5)
        {
            heartArray[5].gameObject.SetActive(false);
            heartArray[4].gameObject.SetActive(true);
            heartArray[3].gameObject.SetActive(true);
            heartArray[2].gameObject.SetActive(true);
            heartArray[1].gameObject.SetActive(true);
            heartArray[0].gameObject.SetActive(true);
        }

        if (playerhp == 4)
        {
            heartArray[5].gameObject.SetActive(false);
            heartArray[4].gameObject.SetActive(false);
            heartArray[3].gameObject.SetActive(true);
            heartArray[2].gameObject.SetActive(true);
            heartArray[1].gameObject.SetActive(true);
            heartArray[0].gameObject.SetActive(true);
        }

        if (playerhp == 3)
        {
            heartArray[5].gameObject.SetActive(false);
            heartArray[4].gameObject.SetActive(false);
            heartArray[3].gameObject.SetActive(false);
            heartArray[2].gameObject.SetActive(true);
            heartArray[1].gameObject.SetActive(true);
            heartArray[0].gameObject.SetActive(true);
        }
        if (playerhp == 2)
        {
            heartArray[5].gameObject.SetActive(false);
            heartArray[4].gameObject.SetActive(false);
            heartArray[3].gameObject.SetActive(false);
            heartArray[2].gameObject.SetActive(false);
            heartArray[1].gameObject.SetActive(true);
            heartArray[0].gameObject.SetActive(true);
        }

        if (playerhp == 1)
        {
            heartArray[5].gameObject.SetActive(false);
            heartArray[4].gameObject.SetActive(false);
            heartArray[3].gameObject.SetActive(false);
            heartArray[2].gameObject.SetActive(false);
            heartArray[1].gameObject.SetActive(false);
            heartArray[0].gameObject.SetActive(true);
        }

        if (playerhp == 0)
        {
            heartArray[5].gameObject.SetActive(false);
            heartArray[4].gameObject.SetActive(false);
            heartArray[3].gameObject.SetActive(false);
            heartArray[2].gameObject.SetActive(false);
            heartArray[1].gameObject.SetActive(false);
            heartArray[0].gameObject.SetActive(false);

            GameOverUI.SetActive(true);
        }
    }
}