using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OROCHI
{
    public class EnemyShot : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("�΋����w��")]
        private GameObject fireball;

        public void OnCollisionEnter(Collision collision)
        {
            // �v���C���[�ɓ���������I�u�W�F�N�g��j��
            if (collision.gameObject.tag == "Player")
            {
                Destroy(fireball);
            }
        }
    }
}
