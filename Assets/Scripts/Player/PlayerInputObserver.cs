using ShootEmUp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputObserver : MonoBehaviour,
    IGameStartListener,IGameFinishListener
{
    [SerializeField]
    private Player _player;
    [SerializeField]
    private InputManager _inputManager;

    private void Start()
    {
        this.As<IGameListener>().Register();
    }

    public void OnGameStart()
    {
        _inputManager.OnShootEvent += _player.RequireFire;
        _inputManager.OnMoveEvent += _player.SetMoveDirection;
    }

    public void OnGameFinish()
    {
        _inputManager.OnShootEvent -= _player.RequireFire;
        _inputManager.OnMoveEvent -= _player.SetMoveDirection;
    }

}
