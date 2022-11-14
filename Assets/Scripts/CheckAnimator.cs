using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class CheckAnimator : StateMachineBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    UnityEvent UnityEvent = null;
     override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(stateInfo.IsName("Attack") || stateInfo.IsName("Attack2") || stateInfo.IsName("Attack3"))
        {
            UnityEvent.Invoke();
            Debug.Log("çUåÇÅI");
        }
    }
}
