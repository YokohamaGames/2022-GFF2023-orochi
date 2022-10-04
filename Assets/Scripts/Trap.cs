using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public GameObject shellPrefab;
    public AudioClip sound;
    private int count;

   

    void Update()
    {
        count += 1;

        //ˆê’èŠÔŠu‚Å’e‚ğ”­Ë‚·‚é
        if (count % 480 == 0)
        {
            GameObject shell = Instantiate(shellPrefab, transform.position, Quaternion.identity);
            Rigidbody shellRb = shell.GetComponent<Rigidbody>();

            // ’e‘¬‚ğİ’è
            shellRb.AddForce(transform.forward * 1500);

           

            // ”­Ë‰¹‚ğo‚·
            // AudioSource.PlayClipAtPoint(sound, transform.position);

            // ‚T•bŒã‚É–C’e‚ğ”j‰ó‚·‚é
            Destroy(shell, 5.0f);

            
        }
    }

    
}
