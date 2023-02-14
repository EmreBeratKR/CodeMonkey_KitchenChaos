using Utils;
using UnityEngine;

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
            where T : Component
        {
            var count = TryGetInteractions<T>();

            if (count <= 0)
            {
                interaction = null;
                return false;
            }
            
            var center = Center;
            var minSqrDistance = float.MaxValue;
            interaction = null;

            for (var i = 0; i < count; i++)
            {
                var element = m_ColliderBuffer[i];
                var sqrDistance = Vector3.SqrMagnitude(element.transform.position - center);
                
                if (!interaction)
                {
                    interaction = element.GetComponent<T>();
                    minSqrDistance = sqrDistance;
                    continue;
                }

                if (sqrDistance >= minSqrDistance) continue;

                interaction = element.GetComponent<T>();;
                minSqrDistance = sqrDistance;
            }

            return interaction;
        }


        private int TryGetInteractions<T>()
            where T : Component
        {
            var count = Physics.OverlapBoxNonAlloc(Center, HalfExtends, m_ColliderBuffer, Rotation, layerMask);
            var interactionCount = 0;

            for (var i = 0; i < count; i++)
            {
                if (!m_ColliderBuffer[i].TryGetComponent(out T _)) continue;

                m_ColliderBuffer[interactionCount] = m_ColliderBuffer[i];
                interactionCount += 1;
            }

            return interactionCount;
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