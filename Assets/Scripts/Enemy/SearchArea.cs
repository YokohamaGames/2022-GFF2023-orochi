using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OROCHI
{
    //vC[ͺGΜυGΝΝΰΝΝΙNόAEoΜ
    public class SearchArea : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("eΜEnemyXNvgΜζΎ")]
        Enemy parent_enemy = null;

        /// <summary>
        /// ^[QbgΜυGΝΝΰΜNό»θ
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
        /// ^[QbgΜυGΝΝOΜEo»θ
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
