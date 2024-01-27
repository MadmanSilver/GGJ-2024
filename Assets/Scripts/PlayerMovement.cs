using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public bool overworld = true;
    public float gravity = -9.8f;
    public float jumpStrength = 1f;
    public float speed = 6.0f;

    [SerializeField] private Vector3 overworldRotation;

    private CharacterController charCon;
    private Vector2 velociraptor;
    private Transform trans;

    // Start is called before the first frame update
    void Start()
    {
        charCon = GetComponent<CharacterController>();
        trans = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (charCon.isGrounded || overworld)
        {
            velociraptor = new Vector2(Input.GetAxis("Horizontal"), overworld ? Input.GetAxis("Vertical") : 0f);
            velociraptor *= speed;

            if (Input.GetButton("Jump") && !overworld)
            {
                velociraptor.y += Mathf.Sqrt(jumpStrength * -3.0f * gravity);
            }
        }
        else if (!charCon.isGrounded && !overworld)
        {
            velociraptor = new Vector2(Input.GetAxis("Horizontal") * speed, velociraptor.y);
        }

        if (!overworld)
        {
            trans.LookAt(Input.GetAxis("Horizontal") * Vector2.right + (Vector2)trans.position);
            velociraptor.y += gravity * Time.deltaTime;
        }
        else
        {
            if (velociraptor.magnitude > 0.001f)
                trans.LookAt(velociraptor + (Vector2)trans.position, Vector3.back);
        }

        charCon.Move(velociraptor * Time.deltaTime);
    }
}
