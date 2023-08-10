using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OROCHI
{
    // ���C���X�e�[�W�Ɉړ�����
    public class StartPoint : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                SceneManager.LoadScene("Stage");
            }
        }
    }
}
