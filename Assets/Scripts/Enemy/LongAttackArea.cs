using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OROCHI
{
    //vC[ͺGΜ£UΝΝΙNόAEoΜ
    public class LongAttackArea : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("eΜXNvgπζΎ")]
        Enemy Parent_Enemy = null;

        //GΜUΝΝΦΜNό»θ
        private void OnTriggerEnter(Collider colision)
        {
            //Playerͺ£UΝΰΙNό
            if (colision.CompareTag("Player"))
            {
                Parent_Enemy.isLongAttacks = true;
                //£UXe[gΙΟX
                Parent_Enemy.LongAttack(); 
            }
        }

        //GΜ£U»θ©ηΜEoΜ»θ
        private void OnTriggerExit(Collider colision)
        {
            //Playerͺ£UΝOΙEo
            if (colision.CompareTag("Player"))
            {
                Parent_Enemy.isLongAttacks = false;
                Parent_Enemy.SetDiscoverState();
            }
        }
    }
}
