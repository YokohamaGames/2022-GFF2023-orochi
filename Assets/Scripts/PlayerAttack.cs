using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    int playeratk;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("enemy"))
        {
            collision.GetComponent<Enemy>().EnemyDamage(playeratk);
        }
    }
}
