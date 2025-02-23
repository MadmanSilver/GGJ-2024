using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    public static T Instance {get; private set;}

    public Singleton()
    {
        if (Instance == null)
            Instance = (T)this;
    }
}
