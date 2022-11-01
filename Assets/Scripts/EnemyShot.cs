using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    public GameObject shellPrefab;
    //public AudioClip sound;
    private int count;

   


    public void EnemyShotAttack()
    {
        GameObject shell = Instantiate(shellPrefab, transform.position, Quaternion.identity);
        Rigidbody shellRb = shell.GetComponent<Rigidbody>();
        Debug.Log("î≠éÀ");
        // íeë¨Çê›íË
        shellRb.AddForce(transform.forward * 1500);
        Destroy(shell, 1.0f);
    }

    
}
