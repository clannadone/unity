using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PointType
{
    Move,
    Idea
}
[System.Serializable]
public class PointPos
{
    public int x, z;
 
 
    public PointPos(int x, int z)
    {
        this.x = x;
        this.z = z;
     
    }
}
public class Point : MonoBehaviour
{
    public IPiece piece=null;
    public Material MoveColor;
    public Material IdeaColor;
    Renderer renderer;
    public PointType pointType;
    public int index;
    public PointPos pointpos;
    // Use this for initialization
    void Awake()
    {
        renderer = GetComponent<Renderer>();
    }

    public void ChangeMoveColor()
    {
        renderer.material = MoveColor;
    }
    public void ChangeIdeaColor()
    {
        renderer.material = IdeaColor;

    }
}
