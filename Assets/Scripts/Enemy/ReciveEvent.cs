using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//アニメーション終了を検知
public class ReciveEvent : MonoBehaviour
{
    
   

    [SerializeField]
    UnityEvent[] edit;
    private void Start()
    {
        
    }
    public void ReceiveAnimationEvent(int id)
    {
        edit[id].Invoke();
        
    }
    public void ReceiveAnimationEvent2()
    {
        edit[1].Invoke();
    }
    public void ReceiveAnimationEvent3()
    {
        edit[2].Invoke();
    }
    public void ReceiveAnimationEvent4()
    {
        edit[3].Invoke();
        
    }

}
