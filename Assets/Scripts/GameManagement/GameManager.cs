using Extensions;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class GameManager : ITickable, IFixedTickable, ILateTickable
    {
        private GameState _gameState;

        private readonly List<IGameListener> _gameListeners = new();

        private readonly List<IGameUpdateListener> _gameUpdateListeners = new();
        private readonly List<IGameLateUpdateListener> _gameLateUpdateListeners = new();
        private readonly List<IGameFixedUpdateListener> _gameFixedUpdateListeners = new();

     
        [Inject]
        private void Construct(IEnumerable<IGameListener> listeners)
        {
            foreach (IGameListener listener in listeners)
            {
                AddListener(listener);
            }
        }

        public void Tick()
        {
            if (_gameState != GameState.Play) return;

            for (var i = 0; i < _gameUpdateListeners.Count; i++)
            {
                _gameUpdateListeners[i].OnUpdate(Time.deltaTime);
            }
        }

        public void LateTick()
        {
            if (_gameState != GameState.Play) return;

            for (var i = 0; i < _gameLateUpdateListeners.Count; i++)
            {
                _gameLateUpdateListeners[i].OnLateUpdate(Time.deltaTime);
            }
        }

        public void FixedTick()
        {
            if (_gameState != GameState.Play) return;

            for (var i = 0; i < _gameFixedUpdateListeners.Count; i++)
            {
                _gameFixedUpdateListeners[i].OnFixedUpdate(Time.fixedDeltaTime);
            }
        }

        private void AddListener(IGameListener listener)
        {
            _gameListeners.Add(listener);
         
            if (listener is IGameUpdateListener updateListener)
            {
                _gameUpdateListeners.Add(updateListener);
            }

            if (listener is IGameFixedUpdateListener fixedUpdateListener)
            {
                _gameFixedUpdateListeners.Add(fixedUpdateListener);
            }

            if (listener is IGameLateUpdateListener lateUpdateListener)
            {
                _gameLateUpdateListeners.Add(lateUpdateListener);
            }
        }

        public void OnStart()
        {
            if (_gameState != GameState.None && _gameState != GameState.Finish) return;
            
            for (var i = 0; i < _gameListeners.Count; i++)
            {
                _gameListeners[i].As<IGameStartListener>()?.OnGameStart();
            }
            
            _gameState = GameState.Play;
        }

        public void OnPause()
        {
            if (_gameState != GameState.Play) return;
            
            for (var i = 0; i < _gameListeners.Count; i++)
            {
                _gameListeners[i].As<IGamePauseListener>()?.OnGamePause();
            }
            
            _gameState = GameState.Pause;
        }

        public void OnResume()
        {
            if (_gameState != GameState.Pause) return;
            
            for (var i = 0; i < _gameListeners.Count; i++)
            {
                _gameListeners[i].As<IGameResumeListener>()?.OnGameResume();
            }
            
            _gameState = GameState.Play;
        }

        public void OnFinish()
        {
            if (_gameState != GameState.Play) return;

            for (var i = 0; i < _gameListeners.Count; i++)
            {
                _gameListeners[i].As<IGameFinishListener>()?.OnGameFinish();
            }
            
            _gameState = GameState.Finish;
            Debug.Log("Game over!");
        }

    }

}