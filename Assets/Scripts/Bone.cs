using UnityEngine;

// プレイヤーが取得できるアイテムを表します。
public class Bone : MonoBehaviour
{

    //回復エフェクトの指定
    [SerializeField]
    public GameObject HealObject;                                             //回復エフェクトのインスタンスの指定

    [SerializeField]
    MoveBehaviourScript Move_Behaviour_Script;                                //関数呼び出し元のスクリプトの指定
    // トリガー内に他のオブジェクトが侵入してきた際に呼び出されます。
    void OnCollisionEnter(Collision collision)
    {
        //プレイヤーと接触判定
        if (collision.gameObject.name == "Player")
        {
            
            Vector3 EffectPosition = collision.contacts[0].point;              //プレイヤーの座標を取得
            EffectPosition.y = 0;                                             //y座標に-1を代入して足元にエフェクトを配置
            // 骨を取ったら回復
            StageScene.Instance.Heal(EffectPosition);
            Destroy(gameObject);                                               //骨の破壊
        }
    }
}