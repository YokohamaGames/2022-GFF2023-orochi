using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OROCHI
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("�U����")]
        int playeratk;

        [SerializeField]
        [Tooltip("�q�b�g�X�g�b�v���̑���")]
        private float hitTimeScale;
        [SerializeField]
        [Tooltip("�q�b�g�X�g�b�v���̍d������")]
        private int hitStopTime;


        private void OnTriggerEnter(Collider collision)
        {
            // �G�ɓ���������q�b�g�X�g�b�v
            if (collision.CompareTag("enemy"))
            {
                StageScene.Instance.HitStop(hitTimeScale, hitStopTime);
                collision.GetComponent<Enemy>().EnemyDamage(playeratk);
            }
        }
    }
}
