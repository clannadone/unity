using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MA : MonoBehaviour,IPiece  {
    public PiecePos piecePos;
    public bool red;
    public bool CheckLevel(Point point)
    {
        throw new System.NotImplementedException();
    }

    public bool Move(Point point)
    {
        return true;
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
        throw new System.NotImplementedException();
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
