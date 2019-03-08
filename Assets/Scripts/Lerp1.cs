using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lerp1 : MonoBehaviour
{
    Vector3 pointA = new Vector3(-15, 10, 0);
    Vector3 pointB = new Vector3(15, 9, 0);
    void Update()
    {
        transform.position = Vector3.Lerp(pointA, pointB, Mathf.PingPong(Time.time, 3));
    }
}
