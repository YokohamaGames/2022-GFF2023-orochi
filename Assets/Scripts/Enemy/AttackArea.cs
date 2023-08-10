using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OROCHI
{
    // プレイヤーが敵の攻撃範囲に侵入、脱出時の処理
    public class AttackArea : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("親のスクリプトを取得")]
        Enemy parentEnemy = null;

        /// <summary>
        /// ターゲットの攻撃範囲への侵入判定
        /// </summary>
        private void OnTriggerEnter(Collider colision)
        {
            // Playerが攻撃範囲内に侵入
            if (colision.CompareTag("Player"))
            {
                parentEnemy.isAttacks = true;
                // Enemyのステートを攻撃準備に変更
                parentEnemy.SetAttackReadyState();
            }
        }
        // ターゲットが攻撃判定からの脱出の判定
        private void OnTriggerExit(Collider colision)
        {
            // Playerが攻撃範囲外に脱出
            if (colision.CompareTag("Player"))
            {
                parentEnemy.isAttacks = false;
                // EnemyのステートをPlayerを見失うに変更
                parentEnemy.SetMoveState();
            }
        }
    }
}
