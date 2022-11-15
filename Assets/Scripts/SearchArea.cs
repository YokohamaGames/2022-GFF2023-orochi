using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�v���C���[���G�̍��G�͈͓��͈͂ɐN���A�E�o���̏���
public class SearchArea : MonoBehaviour
{   
    //�e��Enemy�X�N���v�g�̎擾
    [SerializeField]
    Enemy Parent_Enemy = null;

    //�^�[�Q�b�g�̍��G�͈͓��̐N������
    private void OnTriggerEnter(Collider colision)
    {
        
        if (colision.CompareTag("Player"))
        {
            Parent_Enemy.SearchArea = true;
            //Parent_Enemy.SetDiscoverState();
        }
    }
    //�^�[�Q�b�g�̍��G�͈͊O�̒E�o����
    private void OnTriggerExit(Collider colision)
    {
        if (colision.CompareTag("Player"))
        {
            Parent_Enemy.SearchArea = false;
            //Parent_Enemy.SetIdleState();
        }
    }
}
