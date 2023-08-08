using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OROCHI
{
    public class Explanation : MonoBehaviour
    {
        // オブジェクトのコライダー
        private Collider col;

        void Start()
        {
            col = GetComponent<Collider>();
        }

        public void OnTriggerEnter(Collider other)
        {
            // プレイヤーが通過したら
            if(other.CompareTag("Player"))
            {
                // 回復の説明を表示
                StageScene.Instance.OnHealExplanation();
            }
        }
    }
}
