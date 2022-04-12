using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawSphere3D : MonoBehaviour
{
    [SerializeField] float radius;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
