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
        /// <param name="player">�`�F�b�N����Transform�I�u�W�F�N�g</param>
        /// <param name="hit">�n�ʃ`�F�b�N�̌��ʂƂ��Ă�RaycastHit2D�I�u�W�F�N�g</param>
        /// <returns>�I�u�W�F�N�g���n�ʂɐڂ��Ă���ꍇ��true�A����ȊO�̏ꍇ��false</returns>
        public override bool CheckGround2D(Transform player, out RaycastHit2D hit)
        {
            RaycastHit2D[] hits = Physics2D.BoxCastAll(
                player.position,                             // Obj�̈ʒu
                scale,                                       // �{�b�N�X�̃T�C�Y
                player.rotation.eulerAngles.z,                  // �{�b�N�X�̉�]�p�x
                Vector2.down,                                // BoxCast�̕����i�������j
                base.length,                                 // BoxCast�̍ő勗��
                base.groundLayer                             // �Փ˂����o���郌�C���[�}�X�N
            );

            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].transform == player)
                    continue;

                if (hits[i].collider.isTrigger) //�g���K�[�������������x
                    continue;

                hit = hits[i]; //out�̕��ɑ������

                //hit.transform.Debuglog();

                return true;
            }

            hit = default;
            return false;
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

        /// <summary>
        /// �v���C���[�̃I�u�W�F�N�g�̉��ɕǂ����邩�ǂ������`�F�b�N���܂��B
        /// BoxCast2D���g�p���Ă��܂�
        /// </summary>
        /// <param name="obj">�`�F�b�N����Transform�I�u�W�F�N�g</param>
        /// <param name="direction">�`�F�b�N��������i�E�܂��͍��j</param>
        /// <param name="hit">�ǃ`�F�b�N�̌��ʂƂ��Ă�RaycastHit2D�I�u�W�F�N�g</param>
        /// <returns>�I�u�W�F�N�g�̉��ɕǂ�����ꍇ��true�A����ȊO�̏ꍇ��false</returns>
        public override bool CheckSideWall2D(Transform obj, out RaycastHit2D hit, Vector2 direction)
        {
            // BoxCast2D���g�p���ĕǃ`�F�b�N�����s���܂��B
            hit = Physics2D.BoxCast(
                obj.position,                                // Obj�̈ʒu
                scale,                                       // �{�b�N�X�̃T�C�Y
                obj.rotation.eulerAngles.z,                  // �{�b�N�X�̉�]�p�x
                direction,                                   // BoxCast�̕����i�E�܂��͍��j
                base.length,                           // BoxCast�̍ő勗��
                base.groundLayer                             // �Փ˂����o���郌�C���[�}�X�N
            );

            return hit.collider != null;
        }

        /// <summary>
        /// �������̕ǃ`�F�b�N�̃M�Y����`�悵�܂��B
        /// </summary>
        /// <param name="obj">�`�F�b�N����Transform�I�u�W�F�N�g</param>
        /// <param name="direction">�`�F�b�N��������i�E�܂��͍��j</param>
        /// <param name="isWallDetected">�ǂ����o���ꂽ���ǂ���</param>
        public override void DrawSideWallCheckGizmo2D(Transform obj, bool isWallDetected, Vector2 direction)
        {
            if (!base.isDraw)
                return;

            Color gizmoColor = isWallDetected ? Color.yellow : Color.cyan;
            gizmoColor.a = base.gizmoAlpha; // �����x��ݒ�

            Gizmos.color = gizmoColor;

            Vector2 endPosition = (Vector2)obj.position + direction * base.length;

            // �I���ʒu�̋�`��`��
            Gizmos.DrawWireCube(endPosition, scale);
        }
    }
}