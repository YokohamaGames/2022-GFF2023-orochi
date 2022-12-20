using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OROCHI
{
    //vC[ͺGΜυGΝΝΰΝΝΙNόAEoΜ
    public class SearchArea : MonoBehaviour
    {
        //eΜEnemyXNvgΜζΎ
        [SerializeField]
        Enemy parent_enemy = null;

        //^[QbgΜυGΝΝΰΜNό»θ
        private void OnTriggerEnter(Collider colision)
        {

            if (colision.CompareTag("Player"))
            {
                Debug.Log("υGΝΝNό");
                parent_enemy.isSearch = true;
                parent_enemy.SetDiscoverState();
            }
        }
        //^[QbgΜυGΝΝOΜEo»θ
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
