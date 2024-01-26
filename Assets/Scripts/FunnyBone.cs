using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunnyBone : MonoBehaviour
{
    public void Collect()
    {
        GameManager.Instance.funnyBone = true;
    }
}
