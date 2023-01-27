using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OROCHI
{
    public class EnemyShot : MonoBehaviour
    {
        public GameObject fireball;

        public void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                Destroy(fireball);
            }
        }
    }
}
