using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OROCHI
{
    //プレイヤーが敵の索敵範囲内範囲に侵入、脱出時の処理
    public class SearchArea : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("親のEnemyスクリプトの取得")]
        Enemy parent_enemy = null;

        /// <summary>
        /// ターゲットの索敵範囲内の侵入判定
        /// </summary>
        private void OnTriggerEnter(Collider colision)
        {
            if (colision.CompareTag("Player"))
            {
                parent_enemy.isSearch = true;
                parent_enemy.SetDiscoverState();
            }
        }

        /// <summary>
        /// ターゲットの索敵範囲外の脱出判定
        /// </summary>
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
