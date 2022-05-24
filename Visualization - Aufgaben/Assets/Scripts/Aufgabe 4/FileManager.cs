using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FileManager : MonoBehaviour
{
    string[] carsData;
    string filePath, fileName;

    // Start is called before the first frame update
    void Start()
    {
        fileName = "cars.txt";
        filePath = Application.dataPath + "/" + fileName;
        ReadFromFile();
    }

    // Update is called once per frame
    public void ReadFromFile()
    {
        carsData = File.ReadAllLines(filePath);
        
        foreach(string line in carsData){
            print(line);
        }
    }
}
