using System;
using UnityEngine;

namespace ShootEmUp
{
    public interface IGameListener
    {
        public static event Action<IGameListener> OnRegister;
        public static event Action<IGameListener> OnRemove;

        public void Register()
        {
            OnRegister?.Invoke(this);
        }

        public void Remove()
        {
            OnRemove?.Invoke(this);
        }
    }

    public interface IGameStartListener : IGameListener
    {
        void OnGameStart();
    }

    public interface IGameFinishListener : IGameListener
    {
        void OnGameFinish();
    }

    public interface IGamePauseListener : IGameListener
    {
        void OnGamePause();
    }

    public interface IGameResumeListener : IGameListener
    {
        void OnGameResume();
    }

    public interface IGameUpdateListener : IGameListener
    {
        void OnUpdate(float time);
    }

    public interface IGameFixedUpdateListener : IGameListener
    {
        void OnFixedUpdate(float deltaTime);
    }

    public interface IGameLateUpdateListener : IGameListener
    {
        void OnLateUpdate(float time);
    }

}
