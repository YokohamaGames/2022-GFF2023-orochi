using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    //Enemy�̎q�I�u�W�F�N�g�̃A�j���[�V�����̎擾
    Animator player_animation;

    static readonly int isAttackId = Animator.StringToHash("isAttack");

    void Start()
    {
        player_animation = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void SetAnimation()
    {
        player_animation.SetTrigger(isAttackId);
    }
}
