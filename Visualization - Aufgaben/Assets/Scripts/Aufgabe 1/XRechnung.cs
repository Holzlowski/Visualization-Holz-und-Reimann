using TMPro;
using UnityEngine;

public class XRechnung : MonoBehaviour
{
    public GameObject roterKreis, blauerKreis;
    public GameObject roterQuader, blauerQuader;
    public TextMeshProUGUI buttonText;
    public GameObject inputField;
    Scaler blueScaler;
    private float guessedRatio;
    private float ratio;
    private float x;

    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    private void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    public void Reset()
    {
        if (blauerKreis.activeInHierarchy)
            blueScaler = blauerKreis.GetComponent<Scaler>();
        else if (blauerQuader.activeInHierarchy)
            blueScaler = blauerQuader.GetComponent<Scaler>();

        blueScaler.RandomizeScale();

        inputField.GetComponent<TMP_InputField>().text = "";
        buttonText.text = "X berechnen";

        guessedRatio = 0;
        x = 0;

        blauerKreis.transform.position = new Vector3(0, 0, 0);
        roterKreis.transform.position = new Vector3(-2.5f, 0, 0);

        blauerQuader.transform.position = new Vector3(0, 0, 0);
        roterQuader.transform.position = new Vector3(-2.5f, 0, 0);

        if (blauerKreis.activeInHierarchy)
            GetRatioKreis(blauerKreis, roterKreis);
        else if (blauerQuader.activeInHierarchy)
            GetRatioQuader(blauerQuader, roterQuader);
    }

    float GetRadius(GameObject kreis)
    {
        float radius = kreis.transform.localScale.z / 2;
        return radius;
    }
    float GetQuaderLength(GameObject quader)
    {
        float length = quader.transform.localScale.x;
        return length;
    }

    float GetSurfaceAreaKreis(GameObject kreis)
    {
        float surfaceArea = Mathf.PI * Mathf.Pow(GetRadius(kreis), 2);
        return surfaceArea;
    }

    float GetSurfaceAreaQuader(GameObject kreis)
    {
        float surfaceArea = kreis.transform.localScale.x * kreis.transform.localScale.z;
        return surfaceArea;
    }

    void GetRatioKreis(GameObject blau, GameObject rot)
    {
        ratio = GetSurfaceAreaKreis(blau) / GetSurfaceAreaKreis(rot);
        GetLogKreis(blau);
        GetLogKreis(rot);
        Debug.Log("Das Verhältnis zwischen dem roten und blauen Kreis ist: 1 zu " + ratio);
        blau.transform.position = new Vector3(blau.transform.localPosition.x + GetRadius(blau) / 2, 0, 0);
        rot.transform.position = new Vector3(rot.transform.localPosition.x - GetRadius(rot), 0, 0);
    }

    void GetRatioQuader(GameObject blau, GameObject rot)
    {
        ratio = GetSurfaceAreaQuader(blau) / GetSurfaceAreaQuader(rot);
        GetLogQuader(blau);
        GetLogQuader(rot);
        Debug.Log("Das Verhältnis zwischen dem roten und blauen Quader ist: 1 zu " + ratio);
        blau.transform.position = new Vector3(blau.transform.localPosition.x + GetQuaderLength(blau) / 2, 0, 0);
        rot.transform.position = new Vector3(rot.transform.localPosition.x - GetQuaderLength(rot), 0, 0);
    }

    public void InputRation(string input)
    {
        guessedRatio = float.Parse(input);
    }

    public void GetX()
    {
        if (x != 0)
        {
            Reset();
        }
        else if (guessedRatio != 0 && x == 0)
        {
            x = Mathf.Log10(1 / guessedRatio) / Mathf.Log10(1 / ratio);
            inputField.GetComponent<TMP_InputField>().text = "X ist: " + x;
            //Debug.Log("X ist: " + x);
            buttonText.text = "Nächster Test";
        }
        else
        {
            Debug.Log("Es wurde noch nichts eingegeben.");
        }
    }

    void GetLogKreis(GameObject kreis)
    {
        Debug.Log("Der " + kreis.name + " hat einen Radius von: " + GetRadius(kreis));
        Debug.Log("Der Flächeninhalt von " + kreis.name + " beträgt: " + GetSurfaceAreaKreis(kreis));
    }
    void GetLogQuader(GameObject quader)
    {
        Debug.Log("Der " + quader.name + " hat eine Länge von: " + GetQuaderLength(quader));
        Debug.Log("Der Flächeninhalt von " + quader.name + " beträgt: " + GetSurfaceAreaQuader(quader));
    }
}
