using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OROCHI
{
    public class EnemyShot : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("火球を指定")]
        private GameObject fireball;

        public void OnCollisionEnter(Collision collision)
        {
            // プレイヤーに当たったらオブジェクトを破壊
            if (collision.gameObject.tag == "Player")
            {
                Destroy(fireball);
            }
        }
    }
}
