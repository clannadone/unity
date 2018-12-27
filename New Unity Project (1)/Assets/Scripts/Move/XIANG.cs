﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XIANG : MonoBehaviour,IPiece  {
    public PiecePos piecePos;
    public bool red;
    public PieceType PieceType;
    public bool GetPieceType(PieceType pieceType)
    {
        return pieceType == PieceType.XIANG;
    }
    public bool CheckPath(Point point)
    {
        return false;
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
            if (point.pointpos.z <= 4)
            {
                if (Mathf.Abs(point.pointpos.x - piecePos.x) == 2 && Mathf.Abs(point.pointpos.z - piecePos.z) == 2)
                    return true;
            }
            return false;
        }
        else
        {
            if (point.pointpos.z >= 5)
            {
                if (Mathf.Abs(point.pointpos.x - piecePos.x) == 2 && Mathf.Abs(point.pointpos.z - piecePos.z) == 2)
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
