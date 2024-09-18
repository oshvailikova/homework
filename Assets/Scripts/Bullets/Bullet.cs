using System;
using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;

namespace ShootEmUp
{
    [RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
    public class Bullet : MonoBehaviour
    {
        public event Action<Bullet> OnDestroyBullet;

        [NonSerialized] public bool _isPlayer;
        [NonSerialized] public int _damage;

        private Rigidbody2D _rigidbody2D;
        private SpriteRenderer _spriteRenderer;
        private LevelBounds _levelBounds;

        public BulletPoolObject PoolObject
        {
            get;private set;
        }

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Init(BulletInfo bulletInfo)
        {
            transform.position = bulletInfo.Position;
            gameObject.layer = (int)bulletInfo.BulletConfig.PhysicsLayer;

            _spriteRenderer.color = bulletInfo.BulletConfig.Color;
            _rigidbody2D.velocity = bulletInfo.Direction * bulletInfo.BulletConfig.Speed;
            _levelBounds = bulletInfo.LevelBounds;
            _damage = bulletInfo.BulletConfig.Damage;
        }

        public void SetPoolObject(BulletPoolObject bulletPoolObject)
        {
            PoolObject = bulletPoolObject;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (CheckCollisionWithBullets(collision)) return;
            if (CheckCollisionWithDestrictableObject(collision)) return;
        }

        private bool CheckCollisionWithBullets(Collision2D collision)
        {
            if (!collision.gameObject.CompareTag("Bullet"))
                return false;

            OnDestroyBullet?.Invoke(this);
            return true;

        }

        private bool CheckCollisionWithDestrictableObject(Collision2D collision)
        {
            if (!collision.gameObject.CompareTag("DestrictableObject"))
                return false;

            var health = collision.gameObject.GetComponent<HealthComponent>();
            health.TakeDamage(_damage);
            OnDestroyBullet?.Invoke(this);
            return true;          
        }

        private void FixedUpdate()
        {
            if (!_levelBounds.InBounds(transform.position))
            {
                OnDestroyBullet?.Invoke(this);
            }
        }
    }
}