using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;

    public static T Instance => instance;

    protected virtual void Awake()
    {
        if ((Object)instance != (Object)null)
        {
            Object.Destroy(base.gameObject);
        }
        else
        {
            instance = (T)this;
            DontDestroyOnLoad(this);
        }

    }
}
