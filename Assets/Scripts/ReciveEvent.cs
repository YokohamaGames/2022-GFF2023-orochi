using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//アニメーション終了を検知
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
