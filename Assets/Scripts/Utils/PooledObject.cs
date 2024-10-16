using UnityEngine;

public class PooledObject : MonoBehaviour
{
        private ObjectPool pool;

        public void Release()
        {
                pool.ReturnToPool(this);
        }
}