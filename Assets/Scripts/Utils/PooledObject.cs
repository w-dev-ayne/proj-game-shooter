using UnityEngine;

public class PooledObject : MonoBehaviour
{
        protected ObjectPool pool;

        public void Release()
        {
                pool.ReturnToPool(this);
        }
}