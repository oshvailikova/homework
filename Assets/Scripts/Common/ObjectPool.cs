

using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField]
        private GameObject _prefab;
        [SerializeField]
        private int _initialSize;
        [SerializeField]
        private Transform _activeContainer;

        private readonly Queue<GameObject> _poolObjects = new();

        public void Awake()
        {
            InitializePool();
        }

        public GameObject GetFromPool()
        {
            if (!_poolObjects.TryDequeue(out var poolObject))
            {
                poolObject = CreateNewPoolObject();
            }

            poolObject.transform.SetParent(_activeContainer);
            poolObject.SetActive(true); 

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
            poolObject.SetActive(false);
            poolObject.transform.SetParent(transform);
            _poolObjects.Enqueue(poolObject);
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
    }
}
