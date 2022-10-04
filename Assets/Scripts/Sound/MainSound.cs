using UnityEngine;
using UnityEngine.UI;

public class MainSound : MonoBehaviour
{
    // ÇªÇÍÇºÇÍÇÃâπê∫ÇéQè∆
    public AudioSource MainSound01 = null;

    AudioSource Mainsound = null;

    [SerializeField]
    private Slider MainSoundValue = null;

    // Start is called before the first frame update
    void Start()
    {

        Mainsound = GetComponent<AudioSource>();
        Mainsound.loop = true;
        Mainsound01();
        MainSoundValue.onValueChanged.AddListener(value => MainSound01.volume = value / 10);
    }

    public void Mainsound01()
    {
        MainSound01.Play();
    }
}
