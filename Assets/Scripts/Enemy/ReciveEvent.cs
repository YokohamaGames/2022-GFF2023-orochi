using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace OROCHI
{
    //�A�j���[�V�������Ɏ��s����֐����󂯎��
    public class ReciveEvent : MonoBehaviour
    {
        [SerializeField]
        UnityEvent[] edit;

        public void ReceiveAnimationEvent(int id)
        {
            edit[id].Invoke();
        }
    }
}
