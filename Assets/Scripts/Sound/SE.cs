using UnityEngine;
using UnityEngine.UI;

public class SE : MonoBehaviour
{
    // それぞれの音声を参照
    public AudioClip OpenUi = null;
    public AudioClip CloseUi = null;
    public AudioClip swordattack = null;
    public AudioClip swordattack2 = null;
    public AudioClip fire = null;
    public AudioClip damage = null;
    public AudioClip swordswing = null;




    AudioSource Se = null;

    [SerializeField]
    private Slider SeSoundValue = null;

    public static SE Instance { get; private set; }
    // Start is called before the first frame update

    private void Awake()
    {
        Instance = this;
    }

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

    public void SowrdAttack()
    {
        Se.PlayOneShot(swordattack);
    }

    public void SowrdAttack2()
    {
        Se.PlayOneShot(swordattack2);
    }

    public void Fire()
    {
        Se.PlayOneShot(fire);
    }

    public void Damage()
    {
        Se.PlayOneShot(damage);
    }

    public void SwordSwing()
    {
        Se.PlayOneShot(swordswing);
    }

}
