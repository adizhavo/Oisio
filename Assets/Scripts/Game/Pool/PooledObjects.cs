using UnityEngine;
using System.Collections.Generic;

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
            AddClone();
    }

    public bool isInitialized()
    {
        return poolParent != null && pool.Count > 0;
    }

    public GameObject GetPooledObject()
    {
        foreach(GameObject ob in pool)
        {
            if (!ob.activeSelf)
            {
                ob.SetActive(true);
                return ob;
            }
        }

        GameObject clone = AddClone();
        clone.SetActive(true);
        return clone;
    }

    private GameObject AddClone()
    {
        GameObject clone = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
        clone.transform.SetParent(poolParent);
        clone.SetActive(false);
        pool.Add(clone);
        return clone;
    }
}