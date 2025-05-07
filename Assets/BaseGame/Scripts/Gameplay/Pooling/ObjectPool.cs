using System.Collections.Generic;
using UnityEngine;

namespace BaseGame.Scripts.Gameplay.Pooling
{
    public class ObjectPool<T> where T : MonoBehaviour
    {
        private readonly T _prefab;
        private readonly Transform _parent;
        private readonly Queue<T> _pool = new Queue<T>();

        public Queue<T> Pool => _pool;

        protected ObjectPool(T prefab, int initialSize, Transform parent = null)
        {
            _prefab = prefab;
            _parent = parent;
            
            for (int i = 0; i < initialSize; i++)
            {
                T inst = Object.Instantiate(_prefab, _parent);
                
                inst.gameObject.SetActive(false);
                _pool.Enqueue(inst);
            }
        }
        
        protected T Get()
        {
            if (_pool.Count > 0)
                return _pool.Dequeue();
            
            T inst = Object.Instantiate(_prefab, _parent);
            
            inst.gameObject.SetActive(false);
            
            return inst;
        }

        protected void Return(T item)
        {
            item.gameObject.SetActive(false);
            _pool.Enqueue(item);
        }
    }
}