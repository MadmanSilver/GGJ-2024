using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAudio : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hoverClip;
    [SerializeField] private AudioClip clickClip;

    public void PlayHover()
    {
        audioSource.PlayOneShot(hoverClip, 1f);
    }

    public void PlayClick()
    {
        audioSource.PlayOneShot(clickClip, 1f);
    }
}
