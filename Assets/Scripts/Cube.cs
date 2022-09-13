using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformObject : MonoBehaviour
{
    //RigidBody定義
    public Rigidbody rigidbody;
    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        this.rigidbody.velocity = new Vector3(1f, 0, 0);
        //オブジェクト移動用コード
    }
}