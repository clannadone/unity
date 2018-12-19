using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PointType
{
    Move,
    Idea
}
public class Point : MonoBehaviour
{
    public Material MoveColor;
    public Material IdeaColor;
    Renderer renderer;
    public PointType pointType;
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
