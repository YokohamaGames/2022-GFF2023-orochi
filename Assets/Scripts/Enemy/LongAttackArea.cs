using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OROCHI
{
    //�v���C���[���G�̉������U���͈͂ɐN���A�E�o���̏���
    public class LongAttackArea : MonoBehaviour
    {
        //�e�̃X�N���v�g���擾
        [SerializeField]
        Enemy Parent_Enemy = null;

        [SerializeField]
        float Transition_time;

        //�G�̍U���͈͂ւ̐N������
        private void OnTriggerEnter(Collider colision)
        {
            //Player���������U���͓��ɐN��
            if (colision.CompareTag("Player"))
            {
                Parent_Enemy.isLongAttacks = true;
                Parent_Enemy.LongAttack();                  //�������U���X�e�[�g�ɕύX
            }
        }
        //�G�̉������U�����肩��̒E�o�̔���
        private void OnTriggerExit(Collider colision)
        {
            //Player���������U���͊O�ɒE�o
            if (colision.CompareTag("Player"))
            {
                Parent_Enemy.isLongAttacks = false;
                Parent_Enemy.SetDiscoverState();
            }
        }
    }
}
