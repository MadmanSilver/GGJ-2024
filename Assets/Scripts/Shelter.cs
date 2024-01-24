using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Shelter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.tag != "Player")
            return;

        other.GetComponent<WindSimulator>().sheltered = true;
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag != "Player")
            return;

        other.GetComponent<WindSimulator>().sheltered = false;
    }
}
