using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactible : MonoBehaviour
{
    public bool autoInteract = false;
    public UnityEvent onInteract;

    [SerializeField] private GameObject prompt;
    [SerializeField] private AudioClip openSound;
    [SerializeField] private AudioClip closeSound;

    private AudioSource source;

    private void Start() {
        source = GetComponent<AudioSource>();
    }

    private void Update() {
        if (Input.GetButtonDown("Interact") && !autoInteract && prompt.activeSelf)
        {
            onInteract?.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag != "Player")
            return;

        if (autoInteract)
            onInteract?.Invoke();
        else
        {
            prompt.SetActive(true);
            source.PlayOneShot(openSound);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag != "Player")
            return;

        if (!autoInteract)
        {
            prompt.SetActive(false);
            source.PlayOneShot(closeSound);
        }
    }
}
