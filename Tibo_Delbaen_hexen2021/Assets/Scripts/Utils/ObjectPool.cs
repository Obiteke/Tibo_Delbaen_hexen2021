using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Utils
{
    public class ObjectPool : MonoBehaviour
    {

        List<GameObject> _pooledObjects = new List<GameObject>();

        [SerializeField]
        int _amount = 10;

        [SerializeField]
        GameObject _pooledObject = null;

        [SerializeField]
        Transform _parent = null;

        private void Start()
        {
            for (int count = 0; count < _amount; count++)
            {
                var go = GameObject.Instantiate(_pooledObject, _parent);
                go.SetActive(false);
                _pooledObjects.Add(go);
            }
        }

        public GameObject GetPooledObject()
        {
            foreach (var pooledObject in _pooledObjects)
            {
                if (!pooledObject.activeInHierarchy)
                {
                    pooledObject.SetActive(true);
                    return pooledObject;
                }
            }

            return null;
        }
    }
}
