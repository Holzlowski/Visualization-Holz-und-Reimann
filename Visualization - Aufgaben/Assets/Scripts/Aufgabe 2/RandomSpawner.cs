using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RandomSpawner : MonoBehaviour
{
    [SerializeField] int numberOfObjects;
    [SerializeField] float minDist;
    [SerializeField] GameObject circleObj;
    [SerializeField] GameObject quaderObj;
    [SerializeField] GameObject rectObj;
    [SerializeField] GameObject leftPanel;
    [SerializeField] GameObject rightPanel;
    [SerializeField] GameObject objectSelector;

    public int distanceToBorder = 2;
    //hier wollte ich die Anzahl aller Test festhalten, aber hab das bis jetzt nicht benötigt
    public int numberOfTests;
    //die Zahl des momentanen Test, fängt bei 0 an
    public int currentTestNumber;
    public int testTimeInMs = 100;

    private List<Vector3> spawnPosLeft;
    private List<Vector3> spawnPosRight;
    private Vector3[] cornerPointsLeft;
    private Vector3[] cornerPointsRight;
    private RectTransform panelLeft;
    private RectTransform panelRight;

    private TMP_Dropdown objectDropdown;
    private List<GameObject> objects;
    private GameObject currentObj;
    private int maxAttempts = 900;
    private string targetDirection;
    //ein Bool der True ist wenn es eine Vielfalt der Distraktoren oder Conjunction Search geben soll
    private bool distractionDiversity = false;
    private ArrayList resultList;
    private string testName;

    // Start is called before the first frame update
    void Start()
    {
        spawnPosLeft = new List<Vector3>();
        spawnPosRight = new List<Vector3>();

        panelLeft = leftPanel.GetComponent<RectTransform>();
        panelRight = rightPanel.GetComponent<RectTransform>();

        objectDropdown = objectSelector.GetComponent<TMP_Dropdown>();
        currentObj = circleObj;
        objects = new List<GameObject>();
        resultList = new ArrayList();
        NextTest();
    }

    //mit dem Next Test Button geht er in den nächsten Test
    public void NextTest()
    {
        currentTestNumber++;
        switch (currentTestNumber)
        {
            case 1:
                testName = "Farbton";
                break;
            case 2:
                testName = "Form";
                break;
            case 3:
                testName = "Erhöhte Vielfalt der Distraktoren";
                distractionDiversity = true;
                break;
            case 4:
                testName = "Conjunction Search";
                break;
            case 5:
                currentTestNumber = 0;
                Auswertung();
                NextTest();
                break;
        }
        testTimeInMs = 100;
        UpdateTimeText();
        UpdateTestText();
    }

    public void Begin()
    {
        DisplayWorldCorners();
        StartCoroutine(RandomSpawn(spawnPosLeft, spawnPosRight));
    }

    void Auswertung()
    {
        Debug.Log("Auswertung kommt hier hin. Zeiten für die jeweiligen Tests: "+ resultList[0] + "ms | " + resultList[1] + "ms | " + resultList[2] + "ms | " + resultList[3] + "ms.");
    }

    public void changeObj()
    {
        Debug.Log(objectDropdown.value);
        if (objectDropdown.value == 0)
        {
            currentObj = circleObj;
        }
        else if (objectDropdown.value == 1)
        {
            currentObj = quaderObj;
        }
        else if (objectDropdown.value == 2)
        {
            currentObj = rectObj;
        }
        Reset();
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
                Debug.Log(testName);

                if (testName == "Form")
                {
                    quaderObj.GetComponent<Image>().color = new Color(0, 0, 1, 1);
                    switch (Random.Range(0, 2))
                    {
                        case 0:
                            obL = Instantiate(quaderObj, left[i], Quaternion.identity, transform);
                            targetDirection = "left";
                            break;
                        case 1:
                            obR = Instantiate(quaderObj, right[i], Quaternion.identity, transform);
                            targetDirection = "right";
                            break;
                    }
                }
                else
                {
                    currentObj.GetComponent<Image>().color = new Color(1, 0, 0, 1);
                    switch (Random.Range(0, 2))
                    {
                        case 0:
                            obL = Instantiate(currentObj, left[i], Quaternion.identity, transform);
                            targetDirection = "left";
                            break;
                        case 1:
                            obR = Instantiate(currentObj, right[i], Quaternion.identity, transform);
                            targetDirection = "right";
                            break;
                    }
                    currentObj.GetComponent<Image>().color = new Color(0, 0, 1, 1);
                }
            }
            //wenn Bool an ist, dann werden Random Objekte gespanwt
            else if (distractionDiversity)
            {
                obL = Instantiate(RandomObject(), left[i], Quaternion.identity, transform);
                obR = Instantiate(RandomObject(), right[i], Quaternion.identity, transform);
            }
            else
            {
                obL = Instantiate(currentObj, left[i], Quaternion.identity, transform);
                obR = Instantiate(currentObj, right[i], Quaternion.identity, transform);
            }

            objects.Add(obL);
            objects.Add(obR);
        }

        yield return new WaitForSeconds((float)testTimeInMs / 1000.0f);
        ClearObjects();

        //testTimeInMs += 50;
    }

    public void WaitForChoice(string choice)
    {

        if (choice == targetDirection)
        {
            Debug.Log("Richtig! Target bei " + (testTimeInMs) + "ms erkannt.");
            resultList.Add(testTimeInMs);
            NextTest();
        }
        else if (choice == "none")
        {
            testTimeInMs += 50;
            UpdateTimeText();
            Debug.Log("Target bei " + (testTimeInMs) + "ms nicht erkannt.");
        }
        else
        {
            testTimeInMs += 50;
            UpdateTimeText();
            Debug.Log("Falsch! Target bei " + (testTimeInMs) + "ms nicht korrekt erkannt.");
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
        UpdateTimeText();
    }

    //Text der Zeit wird geupdated
    private void UpdateTimeText()
    {
        GameObject.Find("TimeText").GetComponent<TextMeshProUGUI>().text = "Time: " + testTimeInMs + "ms";
    }

    private void UpdateTestText()
    {
        GameObject.Find("TestText").GetComponent<TextMeshProUGUI>().text = "Test: " + testName;
    }

    //hier wird das random Objekt ausgewählt, das gespanwt wird, um eine Vielfalt der Distraktoren zu erzeugen
    private GameObject RandomObject()
    {
        int randomNumber = Random.Range(0, 3);
        GameObject randomObject;
        if (randomNumber == 0)
        {
            randomObject = quaderObj;

            //macht alle Quadrate rot, wenn der Test dran ist, wo Conjunction Search getestet werden soll
            if (testName == "Conjunction Search")
            {
                randomObject.GetComponent<Image>().color = Color.red;
            }
            return randomObject;
        }
        else if (randomNumber == 1 && testName != "Conjunction Search")
        {
            randomObject = rectObj;
            randomObject.GetComponent<Image>().color = Color.blue;
            return randomObject;
        }
        else
        { 
            randomObject = circleObj;
            return randomObject;
        }
    }
}

