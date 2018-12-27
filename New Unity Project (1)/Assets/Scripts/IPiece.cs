using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPiece 
{
    bool Move(Point point);
    void Hide(Point point);
    void SetPoisition(int x, int z);
    PiecePos GetPoisition();
    void SetTransformPoisition(Vector3 vec);
    void SetTurn(bool red);
    bool GetTurn();
    bool GetPieceType(PieceType pieceType);
}

