using UnityEngine;

public class Scaler : MonoBehaviour
{
    //[SerializeField] float scale = 2;
    private float scale = 2;

    public float Scale => scale;
    // Start is called before the first frame update

    public void RandomizeScale()
    {
        scale = Random.Range(1.5f, 4.5f);
        transform.localScale = new Vector3(scale, transform.localScale.y, scale);
    }
}
