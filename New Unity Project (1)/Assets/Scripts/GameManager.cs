using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.Events;

public enum State
{
    Select,
    Move,
    GameOver,
}
public class GameManager : MonoBehaviour
{
    public delegate void MyDelegate(bool redTurn, string log, bool IsError = false);
    public event MyDelegate myDelegate;


    public LayerMask pointMask = 1 << 9;
    public LayerMask RedPieceMask = 1 << 10;
    public LayerMask BlackPieceMask = 1 << 11;

    public GameObject chessboard;

    public Point[,] points;
    public int width = 9;
    public int length = 10;

    public  Point _SelectedPoint;
   public  IPiece _SelectedPiece;


    IPiece _TargetPiece;

    IPiece redJinag;
    IPiece blackJiang;

    private State state;

    public bool redTurn = true;

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

        List<IPiece> tep = pieceManager.jiang;

        //for (int i = 0; i < 1; i++)
        //{
        //    if (tep[i].GetTurn())
        //    {
        //        redJinag = tep[i];
        //    }
        //    else
        //    {
        //        blackJiang = tep[i];
        //    }
        //}
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
                            if (myDelegate != null)
                            {
                                myDelegate.Invoke(redTurn, Error(), true);
                            }
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
                            if (myDelegate != null)
                            {
                                myDelegate.Invoke(redTurn, Error(), true);
                            }
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
        // Debug.Log("1");
        //摄像机到点击位置的射线
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        //若点击的位置在棋盘内
        if (Physics.Raycast(ray, out hit, 100, mask.value))
        {
            //  Debug.Log((int)mask);
            //   Debug.Log("名称"+hit.transform.gameObject.name);
            return hit.transform.gameObject;
        }
        return null;
    }
    public bool TryMovePiece()
    {
        if (_SelectedPiece.Move(_SelectedPoint))
        {
            int movex = _SelectedPoint.pointpos.x - _SelectedPiece.GetPoisition().x;
            int movez = _SelectedPoint.pointpos.z - _SelectedPiece.GetPoisition().z;
            string mox =
                string.Format("{0}{1}{2}{3}",
                _SelectedPiece.PieceToString(),

                redTurn ? MoveNum(_SelectedPiece.GetPoisition().x) : MoveNum(Mathf.Abs(_SelectedPiece.GetPoisition().x - 8)),

                redTurn ? DecideMoveRedString(movex, movez) : DecideMoveBlackString(movex, movez),

                _SelectedPiece.GetPieceType() == PieceType.MA ||
                _SelectedPiece.GetPieceType() == PieceType.XIANG ||
                _SelectedPiece.GetPieceType() == PieceType.SHI ?
                MoveNum(Mathf.Abs(Mathf.Abs(movex) - 9)) : MoveNum(Mathf.Abs(Mathf.Abs(MoveDir(movex, movez)) - 9)));

            if (myDelegate != null)
            {
                myDelegate.Invoke(redTurn
                    , mox);
            }
            PiecePos temp = _SelectedPiece.GetPoisition();
            points[temp.x, temp.z].piece = null;
            if (_SelectedPoint.piece != null)
            {
                if (_SelectedPoint.piece.GetPieceType() == PieceType.JIANG)
                {
                    string over = "将军";
                    myDelegate.Invoke(redTurn, over);
                    state = State.GameOver;
                }
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
    public bool JiangJun(Point point,bool red)
    {
        int _x;
        int _z;
        int index;
        if (red)
        {
             _x = redJinag.GetPoisition().x;
             _z = redJinag.GetPoisition().z;
        }
        else
        {
             _x = blackJiang.GetPoisition().x;
             _z = blackJiang.GetPoisition().z;
        }
        List<IPiece> temp = new List<IPiece>();
        //右
        for (int i = 0; i < 8 - _x; i++)
        {
            //车检测
            if (points[_x + i, _z].piece!=null&& points[_x + i, _z].piece.GetTurn()!=red)
            {
                temp.Add(points[_x + i, _z].piece);
            }
            //炮检测
            else if(points[_x + i, _z].piece != null)
            {
                for (int j = _x+i; j < 8-(_x+i); j++)
                {
                    if(points[_x + i+j, _z].piece != null && points[_x + i+j, _z].piece.GetTurn() != red)
                    {
                        temp.Add(points[_x + i+j, _z].piece);
                    }
                }
            }
        }
        //左
        for (int i = 0; i < _x; i++)
        {
            //车检测
            if (points[_x - i, _z].piece!= null && points[_x - i, _z].piece.GetTurn() != red)
            {
                temp.Add(points[_x - i, _z].piece);
            }
        }
        //上
        for (int i = 0; i < 9 - _z; i++)
        {
            //车检测
            if (points[_x, _z+i].piece != null && points[_x , _z+i].piece.GetTurn() != red)
            {
                temp.Add(points[_x , _z+i].piece);
            }
        }
        for (int i = 0; i < _z; i++)
        {
            //车检测
            if (points[_x , _z-i].piece != null && points[_x , _z-i].piece.GetTurn() != red)
            {
                temp.Add(points[_x, _z - i].piece);
            }
        }
        //马检测
        if (points[_x+1,_z+2].piece!=null && points[_x + 1, _z + 2].piece.GetTurn() != red) temp.Add(points[_x + 1, _z + 2].piece);
        if (points[_x+2,_z+1].piece!=null && points[_x + 1, _z + 2].piece.GetTurn() != red) temp.Add(points[_x + 1, _z + 2].piece);
        if (points[_x+1,_z-2].piece!=null && points[_x + 1, _z + 2].piece.GetTurn() != red) temp.Add(points[_x + 1, _z + 2].piece);
        if (points[_x+1,_z-1].piece!=null && points[_x + 1, _z + 2].piece.GetTurn() != red) temp.Add(points[_x + 1, _z + 2].piece);
        if (points[_x-1,_z+2].piece!=null && points[_x + 1, _z + 2].piece.GetTurn() != red) temp.Add(points[_x + 1, _z + 2].piece);
        if (points[_x-1,_z+1].piece!=null && points[_x + 1, _z + 2].piece.GetTurn() != red) temp.Add(points[_x + 1, _z + 2].piece);
        if (points[_x-1,_z-2].piece!=null && points[_x + 1, _z + 2].piece.GetTurn() != red) temp.Add(points[_x + 1, _z + 2].piece);
        if (points[_x-1,_z-2].piece!=null && points[_x + 1, _z + 2].piece.GetTurn() != red) temp.Add(points[_x + 1, _z + 2].piece);

        

        foreach (var item in temp)
        {
            if(item.Move(points[_x, _z]))
            {
                return true;
            }
        }
        return false;
    }
    public string Error()
    {
        return "不能这么做";
    }
    public int MoveDir(int movex, int movez)
    {
        if (movex != 0 && movez == 0)
        {
            return movex;
        }
        else if (movez != 0 && movex == 0)
        {
            return movez;
        }
        return 0;
    }
    public string DecideMoveRedString(int movex, int movez)
    {
        if (movez == 0 && movex != 0)
        {
            return "平";
        }
        else if (movez > 0)
        {
            return "进";
        }
        else if (movez < 0)
        {
            return "退";
        }
        return "";
    }
    public string DecideMoveBlackString(int movex, int movez)
    {
        if (movez == 0 && movex != 0)
        {
            return "平";
        }
        else if (movez > 0)
        {
            return "退";
        }
        else if (movez < 0)
        {
            return "进";
        }
        return "";
    }
    public string MoveNum(int temp)
    {
        switch (temp)
        {
            case 0:
                return "九";
            case 1:
                return "八";
            case 2:
                return "七";
            case 3:
                return "六";
            case 4:
                return "五";
            case 5:
                return "四";
            case 6:
                return "三";
            case 7:
                return "二";
            case 8:
                return "一";
        }
        return "";
    }


}

