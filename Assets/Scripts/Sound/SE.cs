using UnityEngine;
using UnityEngine.UI;

namespace OROCHI
{
    public class SE : MonoBehaviour
    {
        // ÇªÇÍÇºÇÍÇÃâπê∫ÇéQè∆
        public AudioClip OpenUi = null;
        public AudioClip CloseUi = null;
        public AudioClip swordattack = null;
        public AudioClip swordattack2 = null;
        public AudioClip fire = null;
        public AudioClip damage = null;
        public AudioClip swordswing = null;
        public AudioClip chargehit = null;
        public AudioClip damaged = null;
        public AudioClip fireattack = null;
        public AudioClip firedamaged = null;
        public AudioClip charge = null;
        public AudioClip changeSmoke = null;


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

        #region ÇªÇÍÇºÇÍÇÃSE
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

        public void ChargeHit()
        {
            Se.PlayOneShot(chargehit);
        }

        public void Damaged()
        {
            Se.PlayOneShot(damaged);
        }

        public void FireSE()
        {
            Se.PlayOneShot(fireattack);
        }

        public void FireDamaged()
        {
            Se.PlayOneShot(firedamaged);
        }

        public void Charge()
        {
            Se.PlayOneShot(charge);
        }

        public void PlaySound(AudioClip sound)
        {
            Se.PlayOneShot(sound);
        }

        public void Change()
        {
            Se.PlayOneShot(changeSmoke);
        }
        #endregion
    }
}