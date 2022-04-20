using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    [SerializeField] int numberOfObjects;
    [SerializeField] float minDist;
    [SerializeField] GameObject obj;
    private List<Vector3> spawnPos;
    private Vector3[] cornerPoints;
    private RectTransform panel;
    private int maxAttempts = 100;



    // Start is called before the first frame update
    void Start()
    {
        spawnPos = new List<Vector3>();
        panel = GetComponent<RectTransform>();
        DisplayWorldCorners();
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

    void RandomSpawn()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            spawnPos[i] = default(Vector3);
            var attempts = 0;
            while (attempts < maxAttempts)
            {
                spawnPos[i] = new Vector3(Random.Range(cornerPoints[0].x, cornerPoints[3].x), Random.Range(cornerPoints[1].y, cornerPoints[0].y), panel.position.z);
                var ok = true;
                foreach (var position in this.spawnPos)
                {
                    var dist = (spawnPos[i] - position).magnitude;
                    if (dist < minDist)
                    {
                        ok = false;
                        break;
                    }
                }

                if (ok)
                {
                    break;
                }

                attempts++;
            }

            Instantiate(obj, spawnPos[i], Quaternion.identity);

        }
    }

    private void Reset()
    {
        DisplayWorldCorners();
    }
}

