using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomSpawner : MonoBehaviour
{
    [SerializeField] int numberOfObjects;
    [SerializeField] float minDist;
    [SerializeField] GameObject obj;
    [SerializeField] GameObject left;
    [SerializeField] GameObject right;

    public int distanceToBorder = 2;

    private List<Vector3> spawnPosLeft;
    private List<Vector3> spawnPosRight;
    private Vector3[] cornerPointsLeft;
    private Vector3[] cornerPointsRight;
    private RectTransform panelLeft;
    private RectTransform panelRight;

    private List<GameObject> objects;
    private int maxAttempts = 900;

    // Start is called before the first frame update
    void Start()
    {
        spawnPosLeft = new List<Vector3>();
        spawnPosRight = new List<Vector3>();

        panelLeft = left.GetComponent<RectTransform>();
        panelRight = right.GetComponent<RectTransform>();

        objects = new List<GameObject>();

        DisplayWorldCorners();
        RandomSpawn(spawnPosLeft, spawnPosRight);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void DisplayWorldCorners()
    {
        cornerPointsLeft = new Vector3[4];
        cornerPointsRight = new Vector3[4];
        // Funktion von Recttransform des Panels.
        // Das sind die Positonen der Eckpunkte des Panels.
        // Erster Punkt ist unten links und geht dann im Uhrzeigersinn weiter.

        panelLeft.GetWorldCorners(cornerPointsLeft);
        panelRight.GetWorldCorners(cornerPointsRight);

        Debug.Log("World Corners Left");
        for (var i = 0; i < 4; i++)
        {
            Debug.Log("World Corner " + i + " : " + cornerPointsLeft[i]);
        }

        Debug.Log("World Corners Right");
        for (var i = 0; i < 4; i++)
        {
            Debug.Log("World Corner " + i + " : " + cornerPointsRight[i]);
        }
    }

    void RandomSpawn(List<Vector3> left, List<Vector3> right)
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            var attempts = 0;
            while (attempts < maxAttempts)
            {
                Vector3 randomPosLeft = getRandomPositionLeft();
                Vector3 randomPosRight = getRandomPositionRight();
                var ok = true;
                foreach (var position in left)
                {
                    var dist = (randomPosLeft - position).magnitude;

                    if (dist < minDist)
                    {
                        ok = false;
                        break;
                    }
                }

                foreach (var position in right)
                {
                    var dist = (randomPosRight - position).magnitude;

                    if (dist < minDist)
                    {
                        ok = false;
                        break;
                    }
                }

                if (ok)
                {
                    left.Add(randomPosLeft);
                    right.Add(randomPosRight);
                    break;
                }

                attempts++;
            }

            GameObject obL = null;
            GameObject obR = null;

            if (i == numberOfObjects - 1)
            {
                obj.GetComponent<Image>().color = new Color(1, 0, 0, 1);
                switch (Random.Range(0, 2))
                {
                    case 0:
                        obL = Instantiate(obj, left[i], Quaternion.identity, transform);
                        break;
                    case 1:
                        obR = Instantiate(obj, right[i], Quaternion.identity, transform);
                        break;                   
                }
                obj.GetComponent<Image>().color = new Color(0, 0, 1, 1);
            }
            else
            {
                obL = Instantiate(obj, left[i], Quaternion.identity, transform);
                obR = Instantiate(obj, right[i], Quaternion.identity, transform);
            }
            
            objects.Add(obL);
            objects.Add(obR);
        }
    }

    Vector3 getRandomPositionLeft(){
       Vector3 randomPos = new(Random.Range(cornerPointsLeft[0].x + distanceToBorder, cornerPointsLeft[3].x - distanceToBorder),
                                       Random.Range(cornerPointsLeft[1].y - distanceToBorder , cornerPointsLeft[0].y + distanceToBorder), panelLeft.position.z);
        return randomPos;
    }

    Vector3 getRandomPositionRight()
    {
        Vector3 randomPos = new(Random.Range(cornerPointsRight[0].x + distanceToBorder, cornerPointsRight[3].x - distanceToBorder),
                                        Random.Range(cornerPointsRight[1].y - distanceToBorder, cornerPointsRight[0].y + distanceToBorder), panelRight.position.z);
        return randomPos;
    }

    void clearObjects(){
        spawnPosLeft.Clear();
        spawnPosRight.Clear();

        foreach (GameObject ob in objects){
            Destroy(ob);
        }

        objects.Clear();
    }

    public void Reset()
    {
        DisplayWorldCorners();
        clearObjects();
        RandomSpawn(spawnPosLeft, spawnPosRight);
    }
}

