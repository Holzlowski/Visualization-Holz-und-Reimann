using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DropdownManagerr : MonoBehaviour
{
    List<string> m_DropOptions = new List<string>();
    //This is the Dropdown
    TMP_Dropdown m_Dropdown;
    // Start is called before the first frame update

    public CarCardCreator ccc;

    void Start()
    {
        foreach(GameObject obj in ccc.carCardList){
            string herstellerText = obj.transform.Find("Hersteller").GetComponent<TextMeshProUGUI>().text;
            m_DropOptions.Add(herstellerText);
        }

        m_Dropdown = GetComponent<TMP_Dropdown>();
        //Clear the old options of the Dropdown menu
        m_Dropdown.ClearOptions();
        //Add the options created in the List above
        m_Dropdown.AddOptions(m_DropOptions);
    }
}
