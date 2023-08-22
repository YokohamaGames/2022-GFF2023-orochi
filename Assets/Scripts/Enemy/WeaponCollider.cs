using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OROCHI
{
    public class WeaponCollider : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("武器が当たったときのエフェクトを指定")]
        private GameObject damageeffect;

        // Update is called once per frame
        private void OnCollisionEnter(Collision collision)
        {
            //衝突したオブジェクトがBullet(大砲の弾)だったとき
            if (collision.gameObject.CompareTag("Player"))
            {
                GameObject damege = Instantiate(damageeffect, this.transform.position, Quaternion.identity);
                Destroy(damege, 0.5f);
            }
        }
    }
}
