using UnityEngine;

//�p�[�e�B�N�����I��������X�N���v�g
public class ParticleManager : MonoBehaviour
{

    /// <summary>
    /// �p�[�e�B�N���̍Đ����I��������Ɏ��s�����
    /// </summary>
    /// 
    [SerializeField]
    GameObject Parent;
    private void OnParticleSystemStopped()
    {
        Destroy(Parent);
    }

}