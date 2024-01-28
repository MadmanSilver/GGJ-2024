using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ChickenCoop : MonoBehaviour
{
    public int chickCount = 0;

    [SerializeField] private GameObject egg;
    [SerializeField] private GameObject penWalls;
    [SerializeField] private Collider rejectCube;

    private int totalChicks = 0;
    private Collider col;

    // Start is called before the first frame update
    void Start()
    {
        totalChicks = FindObjectsOfType<ChickenAI>().Length;
        col = GetComponent<Collider>();
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

    public void ReevaluateChicken(ChickenAI chicken, bool rejected)
    {
        foreach (Collider col in Physics.OverlapBox(col.bounds.center, col.bounds.extents))
        {
            if (col.tag != "Chicken")
                continue;

            if (col.gameObject == chicken.gameObject)
            {
                if (rejected)
                {
                    chickCount--;
                }
                else
                {
                    chickCount++;

                    if (chickCount >= totalChicks)
                    {
                        egg.SetActive(true);
                        penWalls.SetActive(true);
                    }
                }
            }
        }
    }
}
