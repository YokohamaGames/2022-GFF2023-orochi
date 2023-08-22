using UnityEngine;

namespace OROCHI
{
    // �v���C���[���擾�ł���A�C�e����\���܂��B
    public class Bone : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("�񕜃G�t�F�N�g�̎w��")]
        public GameObject HealObject;

        void OnCollisionEnter(Collision collision)
        {
            //�v���C���[�ƐڐG����
            if (collision.gameObject.name == "Player")
            {
                //�v���C���[�̍��W���擾
                Vector3 EffectPosition = collision.contacts[0].point;
                //y���W��-1�������đ����ɃG�t�F�N�g��z�u
                EffectPosition.y = 0;
                
                StageScene.Instance.Heal(EffectPosition);
                Destroy(gameObject);
            }
        }
    }
}