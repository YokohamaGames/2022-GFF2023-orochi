using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
   
   
    //敵の追跡時歩行スピードを設定します
    [SerializeField]
    private float ChaseSpeed = 5;
    [SerializeField]
    private float AttackSpeed = 1;
    //現在の敵の歩行スピード
    float speed;
    //追跡機能を実装します
    [SerializeField]
    private NavMeshAgent enemy;
    //追跡目標を設定します
    [SerializeField]
    private GameObject target;
    

    public bool SearchArea = false;

    public bool AttackArea = false;

    enum EnemyState
    {
        //待機状態
        Stay,

        //移動状態
        Move,

        //攻撃状態
        Attack,

        //回避状態
        Escape,


    }

      EnemyState currentState = EnemyState.Stay;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        {
            // 状態ごとの分岐処理
            switch (currentState)
            {
                case EnemyState.Stay:
                    UpdateForStay();
                    break;
                case EnemyState.Move:
                    UpdateForMove();
                    break;
                case EnemyState.Attack:
                    UpdateForAttack();
                    break;
                default:
                    break;
            }

            //索敵範囲外なら立ち止まる
            if (!SearchArea)
            {
                enemy.updatePosition = false;                                  //動きを止める
                enemy.updateRotation = false;                                  //回転を止める
                enemy.destination = enemy.transform.position;                  //目標地点を自分自身に設定し瞬間移動の防止
            }
            //索敵範囲内ならターゲットを追跡する
            else if (target != null && SearchArea)
            {
                enemy.updatePosition = true;                                   //目標地点へ動きを始める
                enemy.updateRotation = true;                                   //目標地点へ回転を始める
                enemy.destination = target.transform.position;                 //目標地点をターゲットに設定
            }
            
        }
        //索敵範囲と攻撃範囲の中にいるとき攻撃モード
        if (SearchArea && AttackArea)
        {
            Debug.Log("攻撃範囲内");
        }
        //索敵範囲内のみの時追跡モード
        else if(SearchArea)
        {
            Debug.Log("索敵範囲内");
        }
        //索敵範囲外の時Stay
        else if (!SearchArea)
        {
            Debug.Log("索敵範囲外");
        }
        
        



        
    }

    public void SetStayState()
    {
        currentState = EnemyState.Stay;
        speed = 0;
    }

    public void SetMoveState()
    {
        currentState = EnemyState.Move;
        speed = ChaseSpeed;
    }
    public void SetAttackState()
    {
        currentState = EnemyState.Attack;
        speed = AttackSpeed;
    }
    public void SetEscapeState()
    {

    }
    void UpdateForStay()
    {
        //Debug.Log("待機中");
    }

    void UpdateForMove()
    {

    }

    void UpdateForAttack()
    {
        enemy.updatePosition = true;
        enemy.updateRotation = true;
        enemy.destination = target.transform.position;
    }

   /* private void OnTriggerExit(Collider colision)
    {
        if (colision.CompareTag("Player"))
        {
            //Debug.Log("索敵範囲外");
            SearchArea = false;
        }
    }*/
}


