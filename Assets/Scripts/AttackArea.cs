using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    //親のスクリプトを取得
    [SerializeField]
    Enemy Parent_Enemy = null;
    private void Start()
    {
        
    }
    //ターゲットの攻撃範囲への侵入判定
    private void OnTriggerEnter(Collider colision)
    {
        if (colision.CompareTag("Player"))
        {
            Debug.Log("攻撃範囲内");
            Parent_Enemy.AttackArea = true;
            Parent_Enemy.SetAttackState();

        }
    }
    //ターゲットが攻撃判定からの脱出の判定
    private void OnTriggerExit(Collider colision)
    {
        if (colision.CompareTag("Player"))
        {
            Debug.Log("攻撃範囲外");
            Parent_Enemy.AttackArea = false;
           // Parent_Enemy.SearchArea = true;
           Parent_Enemy.SetMoveState();
        }        
    }

}
