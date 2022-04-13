using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class XRechnung : MonoBehaviour
{   
    public GameObject roterKreis, blauerKreis;
    public TextMeshProUGUI buttonText; 
    public GameObject inputField;
    Scaler blueScaler;
    private float guessedRatio;
    private float ratio;
    private float x;

    private void Awake() {
        blueScaler = blauerKreis.GetComponent<Scaler>();
        //blueScaler.RandomizeScale();

    }
    // Start is called before the first frame update
    void Start()
    {   
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reset() {
        blueScaler.RandomizeScale();
        inputField.GetComponent<TMP_InputField>().text = "";
        buttonText.text = "X berechnen";

        guessedRatio = 0;
        x = 0;
        
        GetRatio(blauerKreis, roterKreis);
    }

    float GetRadius(GameObject kreis){
        float radius = kreis.transform.localScale.z/2;
        return radius;
    }

    float GetSurfaceArea(GameObject kreis){
        float surfaceArea = Mathf.PI*Mathf.Pow(GetRadius(kreis), 2);
        return surfaceArea;
    }

    void GetRatio(GameObject blau, GameObject rot){
        ratio = GetSurfaceArea(blau) / GetSurfaceArea(rot);
        GetLog(blau);
        GetLog(rot);
        Debug.Log("Das Verhältnis zwischen dem roten und blauen Kreis ist: 1 zu " + ratio);
    }

    public void InputRation(string input){
        guessedRatio = float.Parse(input);
    }

    public void GetX(){
        if(x != 0){
            Reset();
        }
        else if(guessedRatio != 0 && x == 0){
            x = Mathf.Log10(1/guessedRatio)/Mathf.Log10(1/ratio);
            inputField.GetComponent<TMP_InputField>().text = "X ist: " + x;
            //Debug.Log("X ist: " + x);
            buttonText.text = "Nächster Test";
        }
        else{
            Debug.Log("Es wurde noch nichts eingegeben.");
        }
    }

    void GetLog(GameObject kreis){
        Debug.Log("Der " + kreis.name + " hat einen Radius von: " + GetRadius(kreis));
        Debug.Log("Der Flächeninhalt von " + kreis.name + " beträgt: " + GetSurfaceArea(kreis));
    }
}
