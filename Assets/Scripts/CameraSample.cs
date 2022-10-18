// PlayerFollowCamera.cs
using UnityEngine;

// プレイヤー追従カメラ
public class CameraSample : MonoBehaviour
{
    [SerializeField] private Transform player;          // 注視対象プレイヤー

    [SerializeField] private Vector3 offset;     // playerとの距離

    [SerializeField] private float num;
    private void Start()
    {
        
    }

    private void Update()
    {
        // 新しいTransformを代入
        transform.position = player.transform.position + offset;
        //transform.rotation = Quaternion.identity;

        // transformを取得
        Transform myTransform = this.transform;

        // ローカル座標を基準に回転
        Vector3 localAngle = myTransform.localEulerAngles;
        localAngle.x = num;
        myTransform.localEulerAngles = localAngle;

    }
}