using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunnySideUpEgg : MonoBehaviour
{
    public void Collect()
    {
        GameManager.Instance.sunnySideUpEgg = true;
    }
}
