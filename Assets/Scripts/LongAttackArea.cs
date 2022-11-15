using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//vC[ͺGΜ£UΝΝΙNόAEoΜ
public class LongAttackArea : MonoBehaviour
{
    //eΜXNvgπζΎ
    [SerializeField]
    Enemy Parent_Enemy = null;

    [SerializeField]
    float Transition_time;

    //GΜUΝΝΦΜNό»θ
    private void OnTriggerEnter(Collider colision)
    {
        //Playerͺ£UΝΰΙNό
        if (colision.CompareTag("Player"))
        {
            Parent_Enemy.LongAttackArea = true;
            //Parent_Enemy.LongAttack();                  //£UXe[gΙΟX
        }
    }
    //GΜ£U»θ©ηΜEoΜ»θ
    private void OnTriggerExit(Collider colision)
    {
        //Playerͺ£UΝOΙEo
        if (colision.CompareTag("Player"))
        {
            Parent_Enemy.LongAttackArea = false;
            //Parent_Enemy.SetDiscoverState();
        }
    }


}
