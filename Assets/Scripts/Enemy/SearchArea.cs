using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OROCHI
{
    //�v���C���[���G�̍��G�͈͓��͈͂ɐN���A�E�o���̏���
    public class SearchArea : MonoBehaviour
    {
        //�e��Enemy�X�N���v�g�̎擾
        [SerializeField]
        Enemy parent_enemy = null;

        //�^�[�Q�b�g�̍��G�͈͓��̐N������
        private void OnTriggerEnter(Collider colision)
        {

            if (colision.CompareTag("Player"))
            {
                parent_enemy.isSearch = true;
                parent_enemy.SetDiscoverState();
            }
        }
        //�^�[�Q�b�g�̍��G�͈͊O�̒E�o����
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
