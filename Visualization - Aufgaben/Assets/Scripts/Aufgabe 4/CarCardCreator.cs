using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class CarCardCreator : MonoBehaviour
{
    public GameObject carCardPrefab;
    public Transform parent;
    public List<string> carDataList;
    FileManager fm;
    public string[] carData;
    public string[] subs;
    public List<GameObject> carCardList = new List<GameObject>();

    private void Start()
    {
        InitializeCarCards();
    }

    void InitializeCarCards()
    {
        fm = GameObject.Find("FileManager").GetComponent<FileManager>();
        carData = fm.data;


        if (fm != null)
        {
            CreateCards();
        }
    }

    void CreateCards()
    {
        foreach (var car in carData)
            {
                subs = car.Split('\t');
                GameObject carCard = Instantiate(carCardPrefab, parent);
                
                carCard.transform.Find("Autoname").GetComponent<TextMeshProUGUI>().text = subs[0];
                carCard.transform.Find("Hersteller").GetComponent<TextMeshProUGUI>().text = subs[1];
                carCard.transform.Find("MPG").GetComponent<TextMeshProUGUI>().text = subs[2];
                carCard.transform.Find("Cylinders").GetComponent<TextMeshProUGUI>().text = subs[3];
                carCard.transform.Find("Displacement").GetComponent<TextMeshProUGUI>().text = subs[4];
                carCard.transform.Find("Horsepower").GetComponent<TextMeshProUGUI>().text = subs[5];
                carCard.transform.Find("Weight").GetComponent<TextMeshProUGUI>().text = subs[6];
                carCard.transform.Find("Accelaration").GetComponent<TextMeshProUGUI>().text = subs[7];
                carCard.transform.Find("Model Year").GetComponent<TextMeshProUGUI>().text = subs[8];
                
                switch (subs[9])
                {
                    case "American":
                    carCard.transform.Find("Origin").GetComponent<TextMeshProUGUI>().text = "A";
                    break;
                    case "European":
                    carCard.transform.Find("Origin").GetComponent<TextMeshProUGUI>().text = "E";
                    break;
                    case "Japanese":
                    carCard.transform.Find("Origin").GetComponent<TextMeshProUGUI>().text = "J";
                    break;
                    default:
                    carCard.transform.Find("Origin").GetComponent<TextMeshProUGUI>().text = " ";
                    break;
                }

                carCardList.Add(carCard);
            }
    }

    public void FilterCardsOrigin (string origin)
    {
        Debug.Log(carCardList.Count);
        
        List<GameObject> displayList = (from car in carCardList
            where car.transform.Find("Origin").GetComponent<TextMeshProUGUI>().text == origin
            select car).ToList();

        Debug.Log(displayList.Count);

        foreach (Transform child in transform) {
            GameObject.Destroy(child.gameObject);
        }

        foreach (GameObject obj in displayList){
            Instantiate(obj, parent);
        }
    }
}
