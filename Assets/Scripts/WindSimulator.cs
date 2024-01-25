using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class WindSimulator : MonoBehaviour
{
    public bool sheltered = false;
    public float windForce = 2f;
    public float duration = 5f;
    public float cooldown = 2f;

    private CharacterController charCon;
    private float timer = 0f;
    private float randMod = 0f;
    private bool blowing = false;

    // Start is called before the first frame update
    void Start()
    {
        charCon = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (blowing && timer >= duration + randMod)
        {
            timer = 0f;
            randMod = Random.Range(-1f, 1f);
            blowing = false;
        }
        else if (!blowing && timer >= cooldown + randMod)
        {
            timer = 0f;
            randMod = Random.Range(-1f, 1f);
            blowing = true;
        }

        if (sheltered || !blowing)
            return;

        charCon.Move(Vector2.down * windForce * Time.deltaTime);
    }

    public void Sheltered(bool val)
    {
        sheltered = val;
    }
}
