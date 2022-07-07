using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRd : MonoBehaviour
{
    private LineRenderer lineRenderer;
    [SerializeField]
    private Transform start;
    Vector3 vStart;
    void Start()
    {
        vStart = start.position;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, vStart);
        lineRenderer.SetPosition(1, transform.position);
    }
    void Update()
    {
        lineRenderer.SetPosition(1, transform.position);
    }
}
