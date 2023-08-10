// PlayerFollowCamera.cs
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using Cinemachine;

namespace OROCHI
{
    // �v���C���[�Ǐ]�J����
    public class MainCamera : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("��b�ԂŐU�������p�x")]
        private Vector2 rotationSpeed = new Vector2(180, 180);

        // �J�����̐U��������͒l
        private Vector2 cameraRotationInput = Vector2.zero;

        [SerializeField]
        [Tooltip("�o�[�`�����J�������w��")]
        CinemachineVirtualCamera vCam = null;

        private void Start()
        {
            vCam = GetComponent<CinemachineVirtualCamera>();
        }

        private void FixedUpdate()
        {
            if (vCam != null)
            {
                // Virtual�J������Follow���^�[�Q�b�g�Ƃ��č��W���l��
                Transform target = vCam.Follow;
                if (target != null)
                {
                    // �^�[�Q�b�g�̊p�x���I�C���[�p�x(x, y, z)�Ŏ擾
                    Vector3 targetEulerAngles = target.rotation.eulerAngles;

                    // y���̉�]��ς���
                    targetEulerAngles.y += cameraRotationInput.x * rotationSpeed.y * Time.fixedDeltaTime;
                    // �I�C���[�p�x���N�I�[�^�j�I���ɕϊ����ĒǐՃ^�[�Q�b�g�̉�]��ς���
                    target.transform.rotation = Quaternion.Euler(targetEulerAngles);

                }
            }

        }

        /// <summary>
        /// �J�����̌����𒲐�
        /// </summary>
        /// <param name="input">�v���C���[�̓��͒l</param>
        void Look(Vector2 input)
        {
            // ���͒l����
            cameraRotationInput = input;
        }

        /// <summary>
        /// �v���C���[�̓��͂��󂯎��
        /// </summary>
        public void Onlook(InputAction.CallbackContext context)
        {
            Look(context.ReadValue<Vector2>());
        }
    }

}