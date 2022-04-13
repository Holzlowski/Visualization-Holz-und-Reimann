using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class XRechnung : MonoBehaviour
{   
    public GameObject roterKreis, blauerKreis;
    private string guessedRatio;
    private float ratio;

    
    // Start is called before the first frame update
    void Start()
    {
        GetRadius(roterKreis);
        GetRadius(blauerKreis);
        GetSurfaceArea(roterKreis);
        GetSurfaceArea(blauerKreis);
        GetRatio(roterKreis, blauerKreis);
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

    void GetRatio(GameObject rot, GameObject blau){
        ratio = GetSurfaceArea(blau) / GetSurfaceArea(rot);
        Debug.Log("Das Verhältnis zwischen dem roten und blauen Kreis ist: 1 zu " + ratio);
    }

    public void InputRation(string input){
        guessedRatio = input;
        Debug.Log(guessedRatio);
    }

    public void GetX(){
        if(guessedRatio != null){
            float x = Mathf.Log10(1/float.Parse(guessedRatio))/Mathf.Log10(1/ratio);
            Debug.Log("X ist: " + x);
        }
        else{
            Debug.Log("Es wurde noch nichts eingegeben.");
        }
    
    }
}
