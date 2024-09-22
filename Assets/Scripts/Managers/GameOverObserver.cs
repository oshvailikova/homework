using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class GameOverObserver : MonoBehaviour
    {
        [SerializeField]
        private Player _player;

        private GameManager _gameManager;

        private void Awake()
        {
            _gameManager = GetComponent<GameManager>();
        }

        private void OnEnable()
        {
            _player.OnDestroy += FinishGame;
        }

        private void OnDisable()
        {
            _player.OnDestroy -= FinishGame;
        }

        private void FinishGame(Player player)
        {
            _gameManager.FinishGame();
        }
    }
}