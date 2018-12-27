using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MA : MonoBehaviour, IPiece
{
    public PiecePos piecePos;
    public bool red;
    GameManager gameManager;
    public PieceType PieceType;
    public bool GetPieceType(PieceType pieceType)
    {
        return pieceType == PieceType.MA;
    }
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
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
            //|_|*|       
            //|_|_|
            //|*|_|正日  
 
            //|*|_|
            //|_|_|
            //|_|*|正日
            if (Mathf.Abs(point.pointpos.x - piecePos.x) == 1 && Mathf.Abs(point.pointpos.z - piecePos.z) == 2)
            {

                if (gameManager.points[piecePos.x, piecePos.z +1].piece!=null)
                {
                    Debug.Log("蹩马腿了");
                    return false;
                }
                else if(gameManager.points[piecePos.x, piecePos.z - 1].piece != null)
                {
                    Debug.Log("蹩马腿了");
                    return false;
                }
                return true;
            }
            //|_|_|*|
            //|*|_|_|

            //|*|_|_|
            //|_|_|*|
            else if (Mathf.Abs(point.pointpos.x - piecePos.x) == 2 && Mathf.Abs(point.pointpos.z - piecePos.z) == 1)
            {
                if (gameManager.points[piecePos.x+1, piecePos.z].piece != null)
                {
                    Debug.Log("蹩马腿了");
                    return false;
                }
                else if (gameManager.points[piecePos.x - 1, piecePos.z].piece != null)
                {
                    Debug.Log("蹩马腿了");
                    return false;
                }
                return true;

            }
            return false;
        }
        else
        {
            if (Mathf.Abs(point.pointpos.x - piecePos.x) == 1 && Mathf.Abs(point.pointpos.z - piecePos.z) == 2)
            {
                if (gameManager.points[piecePos.x, piecePos.z + 1].piece != null)
                {
                    Debug.Log("蹩马腿了");
                    return false;
                }
                else if (gameManager.points[piecePos.x, piecePos.z - 1].piece != null)
                {
                    Debug.Log("蹩马腿了");
                    return false;
                }
                return true;
            }
            else if (Mathf.Abs(point.pointpos.x - piecePos.x) == 2 && Mathf.Abs(point.pointpos.z - piecePos.z) == 1)
            {
                if (gameManager.points[piecePos.x + 1, piecePos.z].piece != null)
                {
                    Debug.Log("蹩马腿了");
                    return false;
                }
                else if (gameManager.points[piecePos.x - 1, piecePos.z].piece != null)
                {
                    Debug.Log("蹩马腿了");
                    return false;
                }
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
