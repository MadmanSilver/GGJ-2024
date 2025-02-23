using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalPositionConstraint : MonoBehaviour
{
    [SerializeField] private Transform target;

    private Transform trans;

    // Start is called before the first frame update
    void Start()
    {
        trans = transform;
    }

    // Update is called once per frame
    void Update()
    {
        trans.localPosition = target.localPosition;
    }
}
