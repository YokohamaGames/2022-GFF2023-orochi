// PlayerFollowCamera.cs
using UnityEngine;

// �v���C���[�Ǐ]�J����
public class EnemyChaseCamera : MonoBehaviour
{
    [SerializeField] private Transform player;          // �����Ώۃv���C���[

    [SerializeField] private float distance = 15.0f;    // �����Ώۃv���C���[����J�����𗣂�����
    [SerializeField] private Quaternion vRotation;      // �J�����̐�����](�����낵��])
    [SerializeField] public Quaternion hRotation;      // �J�����̐�����]
    

    void Start()
    {
        // ��]�̏�����
        vRotation = Quaternion.Euler(30, 180, 0);         // ������](X�������Ƃ����])�́A30�x�����낷��]
        hRotation = Quaternion.identity;                // ������](Y�������Ƃ����])�́A����]
        transform.rotation = hRotation * vRotation;     // �ŏI�I�ȃJ�����̉�]�́A������]���Ă��琅����]���鍇����]

        // �ʒu�̏�����
        // player�ʒu���狗��distance������O�Ɉ������ʒu��ݒ肵�܂�
        transform.position = player.position - transform.rotation * Vector3.forward * distance;
    }

    void LateUpdate()
    {
        // �J�����̈ʒu(transform.position)�̍X�V
        // player�ʒu���狗��distance������O�Ɉ������ʒu��ݒ肵�܂�
        transform.position = player.position - transform.rotation * Vector3.forward * distance;
    }
}