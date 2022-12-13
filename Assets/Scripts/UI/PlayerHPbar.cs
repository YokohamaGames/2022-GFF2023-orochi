using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class PlayerHPbar : MonoBehaviour
{
    public static PlayerHPbar Instance { get; private set; }

    public RawImage hpbar;

    private  Rect uvRect;

    RectTransform rectTransform;
    void Start()
    {
        hpbar = GetComponent<RawImage>();
        uvRect = new Rect(1.0f,0.0f,1.0f,0.0f);

        rectTransform = gameObject.GetComponent<RectTransform>();

        rectTransform.sizeDelta = new Vector2(390.0f,31.0f);


    }

    private void Awake()
    {
        Instance = this;
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
            uvRect.x = 1.0f;
            uvRect.width = 1.0f;
            hpbar.uvRect = uvRect;

            rectTransform.sizeDelta = new Vector2(390, 31);

        }
        else if (StageScene.Instance.playerhp == 5)
        {

            uvRect.x = 0.2f;
            uvRect.width = 0.8f;
            hpbar.uvRect = uvRect;

            rectTransform.sizeDelta = new Vector2(312, 31);



        }

        else if (StageScene.Instance.playerhp == 4)
        {
            uvRect.x = 0.4f;
            uvRect.width = 0.6f;
            hpbar.uvRect = uvRect;

            rectTransform.sizeDelta = new Vector2(282, 31);
        }

        else if (StageScene.Instance.playerhp == 3)
        {
            uvRect.x = 0.6f;
            uvRect.width = 0.4f;
            hpbar.uvRect = uvRect;

            rectTransform.sizeDelta = new Vector2(192, 31);
        }
        else if (StageScene.Instance.playerhp == 2)
        {
            uvRect.x = 0.8f;
            uvRect.width = 0.2f;
            hpbar.uvRect = uvRect;

            rectTransform.sizeDelta = new Vector2(114, 31);

        }

        else if (StageScene.Instance.playerhp == 1)
        {
            uvRect.x = 0.9f;
            uvRect.width = 0.1f;
            hpbar.uvRect = uvRect;

            rectTransform.sizeDelta = new Vector2(36, 31);

        }

        else if (StageScene.Instance.playerhp == 0)
        {
            uvRect.x = 0.0f;
            uvRect.width = 0.0f;
            hpbar.uvRect = uvRect;

            rectTransform.sizeDelta = new Vector2(0, 31);

        }
    }
}