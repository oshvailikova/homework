

using ShootEmUp;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Common
{
    public class ObjectPool : MonoBehaviour,IGameStartListener, IGameFinishListener
    {
        [SerializeField]
        private GameObject _prefab;
        [SerializeField]
        private int _initialSize;
        [SerializeField]
        private Transform _activeContainer;

        private readonly Queue<GameObject> _poolObjects = new();
        private readonly HashSet<GameObject> _activeObjects = new();

        public void Start()
        {
            this.As<IGameListener>().Register();
        }

        public GameObject GetFromPool()
        {
            if (!_poolObjects.TryDequeue(out var poolObject))
            {
                poolObject = CreateNewPoolObject();
            }

            poolObject.transform.SetParent(_activeContainer);
            poolObject.SetActive(true);
            _activeObjects.Add(poolObject);
            return poolObject;
        }

        public T GetFromPool<T>() where T : Component
        {
            GameObject obj = GetFromPool(); 
            T component = obj.GetComponent<T>(); 

            if (component == null)
            {
                Debug.LogWarning("Component " + typeof(T).Name + " not found.");
            }

            return component;
        }

        public void ReturnToPool(GameObject poolObject)
        {
            if (_activeObjects.Remove(poolObject))
            {
                poolObject.SetActive(false);
                poolObject.transform.SetParent(transform);
                _poolObjects.Enqueue(poolObject);
            }           
        }

        private void InitializePool()
        {
            for (int i = 0; i < _initialSize; i++)
            {
                _poolObjects.Enqueue(CreateNewPoolObject());
            }
        }

        private GameObject CreateNewPoolObject()
        {
            var newPoolObject = Instantiate(_prefab, transform);
            newPoolObject.SetActive(false);
            return newPoolObject;
        }
        public void OnGameStart()
        {
            InitializePool();
        }

        public void OnGameFinish()
        {
            while (_activeObjects.Count > 0)
            {
                ReturnToPool(_activeObjects.ElementAt(0));
            }

            while (_poolObjects.TryDequeue(out var poolObject))
            {
                var c = poolObject.GetComponent<IGameListener>();
                Debug.Log(gameObject + "  " + c);
                if (poolObject.TryGetComponent<IGameListener>(out IGameListener listener))
                {
                    listener.Remove();
                }
                Destroy(poolObject);
            }
        }

    }
}
