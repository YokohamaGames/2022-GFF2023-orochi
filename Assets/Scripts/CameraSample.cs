// PlayerFollowCamera.cs
using UnityEngine;

// �v���C���[�Ǐ]�J����
public class CameraSample : MonoBehaviour
{
    [SerializeField] private Transform player;          // �����Ώۃv���C���[

    [SerializeField] private Vector3 offset;     // player�Ƃ̋���

    private void Start()
    {
        
    }

    private void Update()
    {
        // �V����Transform����
        transform.position = player.transform.position + offset;
        //transform.rotation = Quaternion.identity;
    }
}