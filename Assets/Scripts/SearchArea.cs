using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchArea : MonoBehaviour
{   
    //�e�̃X�N���v�g�̎擾
    [SerializeField]
    Enemy Parent_Enemy = null;
    private void Start()
    {

    }
    //�^�[�Q�b�g�̍��G�͈͓��̐N������
    private void OnTriggerEnter(Collider colision)
    {
        
        if (colision.CompareTag("Player"))
        {
            //Debug.Log("���G�͈͓�");
            Parent_Enemy.SearchArea = true;
            Parent_Enemy.SetDiscoverState();
            
        }
    }
    //�^�[�Q�b�g�̍��G�͈͊O�̒E�o����
    private void OnTriggerExit(Collider colision)
    {
        if (colision.CompareTag("Player"))
        {
            //Debug.Log("���G�͈͊O");
            Parent_Enemy.SearchArea = false;
            Parent_Enemy.SetStayState();
        }
    }

}
