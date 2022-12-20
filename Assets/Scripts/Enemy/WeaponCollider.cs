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
            //�Փ˂����I�u�W�F�N�g��Bullet(��C�̒e)�������Ƃ�
            if (collision.gameObject.CompareTag("Player"))
            {
                Debug.Log("�G�ƒe���Փ˂��܂����I�I�I");
                GameObject damege = Instantiate(damageeffect, this.transform.position, Quaternion.identity);
                Destroy(damege, 1.5f);
            }
        }
    }
}
