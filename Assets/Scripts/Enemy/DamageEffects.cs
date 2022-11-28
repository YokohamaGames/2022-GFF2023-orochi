using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�U����HIT���̃G�t�F�N�g����
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
