using ShootEmUp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputObserver : MonoBehaviour
{
    [SerializeField]
    private PlayerController _playerController;
    [SerializeField]
    private InputManager _inputManager;


    private void OnEnable()
    {
        _inputManager.OnShootKeyDownEvent += _playerController.RequireFire;
        _inputManager.OnMoveEvent += _playerController.SetMoveDirection;
    }

    private void OnDisable()
    {
        _inputManager.OnShootKeyDownEvent -= _playerController.RequireFire;
        _inputManager.OnMoveEvent -= _playerController.SetMoveDirection;
    }

}
