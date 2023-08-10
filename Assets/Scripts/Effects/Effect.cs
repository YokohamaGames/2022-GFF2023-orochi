using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OROCHI
{
    public class Effect : MonoBehaviour
    {
        [Header("Effects")]
        [SerializeField]
        [Tooltip("プレイヤーが敵の剣攻撃を受けた時のダメージエフェクト")]
        GameObject playerdamageeffect = null;

        /// <summary>
        /// エフェクトを生成・破壊
        /// </summary>
        /// <param name="effect">出したいエフェクト</param>
        public void EffectPlay(GameObject effect)
        {
            GameObject effectplay = Instantiate(effect, this.transform.position, Quaternion.identity);

            Destroy(effectplay, 1.5f);
        }

    }
}