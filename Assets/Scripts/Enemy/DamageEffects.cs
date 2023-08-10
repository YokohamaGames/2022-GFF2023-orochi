using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OROCHI
{
    //�U����HIT���̃G�t�F�N�g����
    public class DamageEffects : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("��_���G�t�F�N�g���w��")]
        GameObject damageeffect = null;

        // �v���C���[�̍U���������������̔���
        private void OnTriggerEnter(Collider collision)
        {
            if (collision.CompareTag("Player"))
            {
                GameObject effect = Instantiate(damageeffect, this.transform.position, Quaternion.identity);
                Destroy(effect, 1.5f);
            }
        }
    }
}
