// PlayerFollowCamera.cs
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using Cinemachine;

// プレイヤー追従カメラ
public class CameraSample : MonoBehaviour
{
    public Vector2 rotationSpeed = new Vector2(180, 180);

    CinemachineVirtualCamera vCam = null;
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
                Vector3 targetEulerAngles = target.rotation.eulerAngles;

                targetEulerAngles.y += cameraRotationInput.x * rotationSpeed.y * Time.fixedDeltaTime;

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