using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OROCHI
{
    //�v���C���[���G�̍��G�͈͓��͈͂ɐN���A�E�o���̏���
    public class SearchArea : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("�e��Enemy�X�N���v�g�̎擾")]
        Enemy parent_enemy = null;

        /// <summary>
        /// �^�[�Q�b�g�̍��G�͈͓��̐N������
        /// </summary>
        private void OnTriggerEnter(Collider colision)
        {
            if (colision.CompareTag("Player"))
            {
                parent_enemy.isSearch = true;
                parent_enemy.SetDiscoverState();
            }
        }

        /// <summary>
        /// �^�[�Q�b�g�̍��G�͈͊O�̒E�o����
        /// </summary>
        private void OnTriggerExit(Collider colision)
        {
            if (colision.CompareTag("Player"))
            {
                parent_enemy.isSearch = false;
                parent_enemy.SetIdleState();
            }
        }
    }
}
