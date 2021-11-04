using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler instance;
    public GameObject pooledObjectEnemy;
    public GameObject pooledObjectPlayer;

    public int pooledAmount = 15;
    public bool willGrow = true;

    public List<GameObject> pooledObjectsList;
    public List<GameObject> pooledObjectsListPlayer;

    [SerializeField]
    private GameObject _rocksContainerEnemy = null;
    [SerializeField]
    private GameObject _rocksContainerPlayer = null;

    private void Awake()
    {
        instance = this;
  //  }
  //  void Start()
  //  {
        pooledObjectsList = new List<GameObject>();

        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = Instantiate(pooledObjectEnemy);
            obj.transform.parent = _rocksContainerEnemy.transform;
            obj.SetActive(false);

            pooledObjectsList.Add(obj);
        }
        pooledObjectsListPlayer = new List<GameObject>();

        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = Instantiate(pooledObjectPlayer);
            obj.transform.parent = _rocksContainerPlayer.transform;
            obj.SetActive(false);

            pooledObjectsListPlayer.Add(obj);

            //   }
        }
    }
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjectsList.Count; i++)
        {
            if (!pooledObjectsList[i].activeInHierarchy)
            {
                return pooledObjectsList[i];         
            }
        }
        if (willGrow)
        {
            GameObject obj = Instantiate(pooledObjectEnemy);
            pooledObjectsList.Add(obj);
            return obj;
        }
        return null;
    }

    public GameObject GetPooledObjectPlayer()
    {
        for (int i = 0; i < pooledObjectsListPlayer.Count; i++)
        {
            if (!pooledObjectsListPlayer[i].activeInHierarchy)
            {
                return pooledObjectsListPlayer[i];
            }
        }
        if (willGrow)
        {
            GameObject obj = Instantiate(pooledObjectPlayer);
            pooledObjectsListPlayer.Add(obj);
            return obj;
        }
        return null;
    }
}
