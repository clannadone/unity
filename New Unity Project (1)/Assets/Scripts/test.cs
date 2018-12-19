using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public Transform walla;
    public Transform wallb;
    public int _x;
    public int _z;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Distance_x();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Distance_z();
        }
    }
    public void Distance_x()
    {
        var distance = Mathf.Abs(walla.position.x - wallb.position.x);
        Debug.Log("x轴距离"+distance);
    }
    public void Distance_z()
    {
        var distance = Mathf.Abs(walla.position.z - wallb.position.z);
        Debug.Log("z轴距离" + distance);
    }
}
