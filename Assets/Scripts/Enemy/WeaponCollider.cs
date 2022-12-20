using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OROCHI
{
    public class WeaponCollider : MonoBehaviour
    {
        // Start is called before the first frame update

        [SerializeField]
        private GameObject damageeffect;

        // Update is called once per frame
        private void OnCollisionEnter(Collision collision)
        {
            //衝突したオブジェクトがBullet(大砲の弾)だったとき
            if (collision.gameObject.CompareTag("Player"))
            {
                Debug.Log("敵と弾が衝突しました！！！");
                GameObject damege = Instantiate(damageeffect, this.transform.position, Quaternion.identity);
                Destroy(damege, 1.5f);
            }
        }
    }
}
