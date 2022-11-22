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
        //衝突したオブジェクトがBullet(大砲の弾)だったとき
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("敵と弾が衝突しました！！！");
            GameObject damege = Instantiate(Damageeffect, this.transform.position, Quaternion.identity);
            Destroy(damege, 1.5f);
        }
    }
}
