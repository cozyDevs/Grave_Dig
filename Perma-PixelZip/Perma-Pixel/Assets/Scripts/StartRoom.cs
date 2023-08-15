using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRoom : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FloorTexture[] texts = gameObject.GetComponentsInChildren<FloorTexture>();
        foreach (FloorTexture s in texts)
        {
            s.Open();
        }
    }
}
