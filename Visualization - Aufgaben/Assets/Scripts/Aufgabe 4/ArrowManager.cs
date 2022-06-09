using UnityEngine;

public class ArrowManager : MonoBehaviour
{
    public bool sortAsc = false;
    public void drehen()
    {
        if (!sortAsc)
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 270.0f);
        else
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 90.0f);
        sortAsc = !sortAsc;
    }
}
