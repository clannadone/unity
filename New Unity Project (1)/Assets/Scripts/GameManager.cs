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
    public LayerMask pointMask;
    public LayerMask RedpieceMask = 1 << 10;
    public LayerMask BlackpieceMask = 1 << 10;

    public GameObject chessboard;

    public Point[,] points;
    public int width = 9;
    public int length = 10;
    Point _SelectedPoint;
    IPiece _SelectedPiece;

    private State state;

    private bool redTurn;
    PieceManager pieceManager;

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
                        _SelectedPiece = MainProcess(RedpieceMask).GetComponent<IPiece>();
     
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
                        _SelectedPiece = MainProcess(BlackpieceMask).GetComponent<IPiece>();
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
                        _SelectedPoint = MainProcess(RedpieceMask).GetComponent<Point>();
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
                            _SelectedPoint = null;
                        }
                    }
                }
                else
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        Debug.Log(redTurn);
                        _SelectedPoint = MainProcess(BlackpieceMask).GetComponent<Point>();
                        if (_SelectedPoint == null)
                        {
                            Debug.Log("0");
                            return;
                        }
                        if (TryMovePiece())
                        {
                            Debug.Log("1");
                            Debug.Log(redTurn);
                            _SelectedPiece = null;
                            _SelectedPoint = null;
                            redTurn = !redTurn;
                            state = State.Select;
                            Debug.Log(redTurn);
                        }
                        else
                        {
                            Debug.Log("2");
                            _SelectedPoint = null;
                        }
                    }
                }
                break;
        }

    }

    public GameObject MainProcess(LayerMask mask)
    {     
        //摄像机到点击位置的射线
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        //若点击的位置在棋盘内
        if (Physics.Raycast(ray, out hit, mask))
        {
            Debug.Log(hit.transform.gameObject.name);
            return hit.transform.gameObject;
        }
        return null;
    }

    public bool TryMovePiece()
    {
        if (_SelectedPiece.Move(_SelectedPoint))
        {
            points[_SelectedPoint.pointpos.x, _SelectedPoint.pointpos.z].piece = null;
            _SelectedPiece.SetPoisition(_SelectedPoint.pointpos.x, _SelectedPoint.pointpos.z);
            _SelectedPiece.SetTransformPoisition(_SelectedPoint.transform.position);
            _SelectedPoint.piece = _SelectedPiece;
            Debug.Log(redTurn);
            return true;
        }
        return false;

    }

   
}

