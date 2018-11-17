using UnityEngine;

namespace UnityStandardAssets.Utility
{
    public class SmoothFollow : MonoBehaviour
    {

        // The target we are following
        [SerializeField] private Transform target;

        void LateUpdate()
        {
            Vector3 newPosition = target.position;
            newPosition.y += 1.5f;
            newPosition.z -= 2f;
            transform.position = newPosition;
        }


    }
}