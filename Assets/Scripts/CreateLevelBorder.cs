using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CreateLevelBorder : MonoBehaviour
{
    private Bounds bounds;
    private Transform upperBound, lowerBound, leftBound, rightBound;
    
    // Start is called before the first frame update
    void Awake()
    {
        bounds = GetComponent<BoxCollider2D>().bounds;

        // Create upper boundary.
        upperBound = new GameObject("Upper Boundary", typeof(BoxCollider)).transform;
        upperBound.gameObject.layer = LayerMask.NameToLayer("Camera");
        upperBound.localScale = new Vector3(bounds.size.x, 1f, 100f);
        upperBound.position = new Vector3(bounds.center.x,
                                            bounds.center.y + bounds.extents.y,
                                            bounds.center.z);
        upperBound.parent = transform;

        // Create lower boundary.
        lowerBound = new GameObject("Lower Boundary", typeof(BoxCollider)).transform;
        lowerBound.gameObject.layer = LayerMask.NameToLayer("Camera");
        lowerBound.localScale = new Vector3(bounds.size.x, 1f, 100f);
        lowerBound.position = new Vector3(bounds.center.x,
                                            bounds.center.y - bounds.extents.y,
                                            bounds.center.z);
        lowerBound.parent = transform;

        // Create left boundary.
        leftBound = new GameObject("Left Boundary", typeof(BoxCollider)).transform;
        leftBound.gameObject.layer = LayerMask.NameToLayer("Camera");
        leftBound.localScale = new Vector3(1f, bounds.size.y, 100f);
        leftBound.position = new Vector3(bounds.center.x + bounds.extents.x,
                                            bounds.center.y,
                                            bounds.center.z);
        leftBound.parent = transform;

        // Create right boundary.
        rightBound = new GameObject("Right Boundary", typeof(BoxCollider)).transform;
        rightBound.gameObject.layer = LayerMask.NameToLayer("Camera");
        rightBound.localScale = new Vector3(1f, bounds.size.y, 100f);
        rightBound.position = new Vector3(bounds.center.x - bounds.extents.x,
                                            bounds.center.y,
                                            bounds.center.z);
        rightBound.parent = transform;
    }
}
