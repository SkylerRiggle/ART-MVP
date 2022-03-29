using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T _instance;
    public static T instance { get { return _instance; } }

    protected void Initialize()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
            return;
        }

        _instance = this as T;
        DontDestroyOnLoad(this);
    }
}
