using UnityEngine;

namespace OROCHI
{
    // プレイヤーが取得できるアイテムを表します。
    public class Bone : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("回復エフェクトの指定")]
        public GameObject HealObject;

        void OnCollisionEnter(Collision collision)
        {
            //プレイヤーと接触判定
            if (collision.gameObject.name == "Player")
            {
                //プレイヤーの座標を取得
                Vector3 EffectPosition = collision.contacts[0].point;
                //y座標に-1を代入して足元にエフェクトを配置
                EffectPosition.y = 0;
                
                StageScene.Instance.Heal(EffectPosition);
                Destroy(gameObject);
            }
        }
    }
}