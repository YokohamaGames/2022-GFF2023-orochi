using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OROCHI
{
    //プレイヤーが敵の索敵範囲内範囲に侵入、脱出時の処理
    public class SearchArea : MonoBehaviour
    {
        //親のEnemyスクリプトの取得
        [SerializeField]
        Enemy parent_enemy = null;

        //ターゲットの索敵範囲内の侵入判定
        private void OnTriggerEnter(Collider colision)
        {

            if (colision.CompareTag("Player"))
            {
                parent_enemy.isSearch = true;
                parent_enemy.SetDiscoverState();
            }
        }
        //ターゲットの索敵範囲外の脱出判定
        private void OnTriggerExit(Collider colision)
        {
            if (colision.CompareTag("Player"))
            {
                parent_enemy.isSearch = false;
                parent_enemy.SetIdleState();
            }
        }
    }
}
