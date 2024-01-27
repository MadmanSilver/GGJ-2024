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
        offset = new Vector3(0f, 0f, cam.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newScale = new Vector3(cam.orthographicSize * 2 * cam.aspect,
                                        cam.orthographicSize * 2, 100f);
        col.size = newScale;

        cam.transform.position += Vector3.Scale(trans.position - (cam.transform.position - offset),
                                        new Vector3(x ? 1f : 0f, y ? 1f : 0f, 1f));

        if (!x || !y)
            FixPosition();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player")
            return;

        string otherName = other.gameObject.name.ToLower();

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

        if (otherName.Contains("upper") || otherName.Contains("lower"))
        {
            y = true;
        }
        else if (otherName.Contains("left") || otherName.Contains("right"))
        {
            x = true;
        }
    }

    private void FixPosition()
    {
        Vector3 adjustedPosition = cam.transform.position;
        Bounds bounds = col.bounds;
        Collider left = null, right = null, up = null, down = null;

        foreach (Collider collider in Physics.OverlapBox(bounds.center, bounds.extents, Quaternion.identity, LayerMask.GetMask("Camera")))
        {
            string name = collider.gameObject.name.ToLower();

            if (name.Contains("upper"))
                up = collider;
            else if (name.Contains("lower"))
                down = collider;
            else if (name.Contains("left"))
                left = collider;
            else if (name.Contains("right"))
                right = collider;
        }

        if (left != null && right != null)
        {
            adjustedPosition.x = (left.transform.position.x + right.transform.position.x) / 2f;
        }
        else if (left != null || right != null)
        {
            Vector3 wallPoint = (left == null ? right : left).ClosestPointOnBounds(trans.position);
            Vector3 ourPoint = col.ClosestPointOnBounds(wallPoint + new Vector3(left == null ? bounds.extents.x : -bounds.extents.x, 0f, 0f));

            Vector3 overlap = wallPoint - ourPoint;
            adjustedPosition.x = (bounds.center + overlap).x;

            foreach (Collider collider in Physics.OverlapBox(adjustedPosition, bounds.extents, Quaternion.identity, LayerMask.GetMask("Camera")))
            {
                string name = collider.gameObject.name.ToLower();

                if (name.Contains("left"))
                    left = collider;
                else if (name.Contains("right"))
                    right = collider;
            }

            if (left != null && right != null)
            {
                adjustedPosition.x = (left.transform.position.x + right.transform.position.x) / 2f;
            }
        }

        if (down != null && up != null)
        {
            adjustedPosition.y = (down.transform.position.y + up.transform.position.y) / 2f;
        }
        else if (down != null || up != null)
        {
            Vector3 wallPoint = (down == null ? up : down).ClosestPointOnBounds(trans.position);
            Vector3 ourPoint = col.ClosestPointOnBounds(wallPoint + new Vector3(0f, down == null ? bounds.extents.y : -bounds.extents.y, 0f));

            Vector3 overlap = wallPoint - ourPoint;
            adjustedPosition.y = (bounds.center + overlap).y;

            foreach (Collider collider in Physics.OverlapBox(adjustedPosition, bounds.extents, Quaternion.identity, LayerMask.GetMask("Camera")))
            {
                string name = collider.gameObject.name.ToLower();

                if (name.Contains("down"))
                    down = collider;
                else if (name.Contains("up"))
                    up = collider;
            }

            if (down != null && up != null)
            {
                adjustedPosition.y = (down.transform.position.y + up.transform.position.y) / 2f;
            }
        }

        cam.transform.position = adjustedPosition;
    }
}
