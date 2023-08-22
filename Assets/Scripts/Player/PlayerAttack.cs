using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OROCHI
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("攻撃力")]
        int playeratk;

        [SerializeField]
        [Tooltip("ヒットストップ時の速さ")]
        private float hitTimeScale;
        [SerializeField]
        [Tooltip("ヒットストップ時の硬直時間")]
        private int hitStopTime;


        private void OnTriggerEnter(Collider collision)
        {
            // 敵に当たったらヒットストップ
            if (collision.CompareTag("enemy"))
            {
                StageScene.Instance.HitStop(hitTimeScale, hitStopTime);
                collision.GetComponent<Enemy>().EnemyDamage(playeratk);
            }
        }
    }
}
