using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
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
    public Color A, E, J;
    public List<Toggle> toggleList = new List<Toggle>();
    public List<GameObject> displayList = new List<GameObject>();

    private void Start()
    {
        InitializeCarCards();
    }

    void InitializeCarCards()
    {
        fm = GameObject.Find("FileManager").GetComponent<FileManager>();
        carData = fm.data;

        displayList = carCardList;


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
            Image cardColor = carCard.GetComponent<Image>();

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
                    cardColor.color = A;
                    break;
                case "European":
                    carCard.transform.Find("Origin").GetComponent<TextMeshProUGUI>().text = "E";
                    cardColor.color = E;
                    break;
                case "Japanese":
                    carCard.transform.Find("Origin").GetComponent<TextMeshProUGUI>().text = "J";
                    cardColor.color = J;
                    break;
                default:
                    carCard.transform.Find("Origin").GetComponent<TextMeshProUGUI>().text = " ";
                    break;
            }

            carCardList.Add(carCard);
        }
    }

    public void FilterCardsOrigin(Toggle toggle)
    {
        switch (toggle.isOn)
        {
            case true:
                if (toggle.name == "Japan")
                {
                    foreach (Transform child in parent)
                    {
                        if (child.Find("Origin").GetComponent<TextMeshProUGUI>().text == "J")
                            child.gameObject.SetActive(true);
                    }
                }
                if (toggle.name == "Europa")
                {
                    foreach (Transform child in parent)
                    {
                        if (child.Find("Origin").GetComponent<TextMeshProUGUI>().text == "E")
                            child.gameObject.SetActive(true);
                    }
                }
                if (toggle.name == "America")
                {
                    foreach (Transform child in parent)
                    {
                        if (child.Find("Origin").GetComponent<TextMeshProUGUI>().text == "A")
                            child.gameObject.SetActive(true);
                    }
                }
                break;
            case false:
                if (toggle.name == "Japan")
                {
                   foreach (Transform child in parent)
                    {
                        if (child.Find("Origin").GetComponent<TextMeshProUGUI>().text == "J")
                            child.gameObject.SetActive(false);
                    }
                }
                if (toggle.name == "Europa")
                {
                    foreach (Transform child in parent)
                    {
                        if (child.Find("Origin").GetComponent<TextMeshProUGUI>().text == "E")
                            child.gameObject.SetActive(false);
                    }
                }
                if (toggle.name == "America")
                {
                    foreach (Transform child in parent)
                    {
                        if (child.Find("Origin").GetComponent<TextMeshProUGUI>().text == "A")
                            child.gameObject.SetActive(false);
                    }
                }
                break;
        }
    }
}
