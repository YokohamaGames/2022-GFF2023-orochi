using UnityEngine;
using UnityEngine.UI;

namespace OROCHI
{
    public class MainSound : MonoBehaviour
    {
        // それぞれの音声を参照
        public AudioSource MainSound01 = null;

        AudioSource Mainsound = null;

        [SerializeField]
        [Tooltip("メイン音量のスライダーを設定")]
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
        /// メインBGMを再生
        /// </summary>
        public void Mainsound01()
        {
            MainSound01.Play();
        }
    }
}