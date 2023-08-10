using UnityEngine;
using UnityEngine.EventSystems;
using System.Threading.Tasks;

namespace OROCHI
{
    public class StageScene : MonoBehaviour
    {
        // インスタンス化
        public static StageScene Instance { get; private set; }

        [SerializeField]
        [Tooltip("UIスクリプトを指定")]
        private UI ui = null;

        [Tooltip("プレイヤーのHPを指定")]
        public int playerhp;

        // プレイヤーの体力最大値
        private int MaxHP = 6;

        [Tooltip("回復エフェクトの指定")]
        public GameObject healObject;

        [Tooltip("敵のHPを設定")]
        public float enemyHp;

        [SerializeField]
        [Tooltip("プレイヤーオブジェクトを指定")]
        public GameObject player;

        [SerializeField]
        [Tooltip("スサノオオブジェクトを指定")]
        public GameObject enemy;

        // アニメーター
        private Animator animator;

        [Tooltip("プロローグ中のフラグ")]
        public bool prologue = true;

        // クリア時のフラグ
        private bool clear = false;

        // プロローグ用の変数
        int page = 0;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        /// <summary>
        /// ポーズ画面を表示
        /// </summary>
        public void ControlPauseUI()
        {
            if (Time.timeScale > 0)
            {
                ui.Control();
            }
        }

        private void Update()
        {
            // プレイヤーの体力が0なら
            if (playerhp == 0)
            {
                ui.GameOver();
            }

            // 敵の体力が0なら
            if (enemyHp <= 0)
            {
                Clear();
            }
        }

        /// <summary>
        /// プレイヤーの体力を回復させる処理
        /// </summary>
        /// <param name="EffectTransform">回復時のエフェクトの向き</param>
        public void Heal(Vector3 EffectTransform)
        {
            // 回復エフェクトを生成
            Instantiate(healObject, EffectTransform, Quaternion.identity); //パーティクル用ゲームオブジェクト生成
            if(playerhp < MaxHP)
            {
                playerhp += 1;
            }
        }

        /// <summary>
        /// Damageが呼び出されたらプレイヤーのHPが1減る
        /// </summary>
        public void Damage()
        {
            if (playerhp > 0)
            {
                playerhp--;
            }
        }

        /// <summary>
        /// プレイヤーの大きさを変化した時の処理
        /// </summary>
        public void Change()
        {
            ui.ChangeCooltime();
        }

        /// <summary>
        /// プロローグで次のページを表示する
        /// </summary>
        public void NextPage()
        {
            animator.SetTrigger("Next");

            if(page == 1)
            {
                // プロローグが終ったら閉じる
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

        /// <summary>
        /// 敵を倒した時の処理
        /// </summary>
        public async void Clear()
        {
            if(!clear)
            {
                clear = true;
                // 5秒間のアニメーションの後
                await Task.Delay(5000);
                ui.StageClear();
                player.GetComponent<MoveBehaviourScript>().SetClearState();
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
