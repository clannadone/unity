using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class BING : MonoBehaviour,IPiece {
    public PiecePos piecePos;
    public bool red;
    public PieceType GetPieceType()
    {
        return PieceType.BING;
    }
    public string PieceToString()
    {
        return "兵";
    }
    public void  Hide(Point point)
    {

        gameObject.SetActive(false);

    }
    public bool Move(Point point)
    {
        if (point.piece != null && point.piece.GetTurn() == red) return false;
        if (red)
        {
            if (point.pointpos.z <= 4.5f)
            {
                if (point.pointpos.z - piecePos.z != 1)
                {
                    return false;
                }
                return true;
            }
            else
            {
                if (point.pointpos.z - piecePos.z != 1 && Mathf.Abs(point.pointpos.x - piecePos.x) != 1)
                {
                    return false;
                }             
                return true;
            }
        }
        else
        {
            if (point.pointpos.z >= 4.5f)
            {

                if (piecePos.z- point.pointpos.z  != 1)
                {
                    return false;
                }
                return true;
            }
            else
            {
                if ( piecePos.z-point.pointpos.z != 1 && Mathf.Abs(point.pointpos.x - piecePos.x) != 1)
                {
                    return false;
                }
                return true;
            }
        }
       

    }
    public void SetPoisition(int x, int z)
    {
        piecePos = new PiecePos(x, z);
    }

    public PiecePos GetPoisition()
    {
        return piecePos;
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
