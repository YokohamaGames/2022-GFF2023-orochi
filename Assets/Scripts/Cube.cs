using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformObject : MonoBehaviour
{
    //RigidBody��`
    public Rigidbody rigidbody;
    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        this.rigidbody.velocity = new Vector3(1f, 0, 0);
        //�I�u�W�F�N�g�ړ��p�R�[�h
    }
}