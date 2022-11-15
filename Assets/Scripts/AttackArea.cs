using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�v���C���[���G�̍U���͈͂ɐN���A�E�o���̏���
public class AttackArea : MonoBehaviour
{
    //�e�̃X�N���v�g���擾
    [SerializeField]
    Enemy Parent_Enemy = null;

    [SerializeField]
    float Transition_time;
    
    //�^�[�Q�b�g�̍U���͈͂ւ̐N������
    private void OnTriggerEnter(Collider colision)
    {
        //Player���U���͈͓��ɐN��
        if (colision.CompareTag("Player"))
        {
            Parent_Enemy.AttackArea = true;
            Parent_Enemy.SetAttackReadyState();            //Enemy�̃X�e�[�g���U�������ɕύX
        }
    }
    //�^�[�Q�b�g���U�����肩��̒E�o�̔���
    private void OnTriggerExit(Collider colision)
    {
        //Player���U���͈͊O�ɒE�o
        if (colision.CompareTag("Player"))
        {
            Parent_Enemy.AttackArea = false;
            Parent_Enemy.SetDiscoverState();               //Enemy�̃X�e�[�g��Player���������ɕύX
        }        
    }
}
