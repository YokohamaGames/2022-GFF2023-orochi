using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤーが敵の索敵範囲内範囲に侵入、脱出時の処理
public class SearchArea : MonoBehaviour
{   
    //親のEnemyスクリプトの取得
    [SerializeField]
    Enemy Parent_Enemy = null;

    //ターゲットの索敵範囲内の侵入判定
    private void OnTriggerEnter(Collider colision)
    {
        
        if (colision.CompareTag("Player"))
        {
            Parent_Enemy.SearchArea = true;
            //Parent_Enemy.SetDiscoverState();
        }
    }
    //ターゲットの索敵範囲外の脱出判定
    private void OnTriggerExit(Collider colision)
    {
        if (colision.CompareTag("Player"))
        {
            Parent_Enemy.SearchArea = false;
            //Parent_Enemy.SetIdleState();
        }
    }
}
