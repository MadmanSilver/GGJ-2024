using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera cam;

    private BoxCollider col;
    private Transform trans;
    private Vector3 offset;
    private bool x = true, y = true;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<BoxCollider>();
        trans = transform;
        offset = cam.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newScale = new Vector3(cam.orthographicSize * 2 * cam.aspect,
                                        cam.orthographicSize * 2, 100f);
        col.size = newScale;

        cam.transform.position += Vector3.Scale(trans.position - (cam.transform.position - offset),
                                        new Vector3(x ? 1f : 0f, y ? 1f : 0f, 1f));
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player")
            return;

        string otherName = other.gameObject.name.ToLower();
        Debug.Log($"Trigger Enter: {otherName}");

        if (otherName.Contains("upper") || otherName.Contains("lower"))
        {
            y = false;
        }
        else if (otherName.Contains("left") || otherName.Contains("right"))
        {
            x = false;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player")
            return;

        string otherName = other.gameObject.name.ToLower();
        Debug.Log($"Trigger Exit: {otherName}");

        if (otherName.Contains("upper") || otherName.Contains("lower"))
        {
            y = true;
        }
        else if (otherName.Contains("left") || otherName.Contains("right"))
        {
            x = true;
        }
    }
}
