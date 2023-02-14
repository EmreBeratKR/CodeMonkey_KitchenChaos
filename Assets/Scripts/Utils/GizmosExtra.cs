using UnityEngine;

namespace Utils
{
    public static class GizmosExtra
    {
        public static void DrawWireCube(Vector3 center, Quaternion rotation, Vector3 size)
        {
            var oldMatrix = Gizmos.matrix;
            
            Gizmos.matrix = Matrix4x4.TRS(center, rotation, size);
            Gizmos.DrawWireCube(Vector3.zero, Vector3.one);

            Gizmos.matrix = oldMatrix;
        }
    }
}