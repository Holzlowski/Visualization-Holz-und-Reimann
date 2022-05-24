using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CarCardCreator : MonoBehaviour
{
    public GameObject carCardPrefab;
    //public List<CarCard> carCards;
    FileManager fm;
    public string[] data;
    public string[] subs;

    private void Start()
    {
        fm = GameObject.Find("FileManager").GetComponent<FileManager>();
        data = fm.carsData;

        if (fm != null)
        {
            for (int i = 0; i < 1; i++)
            {
                subs = data[1].Split('\t');

                GameObject carCard = Instantiate(carCardPrefab, Vector3.zero, Quaternion.identity, transform.parent);

                carCard.transform.Find("Autoname").GetComponent<TextMeshProUGUI>().text = subs[0];
                carCard.transform.Find("Hersteller").GetComponent<TextMeshProUGUI>().text = subs[1];
                carCard.transform.Find("MPG").GetComponent<TextMeshProUGUI>().text = subs[2];
                carCard.transform.Find("Cylinders").GetComponent<TextMeshProUGUI>().text = subs[3];
                carCard.transform.Find("Displacement").GetComponent<TextMeshProUGUI>().text = subs[4];
                carCard.transform.Find("Horsepower").GetComponent<TextMeshProUGUI>().text = subs[5];
                carCard.transform.Find("Weight").GetComponent<TextMeshProUGUI>().text = subs[6];
                carCard.transform.Find("Accelaration").GetComponent<TextMeshProUGUI>().text = subs[7];
                carCard.transform.Find("Model Year").GetComponent<TextMeshProUGUI>().text = subs[8];
            }

        }
        else
        {
            Debug.Log("Is nichts");
        }


    }
}
