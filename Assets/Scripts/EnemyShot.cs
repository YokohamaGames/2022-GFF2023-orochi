using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    public GameObject shellPrefab;
    public AudioClip sound;
    private int count;

   

    void Update()
    {
       /* count += 1;

        //���Ԋu�Œe�𔭎˂���
        //if (count % 480 == 0)
        if(Input.GetKey(KeyCode.F))
        {
            GameObject shell = Instantiate(shellPrefab, transform.position, Quaternion.identity);
            Rigidbody shellRb = shell.GetComponent<Rigidbody>();

            // �e����ݒ�
            shellRb.AddForce(transform.forward * 1500);

           

            // ���ˉ����o��
            // AudioSource.PlayClipAtPoint(sound, transform.position);

            // �T�b��ɖC�e��j�󂷂�
            Destroy(shell, 5.0f);

            
        }*/
    }

    public void Enemyshot()
    {
        GameObject shell = Instantiate(shellPrefab, transform.position, Quaternion.identity);
        Rigidbody shellRb = shell.GetComponent<Rigidbody>();
        Debug.Log("����");
        // �e����ݒ�
        shellRb.AddForce(transform.forward * 1500);
    }

    
}
