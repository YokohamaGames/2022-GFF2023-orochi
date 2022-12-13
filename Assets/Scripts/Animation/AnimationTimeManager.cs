using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//アニメーションの時間を管理する
public class AnimationTimeManager : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetAnimationSpeed(float speed)
    {
        animator.SetFloat("AttackSpeed", speed);
    }
}
