using ShootEmUp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputObserver : MonoBehaviour
{
    [SerializeField]
    private Player _player;
    [SerializeField]
    private InputManager _inputManager;

    private void OnEnable()
    {
        _inputManager.OnShootEvent += _player.RequireFire;
        _inputManager.OnMoveEvent += _player.SetMoveDirection;
    }

    private void OnDisable()
    {
        _inputManager.OnShootEvent -= _player.RequireFire;
        _inputManager.OnMoveEvent -= _player.SetMoveDirection;
    }

}
