using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    //�e�̃X�N���v�g���擾
    [SerializeField]
    Enemy Parent_Enemy = null;

    [SerializeField]
    float Transition_time;
    private void Start()
    {
        
    }
    //�^�[�Q�b�g�̍U���͈͂ւ̐N������
    private void OnTriggerEnter(Collider colision)
    {
        if (colision.CompareTag("Player"))
        {
            //Debug.Log("�U���͈͓�");
            Parent_Enemy.AttackArea = true;
            StartCoroutine(DelayState());

            Parent_Enemy.SetAttackReadyState();

        }
    }
    //�^�[�Q�b�g���U�����肩��̒E�o�̔���
    private void OnTriggerExit(Collider colision)
    {
        if (colision.CompareTag("Player"))
        {
            //Debug.Log("�U���͈͊O");
            Parent_Enemy.AttackArea = false;
            // Parent_Enemy.SearchArea = true;
            
            StartCoroutine(DelayState());
           Parent_Enemy.SetDiscoverState();
        }        
    }

    //�w�莞�ԑ҂֐�
    IEnumerator DelayState()
    {
        yield return new WaitForSeconds(Transition_time);
    }
}
