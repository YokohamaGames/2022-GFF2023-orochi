using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//vC[ͺGΜUΝΝΙNόAEoΜ
public class AttackArea : MonoBehaviour
{
    //eΜXNvgπζΎ
    [SerializeField]
    Enemy Parent_Enemy = null;

    [SerializeField]
    float Transition_time;
    
    //^[QbgΜUΝΝΦΜNό»θ
    private void OnTriggerEnter(Collider colision)
    {
        //PlayerͺUΝΝΰΙNό
        if (colision.CompareTag("Player"))
        {
            Parent_Enemy.isAttacks = true;
            Parent_Enemy.SetAttackReadyState();            //EnemyΜXe[gπUυΙΟX
        }
    }
    //^[QbgͺU»θ©ηΜEoΜ»θ
    private void OnTriggerExit(Collider colision)
    {
        //PlayerͺUΝΝOΙEo
        if (colision.CompareTag("Player"))
        {
            Parent_Enemy.isAttacks = false;
            Parent_Enemy.SetMoveState();               //EnemyΜXe[gπPlayerπ©Έ€ΙΟX
        }        
    }
}
