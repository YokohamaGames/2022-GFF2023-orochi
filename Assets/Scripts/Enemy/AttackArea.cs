using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OROCHI
{
    // �v���C���[���G�̍U���͈͂ɐN���A�E�o���̏���
    public class AttackArea : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("�e�̃X�N���v�g���擾")]
        Enemy parentEnemy = null;

        /// <summary>
        /// �^�[�Q�b�g�̍U���͈͂ւ̐N������
        /// </summary>
        private void OnTriggerEnter(Collider colision)
        {
            // Player���U���͈͓��ɐN��
            if (colision.CompareTag("Player"))
            {
                parentEnemy.isAttacks = true;
                // Enemy�̃X�e�[�g���U�������ɕύX
                parentEnemy.SetAttackReadyState();
            }
        }
        // �^�[�Q�b�g���U�����肩��̒E�o�̔���
        private void OnTriggerExit(Collider colision)
        {
            // Player���U���͈͊O�ɒE�o
            if (colision.CompareTag("Player"))
            {
                parentEnemy.isAttacks = false;
                // Enemy�̃X�e�[�g��Player���������ɕύX
                parentEnemy.SetMoveState();
            }
        }
    }
}
