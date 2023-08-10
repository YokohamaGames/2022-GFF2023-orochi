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

        /// <summary>
        /// �G�t�F�N�g�𐶐��E�j��
        /// </summary>
        /// <param name="effect">�o�������G�t�F�N�g</param>
        public void EffectPlay(GameObject effect)
        {
            GameObject effectplay = Instantiate(effect, this.transform.position, Quaternion.identity);

            Destroy(effectplay, 1.5f);
        }

    }
}