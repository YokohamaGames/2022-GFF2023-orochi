using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField]
    private GameObject ArrowPrefab;

    [SerializeField]
    [Tooltip("弾の間隔を指定します")]
    private float Time = 1f;

    [SerializeField]
    [Tooltip("弾の速度を指定します")]
    private float Speed = 500f;

    [SerializeField]
    [Tooltip("弾が壊れる時間を指定します")]
    private float DesTime = 1f;


    void Start()
    {
        // 指定したメソッドを、指定した時間（単位；秒）から、指定した間隔（単位；秒）で繰り返し実行する。
        InvokeRepeating("Shot", 0f, Time);
    }

    void Shot()
    {
        GameObject Arrow = Instantiate(ArrowPrefab, transform.position, Quaternion.identity);
        Rigidbody rigidbody = Arrow.GetComponent<Rigidbody>();

        // 弾速は自由に設定
        rigidbody.AddForce(transform.forward * Speed);


        // ５秒後に砲弾を破壊する
        Destroy(Arrow, DesTime);
    }
}