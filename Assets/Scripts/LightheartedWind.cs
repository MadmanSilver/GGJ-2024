using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightheartedWind : MonoBehaviour
{
    public void Collect()
    {
        GameManager.Instance.lightheartedWind = true;
    }
}
