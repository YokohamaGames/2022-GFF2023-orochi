using UnityEngine;

// �v���C���[���擾�ł���A�C�e����\���܂��B
public class Bone : MonoBehaviour
{

    //�񕜃G�t�F�N�g�̎w��
    [SerializeField]
    public GameObject HealObject;                                             //�񕜃G�t�F�N�g�̃C���X�^���X�̎w��

    [SerializeField]
    MoveBehaviourScript Move_Behaviour_Script;                                //�֐��Ăяo�����̃X�N���v�g�̎w��
    // �g���K�[���ɑ��̃I�u�W�F�N�g���N�����Ă����ۂɌĂяo����܂��B
    void OnCollisionEnter(Collision collision)
    {
        //�v���C���[�ƐڐG����
        if (collision.gameObject.name == "Player")
        {
            
            Vector3 EffectPosition = collision.contacts[0].point;              //�v���C���[�̍��W���擾
            EffectPosition.y = 0;                                             //y���W��-1�������đ����ɃG�t�F�N�g��z�u
            // ������������
            StageScene.Instance.Heal(EffectPosition);
            Destroy(gameObject);                                               //���̔j��
        }
    }
}