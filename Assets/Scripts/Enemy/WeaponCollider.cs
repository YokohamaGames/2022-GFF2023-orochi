using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OROCHI
{
    public class WeaponCollider : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("���킪���������Ƃ��̃G�t�F�N�g���w��")]
        private GameObject damageeffect;

        // Update is called once per frame
        private void OnCollisionEnter(Collision collision)
        {
            //�Փ˂����I�u�W�F�N�g��Bullet(��C�̒e)�������Ƃ�
            if (collision.gameObject.CompareTag("Player"))
            {
                GameObject damege = Instantiate(damageeffect, this.transform.position, Quaternion.identity);
                Destroy(damege, 0.5f);
            }
        }
    }
}
