using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SHI : MonoBehaviour, IPiece
{
    public PiecePos piecePos;
    public bool red;
    Vector3 _vec;
    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
        _vec = transform.position;
    }
    public PieceType GetPieceType()
    {
        return PieceType.SHI;
    }
    public string PieceToString()
    {
        return "士";
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
            if (point.pointpos.z <= 2 && point.pointpos.x >= 3 && point.pointpos.x <= 5)
            {
                if (Mathf.Abs(point.pointpos.x - piecePos.x) == 1 && Mathf.Abs(point.pointpos.z - piecePos.z) == 1)
                    return true;
            }
            return false;
        }
        else
        {
            if (point.pointpos.z >= 7 && point.pointpos.x >= 3 && point.pointpos.x <= 5)
            {
                if (Mathf.Abs(point.pointpos.x - piecePos.x) == 1 && Mathf.Abs(point.pointpos.z - piecePos.z) == 1)
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
