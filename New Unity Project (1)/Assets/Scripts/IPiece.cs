using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IPiece : MonoBehaviour
{
    public PieceType pieceType;
    public virtual void Move() { }
    public virtual void CheckLevel() { }

}
