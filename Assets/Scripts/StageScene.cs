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

        [SerializeField]
        [Tooltip("プレイヤーオブジェクトを指定")]
        public GameObject player;

        // プレイヤーのポジション
        private Vector3 playerPosition;

        [SerializeField]
        [Tooltip("スサノオオブジェクトを指定")]
        public GameObject enemy;

        // スサノオのポジション
        private Vector3 enemyPosition;

        // アニメーター
        private Animator animator;

        // プロローグ中はオン
        public bool prologue = true;

        private bool clear = false;

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

        private void Update()
        {
            if (playerhp == 0)
            {
                ui.GameOver();
            }

            if (enemyHp <= 0)
            {
                Clear();
            }
        }

        private void FixedUpdate()
        {
            /*
            if(enemy != null)
            {
                playerPosition = player.GetComponent<Transform>().position;
                enemyPosition = enemy.GetComponent<Transform>().position;

                Vector3 difference = playerPosition - enemyPosition;
                float differenceX = difference.x;
                float differenceY = difference.y;

                //if(Mathf.Abs(differenceX) = )
            }
            */
        }

        public void Heal(Vector3 EffectTransform)
        {
            Instantiate(healObject, EffectTransform, Quaternion.identity); //パーティクル用ゲームオブジェクト生成
            if(playerhp < 6)
            {
                playerhp += 1;
            }
        }

        // Damageが呼び出されたらHPが1減る
        public void Damage()
        {
            if (playerhp > 0)
            {
                playerhp--;
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
                ui.ClosePrologue();
            }
            page++;
        }

        /// <summary>
        /// 回復の説明用UIを表示
        /// </summary>
        public void OnHealExplanation()
        {
            ui.DisplayHealExplanation();
        }

        public async void Clear()
        {
            if(!clear)
            {
                clear = true;
                await Task.Delay(5000);
                ui.StageClear();
                player.GetComponent<MoveBehaviourScript>().SetClearState();
                Debug.Log("クリア");
            }
        }

        /// <summary>
        /// ヒットストップを表現
        /// </summary>
        /// <param name="f">ヒットストップ時の時間の速さ</param>
        /// <param name="i">ヒットストップ時の時間の長さ</param>
        public async void HitStop(float f, int i)
        {
            Time.timeScale = f;
            await Task.Delay(i);
            Time.timeScale = 1.0f;
        }
    }
}
