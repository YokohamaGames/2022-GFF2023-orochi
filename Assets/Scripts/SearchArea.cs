using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchArea : MonoBehaviour
{   
    //eΜEnemyXNvgΜζΎ
    [SerializeField]
    Enemy Parent_Enemy = null;

    //^[QbgΜυGΝΝΰΜNό»θ
    private void OnTriggerEnter(Collider colision)
    {
        
        if (colision.CompareTag("Player"))
        {
            Parent_Enemy.SearchArea = true;
            Parent_Enemy.SetDiscoverState();
        }
    }
    //^[QbgΜυGΝΝOΜEo»θ
    private void OnTriggerExit(Collider colision)
    {
        if (colision.CompareTag("Player"))
        {
            Parent_Enemy.SearchArea = false;
            Parent_Enemy.SetIdleState();
        }
    }
}
