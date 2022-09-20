using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHPbar : MonoBehaviour
{
    public GameObject[] heartArray = new GameObject[3];
    private int playerhp;

    void Start()
    {
        playerhp = 3;
    }

    void Update()
    {
        //���N���b�N ���C�t�|�C���g���炷
        if (Input.GetMouseButtonDown(0))
        {
            if (playerhp > 0)
            {
                playerhp--;
            }
        }

        //�E�N���b�N ���C�t�|�C���g���₷
        if (Input.GetMouseButtonDown(1))
        {
            if (playerhp < 3)
            {
                playerhp++;
            }
        }

        if (playerhp == 3)
        {
            heartArray[2].gameObject.SetActive(true);
            heartArray[1].gameObject.SetActive(true);
            heartArray[0].gameObject.SetActive(true);
        }

        if (playerhp == 2)
        {
            heartArray[2].gameObject.SetActive(false);
            heartArray[1].gameObject.SetActive(true);
            heartArray[0].gameObject.SetActive(true);
        }
        if (playerhp == 1)
        {
            heartArray[2].gameObject.SetActive(false);
            heartArray[1].gameObject.SetActive(false);
            heartArray[0].gameObject.SetActive(true);
        }

        if (playerhp == 0)
        {
            heartArray[2].gameObject.SetActive(false);
            heartArray[1].gameObject.SetActive(false);
            heartArray[0].gameObject.SetActive(false);
        }
    }
}