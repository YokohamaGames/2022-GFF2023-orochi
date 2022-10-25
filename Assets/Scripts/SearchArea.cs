using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchArea : MonoBehaviour
{   
    //親のスクリプトの取得
    [SerializeField]
    Enemy Parent_Enemy = null;
    private void Start()
    {

    }
    //ターゲットの索敵範囲内の侵入判定
    private void OnTriggerEnter(Collider colision)
    {
        
        if (colision.CompareTag("Player"))
        {
            //Debug.Log("索敵範囲内");
            Parent_Enemy.SearchArea = true;
            Parent_Enemy.SetDiscoverState();
            
        }
    }
    //ターゲットの索敵範囲外の脱出判定
    private void OnTriggerExit(Collider colision)
    {
        if (colision.CompareTag("Player"))
        {
            //Debug.Log("索敵範囲外");
            Parent_Enemy.SearchArea = false;
            Parent_Enemy.SetStayState();
        }
    }

}
