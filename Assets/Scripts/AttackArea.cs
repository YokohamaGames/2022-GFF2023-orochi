using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤーが敵の攻撃範囲に侵入、脱出時の処理
public class AttackArea : MonoBehaviour
{
    //親のスクリプトを取得
    [SerializeField]
    Enemy Parent_Enemy = null;

    [SerializeField]
    float Transition_time;
    
    //ターゲットの攻撃範囲への侵入判定
    private void OnTriggerEnter(Collider colision)
    {
        //Playerが攻撃範囲内に侵入
        if (colision.CompareTag("Player"))
        {
            Parent_Enemy.AttackArea = true;
            Parent_Enemy.SetAttackReadyState();            //Enemyのステートを攻撃準備に変更
        }
    }
    //ターゲットが攻撃判定からの脱出の判定
    private void OnTriggerExit(Collider colision)
    {
        //Playerが攻撃範囲外に脱出
        if (colision.CompareTag("Player"))
        {
            Parent_Enemy.AttackArea = false;
            Parent_Enemy.SetDiscoverState();               //EnemyのステートをPlayerを見失うに変更
        }        
    }
}
