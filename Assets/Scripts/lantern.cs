using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lantern : MonoBehaviour
{
    MeshRenderer mesh;
    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine("Transparent");
            //Destroy(this.gameObject);
        }
    }

    IEnumerator Transparent()
    {
        for ( int i = 0; i < 255; i++)
        {
            Debug.Log("“–‚½‚Á‚½");
            mesh.material.color -= new Color32(0, 0, 0, 1);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
