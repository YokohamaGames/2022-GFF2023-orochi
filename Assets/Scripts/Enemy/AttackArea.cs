using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OROCHI
{
    // vC[ͺGΜUΝΝΙNόAEoΜ
    public class AttackArea : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("eΜXNvgπζΎ")]
        Enemy parentEnemy = null;

        /// <summary>
        /// ^[QbgΜUΝΝΦΜNό»θ
        /// </summary>
        private void OnTriggerEnter(Collider colision)
        {
            // PlayerͺUΝΝΰΙNό
            if (colision.CompareTag("Player"))
            {
                parentEnemy.isAttacks = true;
                // EnemyΜXe[gπUυΙΟX
                parentEnemy.SetAttackReadyState();
            }
        }
        // ^[QbgͺU»θ©ηΜEoΜ»θ
        private void OnTriggerExit(Collider colision)
        {
            // PlayerͺUΝΝOΙEo
            if (colision.CompareTag("Player"))
            {
                parentEnemy.isAttacks = false;
                // EnemyΜXe[gπPlayerπ©Έ€ΙΟX
                parentEnemy.SetMoveState();
            }
        }
    }
}
