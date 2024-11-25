
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
       private static T instance;

       public static T Instance
       {
              get
              {
                     return instance;
              }
       }

       // virtual : 파생 클래스에서 재정의 가능
       public virtual void Awake()
       {
              if (instance == null)
              {
                     instance = this as T;
                     DontDestroyOnLoad(this);
              }
              else
              {
                     Debug.Log("SingleTon Destroyed");
                     Destroy(gameObject);
              }
       }
}