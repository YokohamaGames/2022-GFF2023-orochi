using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OROCHI
{
    public class Effect : MonoBehaviour
    {
        [Header("Effects")]
        [SerializeField]
        [Tooltip("�v���C���[���G�̌��U�����󂯂����̃_���[�W�G�t�F�N�g")]
        GameObject playerdamageeffect = null;


        public void EffectPlay(GameObject effect)
        {
            GameObject effectplay = Instantiate(effect, this.transform.position, Quaternion.identity);

            Destroy(effectplay, 1.5f);
        }

    }
}