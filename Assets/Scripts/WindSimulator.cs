using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class WindSimulator : MonoBehaviour
{
    public int sheltered = 0;
    public float windForce = 2f;
    public float duration = 5f;
    public float cooldown = 2f;
    public float random = 0.5f;
    public float windupTime = 1f;

    [SerializeField] private AudioClip windWindupSound;
    [SerializeField] private AudioClip windBlowingSound;

    private AudioSource audioSource;
    private CharacterController charCon;
    private float timer = 0f;
    private float randMod = 0f;
    private bool blowing = false;
    private bool winding = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        charCon = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (blowing && timer >= duration + randMod)
        {
            timer = 0f;
            randMod = Random.Range(-random, random);
            blowing = false;
            audioSource.Stop();
        }
        else if (!blowing && !winding && timer >= cooldown + randMod)
        {
            timer = 0f;
            winding = true;
            audioSource.PlayOneShot(windWindupSound);
        }
        else if (winding && timer >= windupTime)
        {
            timer = 0f;
            randMod = Random.Range(-random, random);
            winding = false;
            blowing = true;
            audioSource.PlayOneShot(windBlowingSound);
        }

        if (sheltered > 0 || (!blowing && !winding))
            return;

        if (winding)
            charCon.Move(Vector2.down * Mathf.Lerp(0f, windForce, timer / windupTime) * Time.deltaTime);
        else
            charCon.Move(Vector2.down * windForce * Time.deltaTime);
    }
}
