// PlayerFollowCamera.cs
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using Cinemachine;

namespace OROCHI
{
    // プレイヤー追従カメラ
    public class MainCamera : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("一秒間で振り向ける角度")]
        private Vector2 rotationSpeed = new Vector2(180, 180);

        // カメラの振り向き入力値
        private Vector2 cameraRotationInput = Vector2.zero;

        [SerializeField]
        [Tooltip("バーチャルカメラを指定")]
        CinemachineVirtualCamera vCam = null;

        private void Start()
        {
            vCam = GetComponent<CinemachineVirtualCamera>();
        }

        private void FixedUpdate()
        {
            if (vCam != null)
            {
                // VirtualカメラのFollowをターゲットとして座標を獲得
                Transform target = vCam.Follow;
                if (target != null)
                {
                    // ターゲットの角度をオイラー角度(x, y, z)で取得
                    Vector3 targetEulerAngles = target.rotation.eulerAngles;

                    // y軸の回転を変える
                    targetEulerAngles.y += cameraRotationInput.x * rotationSpeed.y * Time.fixedDeltaTime;
                    // オイラー角度をクオータニオンに変換して追跡ターゲットの回転を変える
                    target.transform.rotation = Quaternion.Euler(targetEulerAngles);

                }
            }

        }

        /// <summary>
        /// カメラの向きを調整
        /// </summary>
        /// <param name="input">プレイヤーの入力値</param>
        void Look(Vector2 input)
        {
            // 入力値を代入
            cameraRotationInput = input;
        }

        /// <summary>
        /// プレイヤーの入力を受け取る
        /// </summary>
        public void Onlook(InputAction.CallbackContext context)
        {
            Look(context.ReadValue<Vector2>());
        }
    }

}