using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OROCHI
{
    //ƒvƒŒƒCƒ„[‚ª“G‚Ì‰“‹——£UŒ‚”ÍˆÍ‚ÉN“üA’Eo‚Ìˆ—
    public class LongAttackArea : MonoBehaviour
    {
        //e‚ÌƒXƒNƒŠƒvƒg‚ğæ“¾
        [SerializeField]
        Enemy Parent_Enemy = null;

        [SerializeField]
        float Transition_time;

        //“G‚ÌUŒ‚”ÍˆÍ‚Ö‚ÌN“ü”»’è
        private void OnTriggerEnter(Collider colision)
        {
            //Player‚ª‰“‹——£UŒ‚”Í“à‚ÉN“ü
            if (colision.CompareTag("Player"))
            {
                Parent_Enemy.isLongAttacks = true;
                Parent_Enemy.LongAttack();                  //‰“‹——£UŒ‚ƒXƒe[ƒg‚É•ÏX
            }
        }
        //“G‚Ì‰“‹——£UŒ‚”»’è‚©‚ç‚Ì’Eo‚Ì”»’è
        private void OnTriggerExit(Collider colision)
        {
            //Player‚ª‰“‹——£UŒ‚”ÍŠO‚É’Eo
            if (colision.CompareTag("Player"))
            {
                Parent_Enemy.isLongAttacks = false;
                Parent_Enemy.SetDiscoverState();
            }
        }
    }
}
