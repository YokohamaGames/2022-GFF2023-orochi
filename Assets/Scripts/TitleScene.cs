using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

namespace OROCHI
{
    public class TitleScene : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Title���Q��")]
        private GameObject Title = null;

        [SerializeField]
        [Tooltip("Title�{�^�����Q��")]
        private Selectable TitleButton = null;

        Animator animator;
        static readonly int FadeOutId = Animator.StringToHash("FadeOut");

        private void Start()
        {
            Title.SetActive(true);
            TitleButton.Select();
            animator = GetComponent<Animator>();
        }

        /// <summary>
        /// Title���\���AStageSelect��\��
        /// </summary>
        public void Stageselect()
        {
            Title.SetActive(false);
        }

        /// <summary>
        /// �Q�[�����I������
        /// </summary>
        public void Exit()
        {
            StartCoroutine(OnStart());
            Application.Quit();
        }

        /// <summary>
        /// �`���[�g���A���X�e�[�W�ɑJ��
        /// </summary>
        public void LoadStage()
        {
            StartCoroutine(OnStart());
            SceneManager.LoadScene("Tutorial");
        }

        /// <summary>
        /// �t�F�[�h�A�E�g�p�̎���
        /// </summary>
        IEnumerator OnStart()
        {
            animator.SetTrigger(FadeOutId);
            yield return new WaitForSeconds(2);
        }

    }
}