using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FileManager : MonoBehaviour
{
    public string[] data;
    List<string> dataList;
    string filePath, fileName;

    // Start is called before the first frame update
    void Awake()
    {
        fileName = "cars.txt";
        filePath = Application.dataPath + "/" + fileName;
        ReadFromFile();
    }

    // Update is called once per frame
    public void ReadFromFile()
    {
       data = File.ReadAllLines(filePath);
       dataList = new List<string>(data);
       //weil erster Datensatz nur die Attribute wie Hersteller etc. sind
       dataList.RemoveAt(0);
       data = dataList.ToArray();
    }
}
