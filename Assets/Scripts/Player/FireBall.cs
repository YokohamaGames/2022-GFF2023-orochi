using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    private Vector3 latestPos;  //�O���Position

    public GameObject enemy;
    // Update is called once per frame
    void Update()
    {
        Vector3 diff = transform.position - latestPos;   //�O�񂩂�ǂ��ɐi�񂾂����x�N�g���Ŏ擾
        latestPos = transform.position;  //�O���Position�̍X�V

        //�x�N�g���̑傫����0.01�ȏ�̎��Ɍ�����ς��鏈��������
        if (diff.magnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(diff); //������ύX����
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            Debug.Log("�ڐG");
            enemy.GetComponent<Enemy>().EnemyDamage(5);
        }

        Destroy(this.gameObject);

    }
}