using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PAO : MonoBehaviour, IPiece
{
    public PiecePos piecePos;
    public bool red;
    GameManager gameManager;
    public PieceType PieceType;
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
        if (point.pointpos.x == piecePos.x)
        {
            int temp = point.pointpos.z - piecePos.z;
            if (temp < 0)
            {

                for (int i = 1; i < piecePos.z - point.pointpos.z; i++)
                {
                    //1*如果移动路线中间有障碍物 记住第一个障碍物的坐标 
                    //2*第一个障碍物的坐标计算和目标的距离 并判断中间有没有第二个障碍物
                    if (gameManager.points[point.pointpos.x, piecePos.z - i].piece != null)
                    {
                        if (gameManager.points[point.pointpos.x, point.pointpos.z].piece == null)
                        {
                            Debug.Log("前方有棋子挡住了");
                            return false;
                        }
                        for (int j = point.pointpos.z + i; j < piecePos.z - i; j++)
                        {
                            if (gameManager.points[point.pointpos.x, piecePos.z - i - j].piece != null)
                            {
                                Debug.Log("前方有棋子挡住了");
                                return false;
                            }
                        }
                    }
                }
            }
            else if (temp > 0)
            {
                for (int i = 1; i < point.pointpos.z - piecePos.z; i++)
                {
                    if (gameManager.points[piecePos.x, piecePos.z + i].piece != null)
                    {
                        if (gameManager.points[point.pointpos.x, point.pointpos.z].piece == null)
                        {
                            Debug.Log("前方有棋子挡住了");
                            return false;
                        }
                        for (int j = piecePos.z + i; j < (point.pointpos.z) - (piecePos.z + i); j++)
                        {
                            if (gameManager.points[piecePos.x, piecePos.z + i + j].piece != null)
                            {
                                Debug.Log("前方有棋子挡住了");
                                return false;
                            }
                        }
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
                        if (gameManager.points[point.pointpos.x, point.pointpos.z].piece == null)
                        {
                            Debug.Log("前方有棋子挡住了");
                            return false;
                        }
                        for (int j = point.pointpos.x + i; j < piecePos.x - i; j++)
                        {
                            if (gameManager.points[piecePos.x - i - j, piecePos.z].piece != null)
                            {
                                Debug.Log("左方有棋子挡住了");
                                return false;
                            }
                        }
                    }
                }
            }
            else if (temp > 0)
            {
                for (int i = 1; i < point.pointpos.x - piecePos.x; i++)
                {
                    if (gameManager.points[piecePos.x + i, piecePos.z].piece != null)
                    {
                        if (gameManager.points[point.pointpos.x, point.pointpos.z].piece == null)
                        {
                            Debug.Log("前方有棋子挡住了");
                            return false;
                        }
                        for (int j = piecePos.x + i; j < (point.pointpos.x) - (piecePos.x + i); j++)
                        {
                            if (gameManager.points[piecePos.x + i + j, piecePos.z].piece != null)
                            {
                                Debug.Log("右方有棋子挡住了");
                                return false;
                            }
                        }
                    }
                }
            }
        }
        return true;
    }
    public void Hide(Point point)
    {
        gameObject.SetActive(false);
    }

    public bool Move(Point point)
    {
        if (point.piece != null && point.piece.GetTurn() == red) return false;
        if (red)
        {
            if (Mathf.Abs(point.pointpos.z - piecePos.z) > 0 && Mathf.Abs(point.pointpos.x - piecePos.x) == 0 && CheckPath(point))
            {
                return true;
            }
            else if (Mathf.Abs(point.pointpos.x - piecePos.x) > 0 && Mathf.Abs(point.pointpos.z - piecePos.z) == 0 && CheckPath(point))
            {
                return true;
            }

            return false;
        }
        else
        {
            if (Mathf.Abs(point.pointpos.z - piecePos.z) > 0 && Mathf.Abs(point.pointpos.x - piecePos.x) == 0 && CheckPath(point))
            {
                return true;
            }
            else if (Mathf.Abs(point.pointpos.x - piecePos.x) > 0 && Mathf.Abs(point.pointpos.z - piecePos.z) == 0 && CheckPath(point))
            {
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
