using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRechnung : MonoBehaviour
{   
    public GameObject roterKreis, blauerKreis;
    
    // Start is called before the first frame update
    void Start()
    {
        GetRadius(roterKreis);
        GetRadius(blauerKreis);
        GetSurfaceArea(roterKreis);
        GetSurfaceArea(blauerKreis);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    float GetRadius(GameObject kreis){
        float radius = kreis.transform.localScale.z/2;
        return radius;
    }

    float GetSurfaceArea(GameObject kreis){
        float surfaceArea = Mathf.PI*Mathf.Pow(GetRadius(kreis), 2);
        Debug.Log("Der " + kreis.name + " hat einen Radius von: " + GetRadius(kreis));
        Debug.Log("Der Flächeninhalt von " + kreis.name + " beträgt: " + surfaceArea);
        return surfaceArea;
    }
}
