using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    // Start is called before the first frame update
    //Collider collider;

    [SerializeField]
    private GameObject Damageeffect;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //�Փ˂����I�u�W�F�N�g��Bullet(��C�̒e)�������Ƃ�
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("�G�ƒe���Փ˂��܂����I�I�I");
            GameObject damege = Instantiate(Damageeffect, this.transform.position, Quaternion.identity);
            Destroy(damege, 1.5f);
        }
    }
}
