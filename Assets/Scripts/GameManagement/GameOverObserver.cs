using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class GameOverObserver : MonoBehaviour,
        IGameStartListener,IGameFinishListener
    {
        [SerializeField]
        private Player _player;

        private GameManager _gameManager;

        private void Awake()
        {          
            _gameManager = GetComponent<GameManager>();
        }

        private void Start()
        {
            this.As<IGameListener>().Register();
        }

        private void FinishGame(Player player)
        {
            _gameManager.OnFinish();
        }

        public void OnGameStart()
        {
            _player.OnDestroy += FinishGame;
        }

        public void OnGameFinish()
        {
            _player.OnDestroy -= FinishGame;
        }
    }
}