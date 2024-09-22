using UnityEngine;

namespace Mamavon.Data
{
    [CreateAssetMenu(fileName = "BoxCast2DGroundData", menuName = "Mamavon Packs/ScriptableObject/Object Scripts/BoxCast2DGroundData")]
    public class BoxCastGroundData : Player2DGroundData
    {
        [SerializeField] private Vector2 scale;
        public Vector2 Scale
        {
            set
            {
                scale = new Vector2(value.x - size_margine,
                                   value.y - size_margine);
            }
        }

        /// <summary>
        /// �v���C���[�̃I�u�W�F�N�g���n�ʂɐڂ��Ă��邩�ǂ������`�F�b�N���܂��B
        /// BoxCast2D���g�p���Ă��܂�
        /// </summary>
        /// <param name="obj">�`�F�b�N����Transform�I�u�W�F�N�g</param>
        /// <param name="hit">�n�ʃ`�F�b�N�̌��ʂƂ��Ă�RaycastHit2D�I�u�W�F�N�g</param>
        /// <returns>�I�u�W�F�N�g���n�ʂɐڂ��Ă���ꍇ��true�A����ȊO�̏ꍇ��false</returns>
        public override bool CheckGround2D(Transform obj, out RaycastHit2D hit)
        {
            // BoxCast2D���g�p���Ēn�ʃ`�F�b�N�����s���܂��B
            hit = Physics2D.BoxCast(
                obj.position,                                // Obj�̈ʒu
                scale,                                       // �{�b�N�X�̃T�C�Y
                obj.rotation.eulerAngles.z,                  // �{�b�N�X�̉�]�p�x
                Vector2.down,                                // BoxCast�̕����i�������j
                base.length,                                 // BoxCast�̍ő勗��
                base.groundLayer                             // �Փ˂����o���郌�C���[�}�X�N
            );

            return hit.collider != null;
        }

        public override void DrawGroundCheckGizmo2D(Transform obj, bool isGrounded)
        {
            if (!base.isDraw)
                return;

            Color gizmoColor = isGrounded ? Color.green : Color.red;
            gizmoColor.a = base.gizmoAlpha; // �����x��ݒ�

            Gizmos.color = gizmoColor;

            Vector2 endPosition = (Vector2)obj.position + Vector2.down * base.length;

            // �I���ʒu�̋�`��`��
            Gizmos.DrawWireCube(endPosition, scale);
        }
    }
}