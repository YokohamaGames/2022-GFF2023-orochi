using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OROCHI
{
    //アニメーションの時間を管理する
    public class AnimationTimeManager : MonoBehaviour
    {
        private Animator animator;
        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();
        }

        /// <summary>
        /// BlendTreeのスピードを指定
        /// </summary>
        /// <param name="speed">アニメーションスピード</param>
        public void SetAnimationSpeed(float speed)
        {
            animator.SetFloat("WalkSpeed", speed);
        }
    }
}