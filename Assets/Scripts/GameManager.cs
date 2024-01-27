using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public bool funnyBone = false;
    public bool sunnySideUpEgg = false;
    public bool lightheartedWind = false;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    public bool HasAll()
    {
        return funnyBone && sunnySideUpEgg && lightheartedWind;
    }

    public bool HasNone()
    {
        return !funnyBone && !sunnySideUpEgg && !lightheartedWind;
    }
}
