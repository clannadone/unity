using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PAO : MonoBehaviour, IPiece
{
    public PiecePos piecePos;
    public bool red;
    GameManager gameManager;
    public PieceType PieceType;
    public int index = 0;
    Point[] temporary;
    public bool GetPieceType(PieceType pieceType)
    {
        return pieceType == PieceType.PAO;
    }
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    public bool CheckPath(Point point)
    {
        int _index=0;
        temporary = gameManager.chessboard.transform.GetComponentsInChildren<Point>();
        if (point.pointpos.x == piecePos.x)
        {
            int temp = point.pointpos.z - piecePos.z;
            if (temp < 0)
            {
                for (int i = 1; i < piecePos.z - point.pointpos.z; i++)
                {
                    //如果移动路线中间偶遇障碍物 记住第一个障碍物的坐标 
                    // 计算和目标的距离 并判断中间有没有第二个障碍物
                    if (gameManager.points[point.pointpos.x, piecePos.z - i].piece != null)
                    {
                       
                        //for (int j = 0; j < ; j++)
                        //{

                        //}
                        Debug.Log("前方有棋子挡住了");
                        return false;
                    }
                }
            }
            else if (temp > 0)
            {
                for (int i = 1; i < point.pointpos.z - piecePos.z; i++)
                {
                    if (gameManager.points[piecePos.x, piecePos.z + i].piece != null)
                    {
                        gameManager.points[point.pointpos.x, piecePos.z - i] = temporary[index];
                        temporary[index].pointpos = new PointPos(point.pointpos.x, piecePos.z - i);
                        _index++;
                        Debug.Log("前方有棋子挡住了");
                        return false;
                    }
                }
            }
        }
        else if (point.pointpos.z == piecePos.z)
        {
            int temp = point.pointpos.x - piecePos.x;
            if (temp < 0)
            {
                for (int i = 1; i < piecePos.x - point.pointpos.x; i++)
                {
                    if (gameManager.points[piecePos.x - i, piecePos.z].piece != null)
                    {
                        gameManager.points[point.pointpos.x, piecePos.z - i] = temporary[index];
                        _index++;
                        Debug.Log("左方有棋子挡住了");
                        return false;
                    }
                }
            }
            else if (temp > 0)
            {
                Debug.Log("piecePos.x" + piecePos.x);
                for (int i = 1; i < point.pointpos.x - piecePos.x; i++)
                {
                    if (gameManager.points[piecePos.x + i, piecePos.z].piece != null)
                    {
                        gameManager.points[point.pointpos.x, piecePos.z - i] = temporary[index];
                        _index++;
                        Debug.Log("右方有棋子挡住了");
                        return false;
                    }
                }
            }
        }
        return true;
    }
    public void Hide(Point point)
    {
        if (index == 1)
        {
            gameObject.SetActive(false);
        }      
    }

    public bool Move(Point point)
    {
        Debug.Log(index);
        if (point.piece != null && point.piece.GetTurn() == red) return false;
        if (red)
        {
            if (gameManager.points[point.pointpos.x, point.pointpos.z].piece == null)
            {
                if (Mathf.Abs(point.pointpos.z - piecePos.z) > 0 && Mathf.Abs(point.pointpos.x - piecePos.x) == 0 && CheckPath(point))
                {
                    return true;
                }
                else if (Mathf.Abs(point.pointpos.x - piecePos.x) > 0 && Mathf.Abs(point.pointpos.z - piecePos.z) == 0 && CheckPath(point))
                {
                    return true;
                }
            }
            else if(temporary[index].index==1)
            {
               
               
                  return true;
                
            }

            return false;
        }
        else
        {
            if (gameManager.points[point.pointpos.x, point.pointpos.z].piece == null)
            {
                if (Mathf.Abs(point.pointpos.z - piecePos.z) > 0 && Mathf.Abs(point.pointpos.x - piecePos.x) == 0 && CheckPath(point))
                {
                    return true;
                }
                else if (Mathf.Abs(point.pointpos.x - piecePos.x) > 0 && Mathf.Abs(point.pointpos.z - piecePos.z) == 0 && CheckPath(point))
                {
                    return true;
                }
            }
            else if (temporary[index].index == 1)
            {
                Debug.Log(index);

                return true;

            }
            return false;
        }
    }
    public void SetPoisition(int x, int z)
    {
        piecePos = new PiecePos(x, z);
    }

    public PiecePos GetPoisition()
    {
        return this.piecePos;
    }

    public void SetTransformPoisition(Vector3 vec)
    {
        transform.position = vec;
    }
    public void SetTurn(bool red)
    {
        this.red = red;
    }
    public bool GetTurn()
    {
        return red;
    }
}
