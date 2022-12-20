using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OROCHI
{
    public class EnemyShot : MonoBehaviour
    {
        public GameObject fireball;

        public void EnemyShotAttack()
        {
            GameObject fire = Instantiate(fireball, transform.position, Quaternion.identity);
            Rigidbody firespeed = fire.GetComponent<Rigidbody>();
            Debug.Log("î≠éÀ");
            // íeë¨Çê›íË
            firespeed.AddForce(transform.forward * 1500);
            Destroy(fire, 1.0f);
        }
    }
}
