using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//�A�j���[�V�����I�������m
public class ReciveEvent : MonoBehaviour
{
    
   

    [SerializeField]
    UnityEvent edit;
    private void Start()
    {
        
    }
    public void ReceiveAnimationEvent()
    {
        edit.Invoke();
    }
}
