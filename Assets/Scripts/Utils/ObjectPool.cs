using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ObjectPool : MonoBehaviour
{
    public Queue<PooledObject> pool = new Queue<PooledObject>();
    public PooledObject objPrefab;
    
    [SerializeField]
    private int poolSize = 30;

    protected virtual void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        // Pooled Object 생성 후 비활성화
        for (int i = 0; i < poolSize; i++)
        {
            GameObject pooledObject = Instantiate(objPrefab).gameObject;
            pooledObject.transform.parent = this.transform;
            pooledObject.transform.localScale = Vector3.one;
            pooledObject.GetComponent<PooledObject>().pool = this;    
            pooledObject.SetActive(false);
            pool.Enqueue(pooledObject.GetComponent<PooledObject>());
        }
    }

    public PooledObject TakeFromPool()
    {
        PooledObject takenObject = pool.Dequeue();
        takenObject.transform.SetParent(null);
        takenObject.gameObject.SetActive(true);

        return takenObject;
    }

    public void ReturnToPool(PooledObject pooledObject)
    {
        pool.Enqueue(pooledObject);
        pooledObject.transform.SetParent(this.transform);
        pooledObject.gameObject.SetActive(false);
    }
    
}