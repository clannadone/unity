using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public enum State
{
    Select,
    Move,
}
public class GameManager : MonoBehaviour
{
    public LayerMask pointMask = 1 << 9;
    public LayerMask RedPieceMask = 1 << 10;
    public LayerMask BlackPieceMask = 1 << 11;

    public GameObject chessboard;

    public Point[,] points;
    public int width = 9;
    public int length = 10;

    Point _SelectedPoint;
    IPiece _SelectedPiece;
    IPiece _TargetPiece;

    private State state;

    private bool redTurn = true;

    PieceManager pieceManager;
    public HUD hud;

    void Awake()
    {
        pieceManager = GetComponent<PieceManager>();
        points = new Point[width, length];
        Point[] temp = chessboard.transform.GetComponentsInChildren<Point>();
        int index = 0;
        for (int z = 0; z < length; z++)
        {
            for (int x = 0; x < width; x++)
            {
                points[x, z] = temp[index];
                temp[index].pointpos = new PointPos(x, z);
                index++;
            }
        }
        redTurn = true;
        state = State.Select;
    }
   // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Select:
                if (redTurn)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        GameObject obj = Click(RedPieceMask);
                        if (obj != null)
                            _SelectedPiece = obj.GetComponent<IPiece>();

                        if (_SelectedPiece == null)
                        {
                            return;
                        }
                        state = State.Move;
                    }
                }
                else
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        GameObject obj = Click(BlackPieceMask);
                        if (obj != null)
                            _SelectedPiece = obj.GetComponent<IPiece>();
                        if (_SelectedPiece == null)
                        {
                            return;
                        }
                        state = State.Move;
                    }
                }
                break;
            case State.Move:
                if (redTurn)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        GameObject obj = Click(pointMask);
                        if (obj != null)
                            _SelectedPoint = obj.GetComponent<Point>();

                        if (_SelectedPoint == null)
                        {
                            return;
                        }                   
                        if (TryMovePiece())
                        {
                            _SelectedPiece = null;
                            _SelectedPoint = null;
                            redTurn = !redTurn;
                            state = State.Select;
                        }
                        else
                        {
                            hud.ShowMessage("有棋子挡住了");
                            _SelectedPiece = null;
                            _SelectedPoint = null;
                            state = State.Select;
                        }
                    }
                }
                else
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        GameObject obj = Click(pointMask);
                        if (obj != null)
                            _SelectedPoint = obj.GetComponent<Point>();
                        if (_SelectedPoint == null)
                        {
                            return;
                        }
                        if (TryMovePiece())
                        {
                            _SelectedPiece = null;
                            _SelectedPoint = null;
                            redTurn = !redTurn;
                            state = State.Select;
                        }
                        else
                        {
                            hud.ShowMessage("有棋子挡住了");
                            _SelectedPiece = null;
                            _SelectedPoint = null;
                            state = State.Select;
                        }
                    }
                }
                break;
        }
    }
    public GameObject Click(LayerMask mask)
    {
        //摄像机到点击位置的射线
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        //若点击的位置在棋盘内
        if (Physics.Raycast(ray, out hit, 100, mask.value))
        {
       //  Debug.Log((int)mask);
          //  Debug.Log(hit.transform.gameObject.name);
            return hit.transform.gameObject;
        }
        return null;
    }
    public bool TryMovePiece()
    {
        if (_SelectedPiece.Move(_SelectedPoint))
        {
            PiecePos temp = _SelectedPiece.GetPoisition();
            points[temp.x, temp.z].piece = null;
            if (_SelectedPoint.piece != null)
            {
                _SelectedPoint.piece.Hide(_SelectedPoint);
            }
            points[_SelectedPoint.pointpos.x, _SelectedPoint.pointpos.z].piece = null;
            _SelectedPiece.SetPoisition(_SelectedPoint.pointpos.x, _SelectedPoint.pointpos.z);
            _SelectedPiece.SetTransformPoisition(_SelectedPoint.transform.position);
            _SelectedPoint.piece = _SelectedPiece;
    
            return true;
        }
        return false;
    }   
}

