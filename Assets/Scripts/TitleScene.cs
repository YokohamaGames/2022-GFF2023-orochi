using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

namespace OROCHI
{
    public class TitleScene : MonoBehaviour
    {
        // Title���Q��
        [SerializeField]
        private GameObject Title = null;

        // Title�{�^�����Q��
        [SerializeField]
        private Selectable TitleButton = null;

        // ���̃V�[����ǂݍ��݉\�ȏꍇ��true�A����ȊO��false
        bool isLoadable = false;

        Animator animator;
        static readonly int FadeOutId = Animator.StringToHash("FadeOut");

        private void Start()
        {
            Title.SetActive(true);
            // StageSelect.SetActive(false);
            TitleButton.Select();
            animator = GetComponent<Animator>();
        }

        // Title���\���AStageSelect��\��
        public void Stageselect()
        {
            // SelectButton.Select();
            Title.SetActive(false);
            // StageSelect.SetActive(true);
        }

        // �Q�[�����I������
        public void Exit()
        {
            StartCoroutine(OnStart());
            Application.Quit();
        }

        // Stage��ǂݍ���
        public void LoadStage()
        {
            StartCoroutine(OnStart());
            SceneManager.LoadScene("Tutorial");

        }

        IEnumerator OnStart()
        {
            animator.SetTrigger(FadeOutId);
            yield return new WaitForSeconds(2);
            isLoadable = true;
        }

    }
}