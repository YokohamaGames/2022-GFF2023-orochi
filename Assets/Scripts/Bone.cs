using UnityEngine;

// プレイヤーが取得できるアイテムを表します。
public class Bone : MonoBehaviour
{
    // トリガー内に他のオブジェクトが侵入してきた際に呼び出されます。
    void OnCollisionEnter(Collision collision)
    {

        // 骨を取ったら回復
        StageScene.Instance.Heal();
        Destroy(gameObject);
    }
}