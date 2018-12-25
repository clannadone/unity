using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JU : MonoBehaviour,IPiece {
    public PiecePos piecePos;
    public bool CheckLevel(Point point)
    {
        throw new System.NotImplementedException();
    }

    public bool Move(Point point)
    {
        return true;
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetPoisition(int x, int z)
    {
        piecePos = new PiecePos(x, z);
    }

    public PiecePos GetPoisition()
    {
        return this.piecePos;
    }
}
