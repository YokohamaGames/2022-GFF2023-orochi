using UnityEngine;

// �v���C���[���擾�ł���A�C�e����\���܂��B
public class Bone : MonoBehaviour
{
    // �g���K�[���ɑ��̃I�u�W�F�N�g���N�����Ă����ۂɌĂяo����܂��B
    void OnCollisionEnter(Collision collision)
    {

        // ������������
        StageScene.Instance.Heal();
        Destroy(gameObject);
    }
}