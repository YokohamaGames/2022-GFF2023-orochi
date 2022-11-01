// PlayerFollowCamera.cs
using UnityEngine;

// プレイヤー追従カメラ
public class CameraSample : MonoBehaviour
{
    [SerializeField] private Transform player;          // 注視対象プレイヤー

    [SerializeField] private Vector3 offset;     // playerとの距離

    [SerializeField]
    [Tooltip("大きい時のカメラ")]
    private Vector3 BIGoffset;
    [SerializeField]
    [Tooltip("中型の時のカメラ")]
    private Vector3 MIDOLEoffset;
    [SerializeField]
    [Tooltip("小さい時のカメラ")]
    private Vector3 SMALLoffset;

    [SerializeField]
    [Tooltip("X座標を軸に回転させます")]
    private float rotate;

    

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
        localAngle.x = rotate;
        myTransform.localEulerAngles = localAngle;

        if (StageScene.Instance.playerhp >= 5)
        {
            //offset = BIGoffset;
        }
        else if(StageScene.Instance.playerhp <= 2)
        {
            //offset = SMALLoffset;
        }
        else
        {
            //offset = MIDOLEoffset;
        }
    }

}