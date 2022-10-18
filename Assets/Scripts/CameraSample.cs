// PlayerFollowCamera.cs
using UnityEngine;

// プレイヤー追従カメラ
public class CameraSample : MonoBehaviour
{
    [SerializeField] private Transform player;          // 注視対象プレイヤー

    [SerializeField] private Vector3 offset;     // playerとの距離

    private void Start()
    {
        
    }

    private void Update()
    {
        // 新しいTransformを代入
        transform.position = player.transform.position + offset;
        //transform.rotation = Quaternion.identity;
    }
}