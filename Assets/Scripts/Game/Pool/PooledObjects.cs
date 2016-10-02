using UnityEngine;
using System.Collections.Generic;

namespace Oisio.Game
{
    // will contain oll pooled objects
    [CreateAssetMenu(fileName = GameConfig.GAMEOBJECT_POOL_NAME, menuName = "Oisio / GameObjectPool", order = 0)]
    public class PooledObjects : ScriptableObject
    {
        private static PooledObjects instance;
        public static PooledObjects Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Resources.Load(GameConfig.GAMEOBJECT_POOL_PATH) as PooledObjects;
                    instance.Init();
                }

                return instance;
            }
        }

        [SerializeField] private Pool[] configuredPools;

        private void Init()
        {
            foreach(Pool p in configuredPools)
                p.Init();
        }

        public GameObject RequestGameObject(GameObjectPool requestedObject)
        {
            foreach(Pool p in configuredPools)
                if (p.PooledObject.Equals(requestedObject))
                    return p.GetPooledObject();

            // it will never enter here, the pool witth create new gameObjects if needed
            return null;
        }
    }

    // Pool of gameobjects
    [System.Serializable]
    public class Pool
    {
        // Just to keep the editor organised with names
        public string PoolName;

        [SerializeField] private GameObjectPool Object;
        public GameObjectPool PooledObject
        {
            get { return Object; }
        }

        [SerializeField] private GameObject prefab;
        [SerializeField] private int size;

        private Transform poolParent;
        private List<GameObject> pool;

        public void Init()
        {
            if (isInitialized()) return;

            if (poolParent == null) poolParent = new GameObject(prefab.name + "_pool").transform;

            pool = new List<GameObject>();

            for(int i = 0; i < size; i ++)
                CreateAndAddClone(false);
        }

        public bool isInitialized()
        {
            return poolParent != null && pool.Count > 0;
        }

        public GameObject GetPooledObject()
        {
            for (int i = 0; i < pool.Count; i++)
            {
                if (pool[i] == null) 
                {
                    return CreateAndAddClone(true);   
                }
                else if (!pool[i].activeSelf)
                {
                    pool[i].SetActive(true);
                    return pool[i];
                }
            }

            return CreateAndAddClone(true);
        }

        private GameObject CreateAndAddClone(bool active)
        {
            GameObject clone = CreateClone();
            AddClone(clone);
            clone.SetActive(active);
            return clone;
        }

        private GameObject CreateClone()
        {
            GameObject clone = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
            clone.transform.SetParent(poolParent);
            clone.SetActive(false);
            return clone;
        }

        private GameObject AddClone(GameObject clone)
        {
            pool.Add(clone);
            return clone;
        }
    }
}