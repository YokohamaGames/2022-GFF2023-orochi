using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace OROCHI
{
    //アニメーション終了時に呼び出される処理
    public class CheckAnimator : StateMachineBehaviour
    {
        //実行したい関数の設定
        [SerializeField]
        UnityEvent UnityEvent = null;
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (stateInfo.IsName("Attack") || stateInfo.IsName("Attack2") || stateInfo.IsName("Attack3"))
            {
                UnityEvent.Invoke();
                Debug.Log("攻撃！");
            }
        }
    }
}