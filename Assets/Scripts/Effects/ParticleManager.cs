using UnityEngine;

public class ParticleManager : MonoBehaviour
{

    /// <summary>
    /// パーティクルの再生が終わった時に実行される
    /// </summary>
    /// 
    [SerializeField]
    GameObject Parent;
    private void OnParticleSystemStopped()
    {
        Debug.Log("パーティクル終わったよ！");
        Destroy(Parent);
    }

}