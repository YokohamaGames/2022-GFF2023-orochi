using UnityEngine;

public class ParticleManager : MonoBehaviour
{

    /// <summary>
    /// �p�[�e�B�N���̍Đ����I��������Ɏ��s�����
    /// </summary>
    private void OnParticleSystemStopped()
    {
        Debug.Log("�p�[�e�B�N���I�������I");
        Destroy(gameObject);
    }

}