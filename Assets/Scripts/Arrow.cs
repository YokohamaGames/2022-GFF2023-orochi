using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField]
    private GameObject ArrowPrefab;

    [SerializeField]
    [Tooltip("�e�̊Ԋu���w�肵�܂�")]
    private float Time = 1f;

    [SerializeField]
    [Tooltip("�e�̑��x���w�肵�܂�")]
    private float Speed = 500f;

    [SerializeField]
    [Tooltip("�e�����鎞�Ԃ��w�肵�܂�")]
    private float DesTime = 1f;


    void Start()
    {
        // �w�肵�����\�b�h���A�w�肵�����ԁi�P�ʁG�b�j����A�w�肵���Ԋu�i�P�ʁG�b�j�ŌJ��Ԃ����s����B
        InvokeRepeating("Shot", 0f, Time);
    }

    void Shot()
    {
        GameObject Arrow = Instantiate(ArrowPrefab, transform.position, Quaternion.identity);
        Rigidbody rigidbody = Arrow.GetComponent<Rigidbody>();

        // �e���͎��R�ɐݒ�
        rigidbody.AddForce(transform.forward * Speed);


        // �T�b��ɖC�e��j�󂷂�
        Destroy(Arrow, DesTime);
    }
}