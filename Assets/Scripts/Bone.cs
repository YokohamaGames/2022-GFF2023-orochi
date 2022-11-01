using UnityEngine;

// �v���C���[���擾�ł���A�C�e����\���܂��B
public class Bone : MonoBehaviour
{

    //�񕜃G�t�F�N�g�̎w��
    [SerializeField]
    public GameObject HealObject;

    [SerializeField]
    MoveBehaviourScript Move_Behaviour_Script;
    // �g���K�[���ɑ��̃I�u�W�F�N�g���N�����Ă����ۂɌĂяo����܂��B
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Vector3 EffectPosition = collision.contacts[0].point;
            // ������������
            StageScene.Instance.Heal(EffectPosition);
            Destroy(gameObject);
        }
    }
}