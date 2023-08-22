using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OROCHI
{
    //�v���C���[���G�̉������U���͈͂ɐN���A�E�o���̏���
    public class LongAttackArea : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("�e�̃X�N���v�g���擾")]
        Enemy Parent_Enemy = null;

        //�G�̍U���͈͂ւ̐N������
        private void OnTriggerEnter(Collider colision)
        {
            //Player���������U���͓��ɐN��
            if (colision.CompareTag("Player"))
            {
                Parent_Enemy.isLongAttacks = true;
                //�������U���X�e�[�g�ɕύX
                Parent_Enemy.LongAttack(); 
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
