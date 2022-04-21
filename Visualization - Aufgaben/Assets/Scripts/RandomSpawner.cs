using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    [SerializeField] int numberOfObjects;
    [SerializeField] float minDist;
    [SerializeField] GameObject obj;
    public int distanceToBorder = 2;
    private List<Vector3> spawnPos;
    private List<GameObject> objects;
    private Vector3[] cornerPoints;
    private RectTransform panel;
    private int maxAttempts = 900;




    // Start is called before the first frame update
    void Start()
    {
        spawnPos = new List<Vector3>();
        objects = new List<GameObject>();
        panel = GetComponent<RectTransform>();
        DisplayWorldCorners();
        RandomSpawn(GetSpawnPos());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void DisplayWorldCorners()
    {
        cornerPoints = new Vector3[4];
        // Funktion von Recttransform des Panels.
        // Das sind die Positonen der Eckpunkte des Panels.
        // Erster Punkt ist unten links und geht dann im Uhrzeigersinn weiter.
        panel.GetWorldCorners(cornerPoints);

        Debug.Log("World Corners");
        for (var i = 0; i < 4; i++)
        {
            Debug.Log("World Corner " + i + " : " + cornerPoints[i]);
        }
    }

    private List<Vector3> GetSpawnPos()
    {
        return spawnPos;
    }

    void RandomSpawn(List<Vector3> spawnPos)
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            var attempts = 0;
            while (attempts < maxAttempts)
            {
                Vector3 randomPos = getRandomPosition();
                var ok = true;
                foreach (var position in this.spawnPos)
                {
                    var dist = (randomPos - position).magnitude;

                    if (dist < minDist)
                    {
                        ok = false;
                        break;
                    }
                }

                if (ok)
                {   
                    spawnPos.Add(randomPos);
                    break;
                }

                attempts++;
            }

            GameObject ob = Instantiate(obj, spawnPos[i], Quaternion.identity, transform);
            objects.Add(ob);

        }
    }

    Vector3 getRandomPosition(){
       Vector3 randomPos = new Vector3(Random.Range(cornerPoints[0].x + distanceToBorder, cornerPoints[3].x - distanceToBorder),
                                       Random.Range(cornerPoints[1].y - distanceToBorder , cornerPoints[0].y + distanceToBorder), panel.position.z);
        return randomPos;
    }

    void clearObjects(){
        spawnPos.Clear();
        foreach(GameObject ob in objects){
            Destroy(ob);
        }
        objects.Clear();
    }

    public void Reset()
    {
        DisplayWorldCorners();
        clearObjects();
        RandomSpawn(GetSpawnPos());
    }
}

