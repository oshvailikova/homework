using System;
using UnityEngine;

public interface IDestroyable<T> where T:Component 
{
    event Action<T> OnDestroy;
    void Destroy();
}