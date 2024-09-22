using UnityEngine;

namespace Mamavon.Data
{
    public abstract class Player2DGroundData : PlayerCastGroundBase
    {
        public abstract bool CheckGround2D(Transform obj, out RaycastHit2D hit);
        public abstract void DrawGroundCheckGizmo2D(Transform obj, bool isGrounded);
    }
}