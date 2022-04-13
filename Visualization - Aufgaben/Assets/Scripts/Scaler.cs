using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaler : MonoBehaviour
{
    [SerializeField] float scale = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.localScale != new Vector3 (scale, transform.localScale.y, scale)){
            transform.localScale = new Vector3 (scale, transform.localScale.y, scale);
        }
    }
}
