using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OROCHI
{
    //攻撃のHIT時のエフェクト処理
    public class DamageEffects : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("被ダメエフェクトを指定")]
        GameObject damageeffect = null;

        // プレイヤーの攻撃が当たったかの判定
        private void OnTriggerEnter(Collider collision)
        {
            if (collision.CompareTag("Player"))
            {
                GameObject effect = Instantiate(damageeffect, this.transform.position, Quaternion.identity);
                Destroy(effect, 1.5f);
            }
        }
    }
}
