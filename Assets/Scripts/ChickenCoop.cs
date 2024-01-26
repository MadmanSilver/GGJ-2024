using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ChickenCoop : MonoBehaviour
{
    public int chickCount = 0;

    [SerializeField] private GameObject egg;
    [SerializeField] private GameObject penWalls;

    private int totalChicks = 0;

    // Start is called before the first frame update
    void Start()
    {
        totalChicks = FindObjectsOfType<ChickenAI>().Length;
    }
    
    private void OnTriggerEnter(Collider other) {
        if (other.tag != "Chicken")
            return;

        // other.gameObject.SetActive(false);
        chickCount++;

        if (chickCount >= totalChicks)
        {
            egg.SetActive(true);
            penWalls.SetActive(true);
        }
    }
    
    private void OnTriggerExit(Collider other) {
        if (other.tag != "Chicken")
            return;

        chickCount--;
    }
}
