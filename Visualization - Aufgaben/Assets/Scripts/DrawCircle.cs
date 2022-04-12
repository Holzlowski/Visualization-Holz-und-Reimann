using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCircle : MonoBehaviour
{
    public LineRenderer circleRenderer;
    public float radius;

    // Start is called before the first frame update
    void Start()
    {
        //DrawCircleWithLineRenderer(100,radius);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DrawCircleWithLineRenderer(int steps, float radius){
        circleRenderer.positionCount = steps;

        for(int currentStep = 0; currentStep < steps; currentStep++){
            float circumferenceProgress = (float)currentStep/(steps-1);
 
            float currentRadian = circumferenceProgress * 2 * Mathf.PI;

            float xScaled = Mathf.Cos(currentRadian);
            float yScaled = Mathf.Sin(currentRadian);
 
            float x = radius * xScaled;
            float y = radius * yScaled;
            float z = 0;
 
            Vector3 currentPosition = new Vector3(x,y,z);
 
            circleRenderer.SetPosition(currentStep,currentPosition);
        }
    }
    
    private void OnDrawGizmos() {
        Gizmos.color = Color.white;

        float theta = 0;
        float x = radius * Mathf.Cos(theta);
        float y = radius * Mathf.Sin(theta);

        Vector3 pos = transform.position + new Vector3(x, 0, y);
        Vector3 newPos = pos;
        Vector3 lastPos = pos;

        for (theta = 0.1f; theta < Mathf.PI * 2; theta += 0.1f)
        {
            x = radius * Mathf.Cos(theta);
            y = radius * Mathf.Sin(theta);
            newPos = transform.position + new Vector3(x, y, 0);
            Gizmos.DrawLine(pos, newPos);
            pos = newPos;
        }
        Gizmos.DrawLine(pos, lastPos);
    }  
        
}


