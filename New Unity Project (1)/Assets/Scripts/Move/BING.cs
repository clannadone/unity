using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class BING : MonoBehaviour,IPiece {
    public PiecePos piecePos;
    public bool red;
    public bool CheckLevel(Point point)
    {
        throw new System.NotImplementedException();
    }
    public bool Move(Point point)
    {
        if (red)
        {
            if (point.pointpos.z <= 5)
            {
                if (point.pointpos.z - piecePos.z != 1)
                {
                    return false;
                }
                return true;
            }
            else
            {
                if (point.pointpos.z - piecePos.z != 1 || Mathf.Abs(point.pointpos.x - piecePos.x) != 1)
                {
                    return false;
                }
                return true;
            }
        }
        else
        {
            if (point.pointpos.z >= 4)
            {

                if (point.pointpos.z - piecePos.z != 1)
                {
                    return false;
                }
                return true;
            }
            else
            {
                if (point.pointpos.z - piecePos.z != 1 || Mathf.Abs(point.pointpos.x - piecePos.x) != 1)
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
