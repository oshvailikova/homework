using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    [RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
    public class Bullet : MonoBehaviour
    {
        public event Action<Bullet> OnDestroy;

        [NonSerialized] public int _damage;

        private Rigidbody2D _rigidbody2D;
        private SpriteRenderer _spriteRenderer;
        private LevelBounds _levelBounds;

        private Vector2 _velocity;

        [Inject]
        public void Construct(LevelBounds levelBounds)
        {
            _levelBounds = levelBounds;

            _rigidbody2D = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Init(BulletInfo bulletInfo)
        {
            transform.position = bulletInfo.Position;
            
            gameObject.layer = (int)bulletInfo.BulletConfig.PhysicsLayer;

            _damage = bulletInfo.BulletConfig.Damage;
            _velocity = bulletInfo.Direction * bulletInfo.BulletConfig.Speed;   

            _spriteRenderer.color = bulletInfo.BulletConfig.Color;
            _rigidbody2D.velocity = _velocity;
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
            if (!collision.gameObject.CompareTag("DestructibleObject"))
            {
                return false;
            }

            var destrictible = collision.gameObject.GetComponent<IDestructible>();
            destrictible.TakeDamage(_damage);

            Destroy();
            
            return true;          
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (CheckCollisionWithBullets(collision)) return;
            if (CheckCollisionWithDestrictableObject(collision)) return;
        }

        public void OnGamePause()
        {           
            _rigidbody2D.velocity = Vector2.zero;
        }

        public void OnGameResume()
        {
            _rigidbody2D.velocity = _velocity;
        }

        public void OnFixedUpdate(float fixedTime)
        {
            if (!_levelBounds.InBounds(transform.position))
            {
                Destroy();
            }
        }
    }
}