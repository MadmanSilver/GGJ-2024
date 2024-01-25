using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ChickenCoop : MonoBehaviour
{
    public int chickCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other) {
        if (other.tag != "Chicken")
            return;

        other.gameObject.SetActive(false);
        chickCount++;
    }
}
