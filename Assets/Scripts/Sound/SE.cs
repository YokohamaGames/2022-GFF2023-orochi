using UnityEngine;
using UnityEngine.UI;

public class SE : MonoBehaviour
{
    // ÇªÇÍÇºÇÍÇÃâπê∫ÇéQè∆
    public AudioClip OpenUi = null;
    public AudioClip CloseUi = null;
    
    AudioSource Se = null;

    [SerializeField]
    private Slider SeSoundValue = null;

    // Start is called before the first frame update
    void Start()
    {
        Se = GetComponent<AudioSource>();
        SeSoundValue.onValueChanged.AddListener(value => Se.volume = value / 10);
    }

    public void OpenUI()
    {
        Se.PlayOneShot(OpenUi);
    }
    public void CloseUI()
    {
        Se.PlayOneShot(CloseUi);
    }
}
