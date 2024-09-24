using UnityEngine;

namespace Mamavon.Data
{
    public abstract class Player2DGroundData : PlayerCastGroundBase
    {
        public abstract bool CheckGround2D(Transform obj, out RaycastHit2D hit);
        public abstract void DrawGroundCheckGizmo2D(Transform obj, bool isGrounded);
        public abstract bool CheckSideWall2D(Transform obj, out RaycastHit2D hit, Vector2 direction);
        public abstract void DrawSideWallCheckGizmo2D(Transform obj, bool isWallDetected, Vector2 direction);
    }
}