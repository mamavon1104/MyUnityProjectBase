using UnityEngine;

namespace Mamavon.Funcs
{
    internal static class VectorClass
    {
        /// <summary>
        /// Vector3×Vector3を行います。 
        /// VectorAのx,y,zそれぞれの値をVectorBのそれぞれ値を倍にします。
        ///
        /// ret Vector3(x1 * x2,y1 * y2,z1 * z2);
        /// </summary>
        public static Vector3 MultiVecs(this Vector3 vectorA, Vector3 vectorB)
        {
            return new Vector3(vectorA.x * vectorB.x,
                               vectorA.y * vectorB.y,
                               vectorA.z * vectorB.z);
        }

        /// <summary>
        /// Vector3Int×Vector3Intを行います。 
        /// VectorAのx,y,zそれぞれの値をVectorBのそれぞれ値を倍にします。
        /// ret Vector3Int(x1 * x2,y1 * y2,z1 * z2);
        /// </summary>
        public static Vector3Int MultiVecs(this Vector3Int vectorA, Vector3Int vectorB)
        {
            return new Vector3Int(vectorA.x * vectorB.x,
                                  vectorA.y * vectorB.y,
                                  vectorA.z * vectorB.z);
        }

        /// <summary>
        /// Vector2×Vector2を行います。 
        /// 単純にx,yそれぞれの値を引数のそれぞれ値倍にします。
        /// ret Vector2(x1 * x2,y1 * y2);
        /// </summary>
        public static Vector2 MultiVecs(this Vector2 vectorA, Vector2 vectorB)
        {
            return new Vector2(vectorA.x * vectorB.x,
                               vectorA.y * vectorB.y);
        }

        /// <summary>
        /// Vector2Int×Vector2Intを行います。 
        /// 単純にx,yそれぞれの値を引数のそれぞれ値倍にします。
        /// ret Vector2(x1 * x2,y1 * y2);
        /// </summary>
        public static Vector2Int MultiVecs(this Vector2Int vectorA, Vector2Int vectorB)
        {
            return new Vector2Int(vectorA.x * vectorB.x,
                                  vectorA.y * vectorB.y);
        }
    }
}