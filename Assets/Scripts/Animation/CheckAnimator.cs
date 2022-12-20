using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace OROCHI
{
    //�A�j���[�V�����I�����ɌĂяo����鏈��
    public class CheckAnimator : StateMachineBehaviour
    {
        //���s�������֐��̐ݒ�
        [SerializeField]
        UnityEvent UnityEvent = null;
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (stateInfo.IsName("Attack") || stateInfo.IsName("Attack2") || stateInfo.IsName("Attack3"))
            {
                UnityEvent.Invoke();
                Debug.Log("�U���I");
            }
        }
    }
}