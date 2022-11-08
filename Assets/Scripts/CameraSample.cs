// PlayerFollowCamera.cs
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using Cinemachine;

// �v���C���[�Ǐ]�J����
public class CameraSample : MonoBehaviour
{
    // 1�b�Ԃ�180�x
    public Vector2 rotationSpeed = new Vector2(180, 180);

    // x����]�̉���
    public float minCameraAngle = -45;
    // x����]�̏��
    public float maxCameraAngle = 75;

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
                // �^�[�Q�b�g�̊p�x���I�C���[�p�x(x, y, z)�Ŏ擾
                Vector3 targetEulerAngles = target.rotation.eulerAngles;

                // y���̉�]��ς���
                targetEulerAngles.y += cameraRotationInput.x * rotationSpeed.y * Time.fixedDeltaTime;

                // y���Ɠ��l��x���̉�]��ς���
                //targetEulerAngles.x -= cameraRotationInput.y * rotationSpeed.x * Time.fixedDeltaTime;

                // target.rotation.eulerAngles��0�`360�̊p�x��Ԃ�����A-180�`180�ɕς���
                //if (targetEulerAngles.x > 180f)
                //{
                //    targetEulerAngles.x -= 360f;
                //}

                // ���̏�ԂŒl�𐧌�����
                //targetEulerAngles.x = Mathf.Clamp(targetEulerAngles.x, minCameraAngle, maxCameraAngle);
               
                // �I�C���[�p�x���N�I�[�^�j�I���ɕϊ����ĒǐՃ^�[�Q�b�g�̉�]��ς���
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