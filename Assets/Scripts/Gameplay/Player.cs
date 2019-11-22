using UnityEngine;

namespace ZigZag.Gameplay
{
    [AddComponentMenu("ZigZag/Player")]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(SphereCollider))]
    public sealed class Player : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            
        }
    }
}
