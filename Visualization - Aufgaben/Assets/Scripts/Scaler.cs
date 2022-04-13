using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaler : MonoBehaviour
{
    //[SerializeField] float scale = 2;
    private float scale = 2;


    public float Scale => scale;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RandomizeScale(){
        scale = Random.Range(1.5f, 5);
        transform.localScale = new Vector3 (scale, transform.localScale.y, scale); 
    }
}
