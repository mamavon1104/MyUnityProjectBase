using UnityEngine;

namespace Mamavon.Data
{
    public abstract class Player3DGroundData : PlayerCastGroundBase
    {
        public abstract bool CheckGround(Transform obj, out RaycastHit hit);
        public abstract void DrawGroundCheckGizmo(Transform obj, bool isGrounded);
    }
}
