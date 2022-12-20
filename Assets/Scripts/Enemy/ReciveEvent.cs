using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace OROCHI
{
    //アニメーション中に実行する関数を受け取る
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
