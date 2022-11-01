using UnityEngine;

// プレイヤーが取得できるアイテムを表します。
public class Bone : MonoBehaviour
{

    //回復エフェクトの指定
    [SerializeField]
    public GameObject HealObject;

    [SerializeField]
    MoveBehaviourScript Move_Behaviour_Script;
    // トリガー内に他のオブジェクトが侵入してきた際に呼び出されます。
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Vector3 EffectPosition = collision.contacts[0].point;
            // 骨を取ったら回復
            StageScene.Instance.Heal(EffectPosition);
            Destroy(gameObject);
        }
    }
}