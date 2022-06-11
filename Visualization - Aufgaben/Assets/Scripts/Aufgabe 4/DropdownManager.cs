using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DropdownManager : MonoBehaviour
{
    List<string> m_DropOptions = new List<string>();
    //This is the Dropdown
    TMP_Dropdown m_Dropdown;
    // Start is called before the first frame update

    public CarCardCreator ccc;

    private void Start()
    {
        UpdateDropdown();
    }

    void UpdateDropdown()
    {
        m_Dropdown = GetComponent<TMP_Dropdown>();
        //Clear the old options of the Dropdown menu
        m_Dropdown.ClearOptions();
        foreach (Transform child in ccc.parent)
        {
            if (child.gameObject.activeInHierarchy)
            {
                string herstellerText = child.transform.Find("Hersteller").GetComponent<TextMeshProUGUI>().text;
                if (!m_DropOptions.Contains(herstellerText))
                    m_DropOptions.Add(herstellerText);
            }
        }

        m_Dropdown = GetComponent<TMP_Dropdown>();
        
        //Add the options created in the List above
        m_Dropdown.AddOptions(m_DropOptions);
    }

    public void clearOptions()
    {
        m_DropOptions.Clear();
        UpdateDropdown();
    }
}
