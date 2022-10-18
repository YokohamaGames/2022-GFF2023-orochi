// PlayerFollowCamera.cs
using UnityEngine;

// �v���C���[�Ǐ]�J����
public class CameraSample : MonoBehaviour
{
    [SerializeField] private Transform player;          // �����Ώۃv���C���[

    [SerializeField] private Vector3 offset;     // player�Ƃ̋���

    [SerializeField]
    [Tooltip("X���W�����ɉ�]�����܂�")]
    private float rotate;
    private void Start()
    {
        
    }

    private void Update()
    {
        // �V����Transform����
        transform.position = player.transform.position + offset;
        //transform.rotation = Quaternion.identity;

        // transform���擾
        Transform myTransform = this.transform;

        // ���[�J�����W����ɉ�]
        Vector3 localAngle = myTransform.localEulerAngles;
        localAngle.x = rotate;
        myTransform.localEulerAngles = localAngle;

    }
}