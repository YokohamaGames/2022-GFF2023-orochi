using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OROCHI
{
    //�v���C���[���G�̍U���͈͂ɐN���A�E�o���̏���
    public class AttackArea : MonoBehaviour
    {
        //�e�̃X�N���v�g���擾
        [SerializeField]
        Enemy parentenemy = null;

        [SerializeField]
        float transitiontime;

        //�^�[�Q�b�g�̍U���͈͂ւ̐N������
        private void OnTriggerEnter(Collider colision)
        {
            //Player���U���͈͓��ɐN��
            if (colision.CompareTag("Player"))
            {
                parentenemy.isAttacks = true;
                parentenemy.SetAttackReadyState();            //Enemy�̃X�e�[�g���U�������ɕύX
            }
        }
        //�^�[�Q�b�g���U�����肩��̒E�o�̔���
        private void OnTriggerExit(Collider colision)
        {
            //Player���U���͈͊O�ɒE�o
            if (colision.CompareTag("Player"))
            {
                parentenemy.isAttacks = false;
                parentenemy.SetMoveState();               //Enemy�̃X�e�[�g��Player���������ɕύX
            }
        }
    }
}
