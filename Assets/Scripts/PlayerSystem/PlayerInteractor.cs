using System;
using UnityEngine;
using Utils;
using Object = UnityEngine.Object;

namespace PlayerSystem
{
    public class PlayerInteractor : MonoBehaviour
    {
        private const int BufferSize = 10;
        
        
        [SerializeField] private LayerMask layerMask;
        
        
        private Vector3 Center => GetCenter();
        private Quaternion Rotation => GetRotation();
        private Vector3 HalfExtends => GetHalfExtends();


        private readonly Collider[] m_ColliderBuffer = new Collider[BufferSize];
        
        
        public bool TryGetInteraction<T>(out T interaction)
            where T : Object
        {
            var size = Physics.OverlapBoxNonAlloc(Center, HalfExtends, m_ColliderBuffer, Rotation, layerMask);

            if (size <= 0)
            {
                interaction = null;
                return false;
            }

            for (var i = 0; i < size; i++)
            {
                if (!m_ColliderBuffer[i].TryGetComponent(out interaction)) continue;

                return true;
            }

            interaction = null;
            return false;
        }


        private Vector3 GetCenter()
        {
            return transform.position;
        }

        private Quaternion GetRotation()
        {
            return transform.rotation;
        }

        private Vector3 GetHalfExtends()
        {
            return transform.lossyScale * 0.5f;
        }


#if UNITY_EDITOR

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            GizmosExtra.DrawWireCube(Center, Rotation, HalfExtends * 2f);
        }

#endif
    }
}