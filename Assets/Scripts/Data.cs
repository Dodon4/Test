using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data
{
    public float[] position = new float[3]; 
    public Data(GameObject square)
    {
        Vector3 squarePos = square.transform.position;

        position = new float[]
        {
            squarePos.x, squarePos.y, squarePos.z
        };
    }
}
