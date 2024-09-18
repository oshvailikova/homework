using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class GameOverObserver : MonoBehaviour
    {
        [SerializeField]
        private PlayerController _playerController;

        private GameManager _gameManager;

        private void Awake()
        {
            _gameManager = GetComponent<GameManager>();
        }

        private void OnEnable()
        {
            _playerController.OnPlayerDeath += FinishGame;
        }

        private void OnDisable()
        {
            _playerController.OnPlayerDeath -= FinishGame;
        }

        private void FinishGame()
        {
            _gameManager.FinishGame();
        }
    }
}