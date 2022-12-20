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
        // 1秒間で180度
        public Vector2 rotationSpeed = new Vector2(180, 180);

        [SerializeField]
        CinemachineVirtualCamera vCam = null;
        [SerializeField]
        Cinemachine3rdPersonFollow follow = null;

        private void Start()
        {
            vCam = GetComponent<CinemachineVirtualCamera>();
            if (vCam != null)
            {
                follow = vCam.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
            }
        }

        private void FixedUpdate()
        {
            if (vCam != null)
            {
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

        Vector2 cameraRotationInput = Vector2.zero;

        void Look(Vector2 input)
        {
            cameraRotationInput = input;
        }

        public void Onlook(InputAction.CallbackContext context)
        {
            Look(context.ReadValue<Vector2>());
        }
    }
}