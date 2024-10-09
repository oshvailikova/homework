using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class BulletManager : IDisposable,
            IGamePauseListener, IGameResumeListener, IGameFinishListener,
            IGameFixedUpdateListener
    {
        private IBulletSpawner _bulletSpawner;

        private readonly List<Bullet> _activeBullets = new();

        [Inject]
        public void Construct(IBulletSpawner bulletSpawner)
        {
            _bulletSpawner = bulletSpawner;

            IBulletSpawner.OnRegister += Register;
            IBulletSpawner.OnRemove += Remove;
        }

        public void Dispose()
        {
            IBulletSpawner.OnRegister -= Register;
            IBulletSpawner.OnRemove -= Remove;
        }

        public void Register(Bullet bullet)
        {           
            _activeBullets.Add(bullet);
        }

        private void Remove(Bullet bullet)
        {            
            _activeBullets.Remove(bullet);
        }

        public void OnGamePause()
        {
            for (int i = 0; i < _activeBullets.Count; i++)
            {
                _activeBullets[i].OnGamePause();
            }
        }

        public void OnGameResume()
        {
            for (int i = 0; i < _activeBullets.Count; i++)
            {
                _activeBullets[i].OnGameResume();
            }
        }

        public void OnGameFinish()
        {
            for (int i = _activeBullets.Count - 1; i >= 0; i--)
            {
                _bulletSpawner.Despawn(_activeBullets[i]);
            }

            _activeBullets.Clear();
        }

        public void OnFixedUpdate(float deltaTime)
        {
            for (int i = 0; i < _activeBullets.Count; i++)
            {
                _activeBullets[i].OnFixedUpdate(deltaTime);
            }
        }
    }
}