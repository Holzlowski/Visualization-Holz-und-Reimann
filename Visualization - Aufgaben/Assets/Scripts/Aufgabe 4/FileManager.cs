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
            string[] subs = line.Split('\t');
            CarCard newCar = new CarCard(subs[0], subs[1], subs[2], subs[3], subs[4], subs[5], subs[6], subs[7], subs[8], subs[9]);
        }
    }
}
