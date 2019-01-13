using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JU : MonoBehaviour, IPiece
{
    GameManager gameManager;
    public PiecePos piecePos;
    public bool red;
    Vector3 _vec;
    Animator anim;
    public PieceType GetPieceType()
    {
        return PieceType.JU;
    }
    public string PieceToString()
    {
        return "车";
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        _vec = transform.position;
        gameManager = FindObjectOfType<GameManager>();
    }
    public bool CheckPath(Point point)
    {

        if (point.pointpos.x == piecePos.x)
        {
            int temp = point.pointpos.z - piecePos.z;
            if (temp < 0)
            {
                for (int i = 1; i < piecePos.z- point.pointpos.z; i++)
                {
                    if (gameManager.points[point.pointpos.x, piecePos.z-i].piece != null)
                    {
                        Debug.Log("前方有棋子挡住了");
                        return false;
                    }
                }
            }
            else if (temp > 0)
            {
                for (int i = 1; i < point.pointpos.z-piecePos.z; i++)
                {
                    if (gameManager.points[piecePos.x, piecePos.z + i].piece != null)
                    {
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
                for (int i = 1; i <piecePos.x-point.pointpos.x ; i++)
                {
                    if (gameManager.points[piecePos.x - i, piecePos.z].piece != null)
                    {
                        Debug.Log("左方有棋子挡住了");
                        return false;
                    }
                }
            }
            else if (temp > 0)
            {
                Debug.Log("piecePos.x"+piecePos.x);
                for (int i = 1; i < point.pointpos.x-piecePos.x ; i++)
                {
                    if (gameManager.points[piecePos.x+i, piecePos.z].piece != null)
                    {
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
    private void Update()
    {
        anim.SetFloat("Run", 0);
        if (Vector3.Distance(transform.position, _vec) > 0.01f)
        {
            anim.SetFloat("Run", 1);
            transform.position = Vector3.Lerp(transform.position, _vec, 2f * Time.deltaTime);
        }
    }
    public void SetTransformPoisition(Vector3 vec)
    {
        _vec = vec;
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
