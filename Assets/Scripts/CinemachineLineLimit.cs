using Cinemachine;
using UnityEngine;

public class CinemachineLineLimit : CinemachineExtension
{
    // �������ʂ�_
    [SerializeField] private Vector3 _origin = Vector3.up;

    // �����̌���
    [SerializeField] private Vector3 _direction = Vector3.right;

    // Extension�R�[���o�b�N
    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage,
        ref CameraState state,
        float deltaTime
    )
    {
        // �J�����ړ���̂ݏ��������s���邱�ƂƂ���
        if (stage != CinemachineCore.Stage.Body)
            return;

        // ���C���`
        var ray = new Ray(_origin, _direction);
        // �v�Z���ꂽ�J�����ʒu
        var point = state.RawPosition;

        // ���C��ɓ��e�����J�����ʒu���v�Z
        point -= ray.origin;
        point = Vector3.Project(point, ray.direction);
        point += ray.origin;

        // ���e�_���J�����ʒu�ɔ��f
        state.RawPosition = point;
    }

    #region DrawGizmos

    private const float GizmoLineLength = 1000;

    // �ړ��͈͂��G�f�B�^��ŕ\��(�m�F�p)
    private void OnDrawGizmos()
    {
        if (!isActiveAndEnabled) return;

        var ray = new Ray(_origin, _direction);

        Debug.DrawRay(
            ray.origin - ray.direction * GizmoLineLength / 2,
            ray.direction * GizmoLineLength
        );
    }

    #endregion
}