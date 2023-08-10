using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OROCHI
{
    //�A�j���[�V�����̎��Ԃ��Ǘ�����
    public class AnimationTimeManager : MonoBehaviour
    {
        private Animator animator;
        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();
        }

        /// <summary>
        /// BlendTree�̃X�s�[�h���w��
        /// </summary>
        /// <param name="speed">�A�j���[�V�����X�s�[�h</param>
        public void SetAnimationSpeed(float speed)
        {
            animator.SetFloat("WalkSpeed", speed);
        }
    }
}