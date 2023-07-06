using UnityEngine;
using UnityEngine.EventSystems;
using System.Threading.Tasks;

namespace OROCHI
{
    public class StageScene : MonoBehaviour
    {
        public static StageScene Instance { get; private set; }

        // UIを指定します。
        [SerializeField]
        private UI ui = null;

        // プレイヤーのHPを指定
        [SerializeField]
        public int playerhp;

        //回復エフェクトの指定
        [SerializeField]
        public GameObject healObject;

        //敵のHPを設定
        [SerializeField]
        public float enemyHp;

        public GameObject player;

        // アニメーター
        private Animator animator;

        // プロローグ中はオン
        public bool prologue = true;

        [SerializeField]
        [Tooltip("チュートリアルステージならオン")]
        private bool tutorial;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        public void ControlPauseUI()
        {
            if (Time.timeScale > 0)
            {
                ui.Control();
            }
        }

        public async Task Update()
        {
            if (playerhp == 0)
            {
                ui.GameOver();
            }

            if (enemyHp <= 0)
            {
                await Task.Delay(5000);
                ui.StageClear();
                player.GetComponent<MoveBehaviourScript>().ClearState();
            }
        }

        public void Heal(Vector3 EffectTransform)
        {
            Instantiate(healObject, EffectTransform, Quaternion.identity); //パーティクル用ゲームオブジェクト生成
            playerhp += 1;
        }

        // Damageが呼び出されたらHPが1減る
        public void Damage()
        {
            if(tutorial)
            {
                if (playerhp > 0)
                {
                    playerhp--;
                }
            }
        }

        public void Change()
        {
            ui.ChangeCooltime();
        }

        int page = 0;

        public void NextPage()
        {
            animator.SetTrigger("Next");

            if(page == 1)
            {
                prologue = false;
                ui.ClosePrologue();
            }
            page++;
        }
    }
}
