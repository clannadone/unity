using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MA : MonoBehaviour, IPiece
{
    public PiecePos piecePos;
    public bool red;
    GameManager gameManager;
    Vector3 _vec;
    Animator anim;
    public PieceType GetPieceType()
    {
        return PieceType.MA;
    }
    public string PieceToString()
    {
        return "马";
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        _vec = transform.position;
        gameManager = FindObjectOfType<GameManager>();
    }
    public void Hide(Point point)
    {

        gameObject.SetActive(false);
    }

    public bool Move(Point point)
    {

        int temp_x = point.pointpos.x - piecePos.x;
        int temp_z = point.pointpos.z - piecePos.z;
        if (point.piece != null && point.piece.GetTurn() == red) return false;
        if (red)
        {
            if (Mathf.Abs(point.pointpos.x - piecePos.x) == 1 && Mathf.Abs(point.pointpos.z - piecePos.z) == 2)
            {
                if (temp_z > 0)
                {
                    //|_|2|       
                    //|_|_|
                    //|1|_|

                    //|2|_|       
                    //|_|_|
                    //|_|1|
                    if (gameManager.points[piecePos.x, piecePos.z + 1].piece != null)
                    {
                        Debug.Log("蹩马腿了");
                        return false;
                    }
                }
                else
                {
                    //|1|_|       
                    //|_|_|
                    //|_|2|

                    //|_|1|       
                    //|_|_|
                    //|2|_|
                    if (gameManager.points[piecePos.x, piecePos.z - 1].piece != null)
                    {
                        Debug.Log("蹩马腿了");
                        return false;
                    }
                }
                return true;
            }
            else if (Mathf.Abs(point.pointpos.x - piecePos.x) == 2 && Mathf.Abs(point.pointpos.z - piecePos.z) == 1)
            {
                if (temp_x > 0)
                {
                    //|_|_|2|
                    //|1|_|_|

                    //|1|_|_|
                    //|_|_|2|
                    if (gameManager.points[piecePos.x + 1, piecePos.z].piece != null)
                    {
                        Debug.Log("蹩马腿了");
                        return false;
                    }
                }
                else
                {
                    //|2|_|_|
                    //|_|_|1|

                    //|_|_|1|
                    //|2|_|_|
                    if (gameManager.points[piecePos.x - 1, piecePos.z].piece != null)
                    {
                        Debug.Log("蹩马腿了");
                        return false;
                    }
                }
                return true;
            }
           
        }
        else
        {
            if (Mathf.Abs(point.pointpos.x - piecePos.x) == 1 && Mathf.Abs(point.pointpos.z - piecePos.z) == 2)
            {
                if (temp_z > 0)
                {
                    //|_|2|       
                    //|_|_|
                    //|1|_|

                    //|2|_|       
                    //|_|_|
                    //|_|1|
                    if (gameManager.points[piecePos.x, piecePos.z + 1].piece != null)
                    {
                        Debug.Log("蹩马腿了");
                        return false;
                    }
                }
                else
                {
                    //|1|_|       
                    //|_|_|
                    //|_|2|

                    //|_|1|       
                    //|_|_|
                    //|2|_|
                    if (gameManager.points[piecePos.x, piecePos.z - 1].piece != null)
                    {
                        Debug.Log("蹩马腿了");
                        return false;
                    }
                }
                return true;
            }
            else if (Mathf.Abs(point.pointpos.x - piecePos.x) == 2 && Mathf.Abs(point.pointpos.z - piecePos.z) == 1)
            {
                if (temp_x > 0)
                {
                    //|_|_|2|
                    //|1|_|_|

                    //|1|_|_|
                    //|_|_|2|
                    if (gameManager.points[piecePos.x + 1, piecePos.z].piece != null)
                    {
                        Debug.Log("蹩马腿了");
                        return false;
                    }
                }
                else
                {
                    //|2|_|_|
                    //|_|_|1|

                    //|_|_|1|
                    //|2|_|_|
                    if (gameManager.points[piecePos.x - 1, piecePos.z].piece != null)
                    {
                        Debug.Log("蹩马腿了");
                        return false;
                    }
                }
                return true;
            }
        }
        return false;
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
