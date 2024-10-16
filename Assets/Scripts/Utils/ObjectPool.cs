using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public Stack<PooledObject> pool = new Stack<PooledObject>();
    public PooledObject objPrefab;
    
    private const int POOL_SIZE = 30;

    void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        // Pooled Object 생성 후 비활성화
        for (int i = 0; i < POOL_SIZE; i++)
        {
            GameObject pooledObject = Instantiate(objPrefab).gameObject;
            pooledObject.transform.parent = this.transform;
            pooledObject.SetActive(false);
            pool.Push(pooledObject.GetComponent<PooledObject>());
        }
    }

    public PooledObject TakeFromPool()
    {
        PooledObject takenObject = pool.Pop();
        takenObject.transform.SetParent(null);
        takenObject.gameObject.SetActive(true);

        return takenObject;
    }

    public void ReturnToPool(PooledObject pooledObject)
    {
        pool.Push(pooledObject);
        pooledObject.transform.SetParent(this.transform);
        pooledObject.gameObject.SetActive(false);
    }
    
}