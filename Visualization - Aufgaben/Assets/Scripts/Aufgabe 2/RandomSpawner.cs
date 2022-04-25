using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RandomSpawner : MonoBehaviour
{
    [SerializeField] int numberOfObjects;
    [SerializeField] float minDist;
    [SerializeField] GameObject obj;
    [SerializeField] GameObject leftPanel;
    [SerializeField] GameObject rightPanel;
    [SerializeField] GameObject beginButton;

    public int distanceToBorder = 2;

    private List<Vector3> spawnPosLeft;
    private List<Vector3> spawnPosRight;
    private Vector3[] cornerPointsLeft;
    private Vector3[] cornerPointsRight;
    private RectTransform panelLeft;
    private RectTransform panelRight;

    private List<GameObject> objects;
    private int maxAttempts = 900;
    private int testTimeInMs = 0;
    private string targetDirection;

    // Start is called before the first frame update
    void Start()
    {
        spawnPosLeft = new List<Vector3>();
        spawnPosRight = new List<Vector3>();

        panelLeft = leftPanel.GetComponent<RectTransform>();
        panelRight = rightPanel.GetComponent<RectTransform>();

        objects = new List<GameObject>();

        testTimeInMs = 100;
        GameObject.Find("TimeText").GetComponent<TextMeshProUGUI>().text = "Time: " + testTimeInMs + "ms";
    }

    public void Begin()
    {
        DisplayWorldCorners();
        StartCoroutine(RandomSpawn(spawnPosLeft, spawnPosRight));
        //beginButton.SetActive(false);
    }

    void Auswertung()
    {
        Debug.Log("Auswertung kommt hier hin");
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
    }

    IEnumerator RandomSpawn(List<Vector3> left, List<Vector3> right)
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            var attempts = 0;
            while (attempts < maxAttempts)
            {
                Vector3 randomPosLeft = GetRandomPositionLeft();
                Vector3 randomPosRight = GetRandomPositionRight();
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
                        targetDirection = "left";
                        break;
                    case 1:
                        obR = Instantiate(obj, right[i], Quaternion.identity, transform);
                        targetDirection = "right";
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
        float testTimeInS = testTimeInMs / 1000f;
        yield return new WaitForSeconds(testTimeInS);
        ClearObjects();

        testTimeInMs += 50;       
    }

    public void WaitForChoice(string choice)
    {
        if (choice == targetDirection)
        {
            Debug.Log("Richtig! Target bei "+ (testTimeInMs - 50) + "ms erkannt.");
            Reset();
        }
        else
        {
            Debug.Log("Falsch! Target bei " + (testTimeInMs-50) + "ms nicht korrekt erkannt.");
            GameObject.Find("TimeText").GetComponent<TextMeshProUGUI>().text = "Time: " + testTimeInMs + "ms";
            //beginButton.SetActive(true);
        }
    }

    Vector3 GetRandomPositionLeft()
    {
        Vector3 randomPos = new(Random.Range(cornerPointsLeft[0].x + distanceToBorder, cornerPointsLeft[3].x - distanceToBorder),
                                        Random.Range(cornerPointsLeft[1].y - distanceToBorder, cornerPointsLeft[0].y + distanceToBorder), panelLeft.position.z);
        return randomPos;
    }

    Vector3 GetRandomPositionRight()
    {
        Vector3 randomPos = new(Random.Range(cornerPointsRight[0].x + distanceToBorder, cornerPointsRight[3].x - distanceToBorder),
                                        Random.Range(cornerPointsRight[1].y - distanceToBorder, cornerPointsRight[0].y + distanceToBorder), panelRight.position.z);
        return randomPos;
    }

    void ClearObjects()
    {
        spawnPosLeft.Clear();
        spawnPosRight.Clear();

        foreach (GameObject ob in objects)
        {
            Destroy(ob);
        }

        objects.Clear();
    }

    public void Reset()
    {
        ClearObjects();
        testTimeInMs = 100;

        GameObject.Find("TimeText").GetComponent<TextMeshProUGUI>().text = "Time: " + testTimeInMs + "ms";
        //beginButton.SetActive(true);
    }
}

