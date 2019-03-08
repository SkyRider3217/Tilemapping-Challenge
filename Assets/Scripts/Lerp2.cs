using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lerp2 : MonoBehaviour
{
    Vector3 pointA = new Vector3(60, -2, 0);
    Vector3 pointB = new Vector3(100, -2, 0);
    void Update()
    {
        transform.position = Vector3.Lerp(pointA, pointB, Mathf.PingPong(Time.time, 3));
    }
}
