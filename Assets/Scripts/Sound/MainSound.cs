using UnityEngine;
using UnityEngine.UI;

namespace OROCHI
{
    public class MainSound : MonoBehaviour
    {
        // ���ꂼ��̉������Q��
        public AudioSource MainSound01 = null;

        AudioSource Mainsound = null;

        [SerializeField]
        [Tooltip("���C�����ʂ̃X���C�_�[��ݒ�")]
        private Slider MainSoundValue = null;

        // Start is called before the first frame update
        void Start()
        {
            Mainsound = GetComponent<AudioSource>();
            Mainsound.loop = true;
            Mainsound01();
            MainSoundValue.onValueChanged.AddListener(value => MainSound01.volume = value / 10);
        }

        /// <summary>
        /// ���C��BGM���Đ�
        /// </summary>
        public void Mainsound01()
        {
            MainSound01.Play();
        }
    }
}