using UnityEngine;

// �v���C���[���擾�ł���A�C�e����\���܂��B
public class Born : MonoBehaviour
{
    // �g���K�[���ɑ��̃I�u�W�F�N�g���N�����Ă����ۂɌĂяo����܂��B
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Project Settings->Physics 2D�ɂăv���C���[�ƃA�C�e���݂̂�
        // ���肳���悤�ɐݒ肳��Ă���B
        // ���̂��ߐN�����Ă����I�u�W�F�N�g�ɂ��āA�v���C���[�ł��邩��
        // �m�F���Ȃ��Ă����Ȃ��B
        //StageScene.Instance.AddItem(1);
        Destroy(gameObject);
        Debug.Log("��");
    }
}