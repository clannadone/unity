using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class JIANG : MonoBehaviour,IPiece {
    public PiecePos piecePos;
    public bool red;
    public float test=0.7f;
    public int index;
    Vector3 _vec;
    Animator anim;
    float speed;
    GameManager gameManager;
    void Start()
    {
        anim = GetComponent<Animator>();
       
        _vec = transform.position;
        gameManager = FindObjectOfType<GameManager>();
    }
    public string PieceToString()
    {
        return "将";
    }
   
    public PieceType GetPieceType()
    {
        return PieceType.JIANG;
    }
    public void Hide(Point point)
    {
        gameObject.SetActive(false);
    }

    public bool Move(Point point)
    {
        if (red)
        {
            if (point.pointpos.z <= 2 && point.pointpos.x >= 3 && point.pointpos.x <= 5)
            {
                if(Mathf.Abs(point.pointpos.z-piecePos.z) !=1|| Mathf.Abs(point.pointpos.x-piecePos.x) != 1)
                return true;
            }
            return false;
        }
        else
        {
            if (point.pointpos.z >=7 && point.pointpos.x >= 3 && point.pointpos.x <= 5)
            {
                if (Mathf.Abs(point.pointpos.z - piecePos.z) != 1 || Mathf.Abs(point.pointpos.x - piecePos.x) != 1)
                    return true;
            }
            return false;
        }      
    }

    public void SetPoisition(int x, int z)
    {
        piecePos = new PiecePos(x,z);
    }

    public PiecePos GetPoisition()
    {
        return this.piecePos;
    }
    private void Update()
    {
        anim.SetFloat("Run", 0);
        if (Vector3.Distance(transform.position, _vec) > test)
        {
            //transform.DOBlendableLocalMoveBy(_vec, 10);
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
