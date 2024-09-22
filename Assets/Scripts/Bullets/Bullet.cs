using System;
using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;

namespace ShootEmUp
{
    [RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
    public class Bullet : MonoBehaviour, IDestroyable<Bullet>
    {
        public event Action<Bullet> OnDestroy;

        [NonSerialized] public int _damage;

        private Rigidbody2D _rigidbody2D;
        private SpriteRenderer _spriteRenderer;
        private LevelBounds _levelBounds;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void FixedUpdate()
        {
            if (!_levelBounds.InBounds(transform.position))
            {
                Destroy();
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (CheckCollisionWithBullets(collision)) return;
            if (CheckCollisionWithDestrictableObject(collision)) return;
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

        public void Destroy()
        {
            OnDestroy?.Invoke(this);
        }

        private bool CheckCollisionWithBullets(Collision2D collision)
        {
            if (!collision.gameObject.CompareTag("Bullet"))
            {
                return false;
            }

            Destroy();
            
            return true;
        }

        private bool CheckCollisionWithDestrictableObject(Collision2D collision)
        {
            if (!collision.gameObject.CompareTag("DestrictableObject"))
            {
                return false;
            }

            var health = collision.gameObject.GetComponent<HealthComponent>();
            health.TakeDamage(_damage);

            Destroy();
            
            return true;          
        }
    }
}