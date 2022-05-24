using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCard : MonoBehaviour
{
    [SerializeField] string carModel;
    [SerializeField] string manufacturer;
    [SerializeField] string MPG;
    [SerializeField] string cylinders;
    [SerializeField] string displacement;
    [SerializeField] string horsepower;
    [SerializeField] string weight;
    [SerializeField] string acceleration;
    [SerializeField] string modelYear;
    [SerializeField] string origin;

    public CarCard(string carModel, string manufacturer, string MPG, string cylinders,
                string displacement, string horsepower, string weight, string acceleration, string modelYear, string origin)
    {
        this.carModel = carModel;
        this.manufacturer = manufacturer;
        this.MPG = MPG;
        this.cylinders = cylinders;
        this.displacement = displacement;
        this.horsepower = horsepower;
        this.weight = weight;
        this.acceleration = acceleration;
        this.modelYear = modelYear;
        this.origin = origin;
    }

    public string toString()
    {
        return "carModel" + carModel + "manufacturer" + manufacturer + "MPG" +
        MPG + "cylinders" + cylinders + "displacement" + displacement + "horsepower" +
        horsepower + "weight" + weight + "acceleration" + acceleration + "modelYear" + modelYear + "origin" + origin;
    }
}
