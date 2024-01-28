using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RejectCube : MonoBehaviour
{
    [SerializeField] private ChickenCoop coop;

    private void OnTriggerEnter(Collider other) {
        coop.ReevaluateChicken(other.GetComponent<ChickenAI>(), true);
    }

    private void OnTriggerExit(Collider other) {
        coop.ReevaluateChicken(other.GetComponent<ChickenAI>(), false);
    }
}
