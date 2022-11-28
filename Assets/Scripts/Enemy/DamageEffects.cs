using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//攻撃のHIT時のエフェクト処理
public class DamageEffects : MonoBehaviour
{
    [SerializeField]
    GameObject damageeffect = null;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameObject effect = Instantiate(damageeffect, this.transform.position, Quaternion.identity);
            Destroy(effect, 1.5f);

        }
    }
}
