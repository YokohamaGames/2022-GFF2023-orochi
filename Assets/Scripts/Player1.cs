using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    [SerializeField] private Vector3 velocity;              // �ړ�����
    [SerializeField] private float moveSpeed = 5.0f;        // �ړ����x

    private Vector3 latestPos;  //�O���Position

    void Start()
    {

    }

    void Update()
    {
        // WASD���͂���AXZ����(�����Ȓn��)���ړ��������(velocity)�𓾂܂�
        velocity = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
            velocity.z += 1;
        if (Input.GetKey(KeyCode.A))
            velocity.x -= 1;
        if (Input.GetKey(KeyCode.S))
            velocity.z -= 1;
        if (Input.GetKey(KeyCode.D))
            velocity.x += 1;

        // ���x�x�N�g���̒�����1�b��moveSpeed�����i�ނ悤�ɒ������܂�
        velocity = velocity.normalized * moveSpeed * Time.deltaTime;

        // �����ꂩ�̕����Ɉړ����Ă���ꍇ
        if (velocity.magnitude > 0)
        {
            // �v���C���[�̈ʒu(transform.position)�̍X�V
            // �ړ������x�N�g��(velocity)�𑫂����݂܂�
            transform.position += velocity;
        }





        Vector3 diff = transform.position - latestPos;   //�O�񂩂�ǂ��ɐi�񂾂����x�N�g���Ŏ擾
        latestPos = transform.position;  //�O���Position�̍X�V

        //�x�N�g���̑傫����0.01�ȏ�̎��Ɍ�����ς��鏈��������
        if (diff.magnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(diff); //������ύX����
        }
    }
}
