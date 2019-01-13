using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XIANG : MonoBehaviour,IPiece  {
    public PiecePos piecePos;
    public bool red;
    Vector3 _vec;
    GameManager gameManager;
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        _vec = transform.position;
        gameManager = FindObjectOfType<GameManager>();
    }
    public string PieceToString()
    {
        return "相";
    }
    public PieceType GetPieceType()
    {
        return PieceType.XIANG;
    }
    public void Hide(Point point)
    {
        gameObject.SetActive(false);
    }
    public bool CheckPath(Point point)
    {
        int temp = point.pointpos.z - piecePos.z;
        if (temp > 0)
        {
            if (gameManager.points[piecePos.x + 1, piecePos.z + 1].piece != null)
            {
                Debug.Log("塞象眼了");
                return false;
            }
            else if (gameManager.points[piecePos.x - 1, piecePos.z + 1].piece != null)
            {
                Debug.Log("塞象眼了");
                return false;
            }
        }
        else if (temp < 0)
        {
            if (gameManager.points[piecePos.x - 1, piecePos.z - 1].piece != null)
            {
                Debug.Log("塞象眼了");
                return false;
            }
            else if (gameManager.points[piecePos.x + 1, piecePos.z - 1].piece != null)
            {
                Debug.Log("塞象眼了");
                return false;
            }
        }
        return true;
    }
    public bool Move(Point point)
    {
        if (point.piece != null && point.piece.GetTurn() == red) return false;
        if (red)
        {
            if (point.pointpos.z <= 4)
            {
                if (Mathf.Abs(point.pointpos.x - piecePos.x) == 2 && Mathf.Abs(point.pointpos.z - piecePos.z) == 2&&CheckPath(point))
                    return true;
            }
            return false;
        }
        else
        {
            if (point.pointpos.z >= 5)
            {
                if (Mathf.Abs(point.pointpos.x - piecePos.x) == 2 && Mathf.Abs(point.pointpos.z - piecePos.z) == 2 && CheckPath(point))
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
