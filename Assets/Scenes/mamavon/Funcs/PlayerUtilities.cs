using UnityEngine;

namespace Mamavon.Funcs
{
    //Player Utilities�@�v���C���[���ȒP�ɓ�����悤�Ɋ�b�I�ȓ����Z�ߏグ�܂���[

    //public static class PlayerCheckGroundClass
    //{
    //    /// <summary>
    //    /// �v���C���[�̃I�u�W�F�N�g���n�ʂɐڂ��Ă��邩�ǂ������`�F�b�N���܂��B
    //    /// Sphere Cast���g�p���Ă��܂�
    //    /// </summary>
    //    /// <param name="obj">�`�F�b�N����Transform�I�u�W�F�N�g</param>
    //    /// <param name="groundCheckOffsetY">�n�ʃ`�F�b�N��Y�����I�t�Z�b�g</param>
    //    /// <param name="groundCheckRadius">�n�ʃ`�F�b�N�̋��̔��a</param>
    //    /// <param name="groundCheckDistance">�n�ʃ`�F�b�N�̍ő勗��</param>
    //    /// <param name="groundLayers">�n�ʂƂ��Ĉ������C���[�̃}�X�N</param>
    //    /// <param name="hit">�n�ʃ`�F�b�N�̌��ʂƂ��Ă�RaycastHit�I�u�W�F�N�g</param>
    //    /// <returns>�I�u�W�F�N�g���n�ʂɐڂ��Ă���ꍇ��true�A����ȊO�̏ꍇ��false</returns>
    //    public static bool CheckGroundSphere(this Transform obj, SphereCastGroundData scripObj, out RaycastHit hit)
    //    {
    //        // SphereCast���g�p���Ēn�ʃ`�F�b�N�����s���܂��B
    //        bool isGrounded = Physics.SphereCast(
    //            obj.position,                                           // Obj�̕���
    //            scripObj.radius - SphereCastGroundData.RADIUS_TOLERANCE, // SphereCast�̔��a - �ق�̂�����Ƃ���
    //            Vector3.down,                                           // SphereCast�̕����i�������j
    //            out hit,                                                // �Փˏ����󂯎�邽�߂�RaycastHit
    //            scripObj.length,                                        // SphereCast�̍ő勗��
    //            scripObj.groundLayer,                                   // �Փ˂����o���郌�C���[�}�X�N
    //            QueryTriggerInteraction.Ignore                          // �g���K�[�𖳎�
    //        );

    //        return isGrounded;
    //    }
    //    public static void DrawGroundCheckGizmo(Transform obj, SphereCastGroundData scripObj, bool isGrounded)
    //    {
    //        if (!scripObj.isDraw)
    //            return;

    //        Color gizmoColor = isGrounded ? Color.green : Color.red;
    //        gizmoColor.a = scripObj.gizmoAlpha; // �����x��ݒ�

    //        Gizmos.color = gizmoColor;

    //        float radius = scripObj.radius - SphereCastGroundData.RADIUS_TOLERANCE;

    //        Vector3 endPosition = obj.position + Vector3.down * scripObj.length;
    //        Gizmos.DrawSphere(endPosition, radius);
    //    }
    //    /// <summary>
    //    /// �v���C���[�̃I�u�W�F�N�g���n�ʂɐڂ��Ă��邩�ǂ������`�F�b�N���܂��B
    //    /// Box Cast���g�p���Ă��܂�
    //    /// </summary>
    //    /// <param name="obj">�`�F�b�N����Transform�I�u�W�F�N�g</param>
    //    /// <param name="scripObj">BoxCastGroundData�I�u�W�F�N�g</param>
    //    /// <param name="hit">�n�ʃ`�F�b�N�̌��ʂƂ��Ă�RaycastHit�I�u�W�F�N�g</param>
    //    /// <returns>�I�u�W�F�N�g���n�ʂɐڂ��Ă���ꍇ��true�A����ȊO�̏ꍇ��false</returns>
    //    public static bool CheckGroundCube(this Transform obj, BoxCastGroundData scripObj, out RaycastHit hit)
    //    {
    //        // BoxCast���g�p���Ēn�ʃ`�F�b�N�����s���܂��B
    //        bool isGrounded = Physics.BoxCast(
    //            obj.position,                                           // Obj�̈ʒu
    //            scripObj.scale,                                            // �{�b�N�X�̔����̃T�C�Y
    //            Vector3.down,                                           // BoxCast�̕����i�������j
    //            out hit,                                                // �Փˏ����󂯎�邽�߂�RaycastHit
    //            obj.rotation,                                           // �{�b�N�X�̉�]
    //            scripObj.length,                                        // BoxCast�̍ő勗��
    //            scripObj.groundLayer,                                   // �Փ˂����o���郌�C���[�}�X�N
    //            QueryTriggerInteraction.Ignore                          // �g���K�[�𖳎�
    //        );

    //        return isGrounded;
    //    }

    //    public static void DrawGroundCheckCube(Transform obj, BoxCastGroundData scripObj, bool isGrounded)
    //    {
    //        if (!scripObj.isDraw)
    //            return;

    //        Color gizmoColor = isGrounded ? Color.green : Color.red;
    //        gizmoColor.a = scripObj.gizmoAlpha; // �����x��ݒ�

    //        Gizmos.color = gizmoColor;

    //        Vector3 endPosition = obj.position + Vector3.down * scripObj.length;

    //        // �I���ʒu�̃L���[�u��`��
    //        Gizmos.DrawCube(endPosition, scripObj.scale);
    //    }
    //}
    internal static class CameraClass
    {
        /// <summary>
        /// �J������Transform���牽���ɉ�]���Ă��邩���āA�����input���|���鎖�ňړ�������vector���Q�b�g����
        /// </summary>
        /// <param name="cameraTransform">Camera��Transform</param>
        /// <param name="inputVector"> player�̓���{vector2}</param>
        /// <returns> Vector3�ŕԂ� �����̒l��speed���|���Ă����Έړ��ł���͂����B </returns>
        public static Vector3 CalculateMovementDirection(this Transform cameraTransform, Vector2 inputVector)
        {
            return (Quaternion.AngleAxis(cameraTransform.eulerAngles.y, Vector3.up) //�J�����̉�]
                    * new Vector3(inputVector.x, 0, inputVector.y))                //player��vector
                    .normalized;                                                    //���K������
        }
    }
    public static class TransformExtention
    {
        public static void Reset(this Transform transform)
        {
            transform.position = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }
        public static void SetPosX(this Transform transform, float x)
        {
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }
        public static void SetPosY(this Transform transform, float y)
        {
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
        }
        public static void SetPosZ(this Transform transform, float z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, z);
        }
    }
}