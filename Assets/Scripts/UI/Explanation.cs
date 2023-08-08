using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OROCHI
{
    public class Explanation : MonoBehaviour
    {
        // �I�u�W�F�N�g�̃R���C�_�[
        private Collider col;

        void Start()
        {
            col = GetComponent<Collider>();
        }

        public void OnTriggerEnter(Collider other)
        {
            // �v���C���[���ʉ߂�����
            if(other.CompareTag("Player"))
            {
                // �񕜂̐�����\��
                StageScene.Instance.OnHealExplanation();
            }
        }
    }
}
