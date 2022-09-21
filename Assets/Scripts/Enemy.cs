using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{


    //敵の追跡時歩行スピードを設定します
    [SerializeField]
    private float ChaseSpeed;
    [SerializeField]
    private float AttackSpeed = 1;
    //現在の敵の歩行スピード
    float speed = 1;
    // //追跡機能を実装します
    [SerializeField]
    private Rigidbody enemy;
    //追跡目標を設定します
    [SerializeField]
    private Transform target;

    [SerializeField]
    private Animation anim;


    //敵の回転速度を設定します
    [SerializeField]
    private float rotMax;


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



        }
        //索敵範囲と攻撃範囲の中にいるとき攻撃モード
        if (SearchArea && AttackArea)
        {
            Debug.Log("攻撃範囲内");
        }
        //索敵範囲内のみの時追跡モード
        else if (SearchArea)
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
    {  //ターゲット方向のベクトルを求める
        Vector3 vec = target.position - transform.position;

        // ターゲットの方向を向く
        // Quaternion(回転値)を取得
        Quaternion quaternion = Quaternion.LookRotation(vec);
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(vec.x, 0, vec.z)), rotMax);
        transform.Translate(Vector3.forward * speed * 0.01f); // 正面方向に移動

        // 算出した回転値をこのゲームオブジェクトのrotationに代入
        this.transform.rotation = quaternion;


    }

    void UpdateForAttack()
    {

        //ターゲット方向のベクトルを求める
        Vector3 vec = target.position - transform.position;

        // ターゲットの方向を向く
        // Quaternion(回転値)を取得
        Quaternion quaternion = Quaternion.LookRotation(vec);
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(vec.x, 0, vec.z)), rotMax);
        transform.Translate(Vector3.forward * speed * 0.01f); // 正面方向に移動

        // 算出した回転値をこのゲームオブジェクトのrotationに代入
        this.transform.rotation = quaternion;
        /*enemy.updatePosition = true;
        enemy.updateRotation = true;
        enemy.destination = target.transform.position;
        */
    }

    /*private void OnTriggerExit(Collider colision)
     {
         if (colision.CompareTag("Player"))
         {
             //Debug.Log("索敵範囲外");
             SearchArea = false;
         }
     }*/
}


