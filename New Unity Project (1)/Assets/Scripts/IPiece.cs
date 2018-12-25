using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPiece 
{
    bool Move(Point point);
    bool CheckLevel(Point point);
    void SetPoisition(int x, int z);
    PiecePos GetPoisition();
}

