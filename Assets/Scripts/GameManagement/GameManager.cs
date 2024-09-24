using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private GameState _gameState;

        private readonly List<IGameStartListener> gameStartListeners = new();
        private readonly List<IGameFinishListener> gameFinishListeners = new();
        private readonly List<IGamePauseListener> gamePauseListeners = new();
        private readonly List<IGameResumeListener> gameResumeListeners = new();

        private readonly List<IGameUpdateListener> gameUpdateListeners = new();
        private readonly List<IGameLateUpdateListener> gameLateUpdateListeners = new();
        private readonly List<IGameFixedUpdateListener> gameFixedUpdateListeners = new();

        private void Awake()
        {
            IGameListener.OnRegister += AddListener;
            IGameListener.OnRemove += RemoveListener;
        }

        private void OnDestroy()
        {
            IGameListener.OnRegister -= AddListener;
            IGameListener.OnRemove -= RemoveListener;
        }

        private void FixedUpdate()
        {
            if (_gameState != GameState.Play) return;

            for (var i = 0; i < gameFixedUpdateListeners.Count; i++)
            {
                gameFixedUpdateListeners[i].OnFixedUpdate(Time.fixedDeltaTime);
            }
        }

        private void Update()
        {
            if (_gameState != GameState.Play) return;

            for (var i = 0; i < gameUpdateListeners.Count; i++)
            {
                gameUpdateListeners[i].OnUpdate(Time.deltaTime);
            }
        }

        private void LateUpdate()
        {
            if (_gameState != GameState.Play) return;

            for (var i = 0; i < gameLateUpdateListeners.Count; i++)
            {
                gameLateUpdateListeners[i].OnLateUpdate(Time.deltaTime);
            }
        }

        private void AddListener(IGameListener listener)
        {

            if (listener is IGameStartListener startListener)
            {
                gameStartListeners.Add(startListener);
            }

            if (listener is IGamePauseListener pauseListener)
            {
                gamePauseListeners.Add(pauseListener);
            }

            if (listener is IGameResumeListener resumeListener)
            {
                gameResumeListeners.Add(resumeListener);
            }

            if (listener is IGameFinishListener finishListener)
            {
                gameFinishListeners.Add(finishListener);
            }


            if (listener is IGameUpdateListener updateListener)
            {
                gameUpdateListeners.Add(updateListener);
            }

            if (listener is IGameFixedUpdateListener fixedUpdateListener)
            {
                gameFixedUpdateListeners.Add(fixedUpdateListener);
            }

            if (listener is IGameLateUpdateListener lateUpdateListener)
            {
                gameLateUpdateListeners.Add(lateUpdateListener);
            }
        }

        private void RemoveListener(IGameListener listener)
        {
            
            if (listener is IGameStartListener startListener)
            {
                gameStartListeners.Remove(startListener);
            }

            if (listener is IGamePauseListener pauseListener)
            {
                gamePauseListeners.Remove(pauseListener);
            }

            if (listener is IGameResumeListener resumeListener)
            {
                gameResumeListeners.Remove(resumeListener);
            }

            if (listener is IGameFinishListener finishListener)
            {
                gameFinishListeners.Remove(finishListener);
            }


            if (listener is IGameUpdateListener updateListener)
            {
                gameUpdateListeners.Remove(updateListener);
            }

            if (listener is IGameFixedUpdateListener fixedUpdateListener)
            {
                gameFixedUpdateListeners.Remove(fixedUpdateListener);
            }

            if (listener is IGameLateUpdateListener lateUpdateListener)
            {
                gameLateUpdateListeners.Remove(lateUpdateListener);
            }
        }

        [Button]
        public void OnStart()
        {
            if (_gameState != GameState.None && _gameState != GameState.Finish) return;
            
            for (var i = 0; i < gameStartListeners.Count; i++)
            {
                gameStartListeners[i].OnGameStart();
            }
            
            _gameState = GameState.Play;
        }

        [Button]
        public void OnPause()
        {
            if (_gameState != GameState.Play) return;
            
            for (var i = 0; i < gamePauseListeners.Count; i++)
            {
                gamePauseListeners[i].OnGamePause();
            }
            
            _gameState = GameState.Pause;
        }

        [Button]
        public void OnResume()
        {
            if (_gameState != GameState.Pause) return;
            
            for (var i = 0; i < gameResumeListeners.Count; i++)
            {
                gameResumeListeners[i].OnGameResume();
            }
            
            _gameState = GameState.Play;
        }

        [Button]
        public void OnFinish()
        {
            if (_gameState != GameState.Play) return;

            for (var i = 0; i < gameFinishListeners.Count; i++)
            {
                Debug.Log(gameFinishListeners[i]);
                gameFinishListeners[i].OnGameFinish();
            }
            
            _gameState = GameState.Finish;
            Debug.Log("Game over!");
        }
    }

}