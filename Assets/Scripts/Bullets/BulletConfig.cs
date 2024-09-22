using UnityEngine;
using Common;

namespace ShootEmUp
{
    [CreateAssetMenu(
        fileName = "BulletConfig",
        menuName = "Bullets/New BulletConfig"
    )]
    public class BulletConfig : ScriptableObject
    {
        [SerializeField]
        private PhysicsLayer _physicsLayer;
        [SerializeField]
        private Color _color;
        [SerializeField]
        private int _damage;
        [SerializeField]
        private float _speed;


        public PhysicsLayer PhysicsLayer
        {
            get => _physicsLayer;
        }

        public Color Color
        {
            get => _color;
        }

        public int Damage
        {
            get => _damage;
        }

        public float Speed
        {
            get => _speed;
        }

    }
}