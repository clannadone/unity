using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PAO : MonoBehaviour, IPiece
{
    public PiecePos piecePos;
    public bool red;
    GameManager gameManager;
    Vector3 _vec;
    Animator anim;
    
    public string PieceToString()
    {
        return "炮";
    }
    public PieceType GetPieceType()
    {
        return PieceType.PAO;
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("Attack", false);
        _vec = transform.position;
        gameManager = FindObjectOfType<GameManager>();
    }
    public bool CheckPath(Point point, bool inside_x = true)
    {
        if (point.piece == null)
        {
            if (inside_x)
            {
                int temp = point.pointpos.z - piecePos.z;
                if (temp < 0)
                {
                    for (int i = 1; i < piecePos.z - point.pointpos.z; i++)
                    {
                        if (gameManager.points[point.pointpos.x, piecePos.z - i].piece != null)
                        {
                            return false;
                        }
                    }
                    return true;
                }
                else if (temp > 0)
                {
                    for (int i = 1; i < point.pointpos.z - piecePos.z; i++)
                    {
                        if (gameManager.points[piecePos.x, piecePos.z + i].piece != null)
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
            else if (!inside_x)
            {
                int temp = point.pointpos.x - piecePos.x;
                if (temp < 0)
                {
                    for (int i = 1; i < piecePos.x - point.pointpos.x; i++)
                    {
                        if (gameManager.points[piecePos.x-i, piecePos.z].piece != null)
                        {
                            return false;
                        }
                    }
                    return true;
                }
                else if (temp > 0)
                {
                    for (int i = 1; i < point.pointpos.x - piecePos.x; i++)
                    {
                        if (gameManager.points[piecePos.x+i, piecePos.z].piece != null)
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }

        }
        else
        {
            anim.SetBool("Attack", true);
           
            if (inside_x)
            {
                int temp = point.pointpos.z - piecePos.z;
                Point tep = null;
                if (temp < 0)
                {
                    for (int i = 1; i < piecePos.z - point.pointpos.z; i++)
                    {
                        if (gameManager.points[point.pointpos.x, piecePos.z - i].piece != null)
                        {
                            tep = gameManager.points[point.pointpos.x, piecePos.z - i];
                            break;
                        }
                    }
                }
                else if (temp > 0)
                {
                    for (int i = 1; i < point.pointpos.z - piecePos.z; i++)
                    {
                        if (gameManager.points[piecePos.x, piecePos.z + i].piece != null)
                        {
                            tep = gameManager.points[piecePos.x, piecePos.z + i];
                            break;
                        }
                    }
                }
                if (tep == null) return false;
                int temp2 = point.pointpos.z - tep.pointpos.z;
                if (temp2 < 0)
                {
                    for (int i = 1; i < tep.pointpos.z - point.pointpos.z; i++)
                    {
                        if (gameManager.points[point.pointpos.x, tep.pointpos.z - i].piece != null)
                        {
                            return false;
                        }
                    }
                    return true;
                }
                else if (temp2 > 0)
                {
                    for (int i = 1; i < point.pointpos.z - tep.pointpos.z; i++)
                    {
                        if (gameManager.points[point.pointpos.x, tep.pointpos.z + i].piece != null)
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
            else if (!inside_x)
            {
                int temp = point.pointpos.x - piecePos.x;
                Point tep = null;
                if (temp < 0)
                {
                    for (int i = 1; i < piecePos.x - point.pointpos.x; i++)
                    {
                        if (gameManager.points[piecePos.x - i, piecePos.z].piece != null)
                        {
                            tep = gameManager.points[piecePos.x - i, piecePos.z];
                            break;
                        }
                    }
                }
                else if (temp > 0)
                {
                    for (int i = 1; i < point.pointpos.x - piecePos.x; i++)
                    {
                        if (gameManager.points[piecePos.x + i, piecePos.z].piece != null)
                        {
                            tep = gameManager.points[piecePos.x + i, piecePos.z];
                            break;
                        }
                    }
                }
                if (tep == null) return false;
                int temp2 = point.pointpos.x - tep.pointpos.x;
                if (temp2 < 0)
                {
                    for (int i = 1; i < tep.pointpos.x - point.pointpos.x; i++)
                    {
                        if (gameManager.points[tep.pointpos.x - i, tep.pointpos.z].piece != null)
                        {
                            return false;
                        }
                    }
                    return true;
                }
                else if (temp2 > 0)
                {
                    for (int i = 1; i < point.pointpos.x - tep.pointpos.x; i++)
                    {
                        if (gameManager.points[tep.pointpos.x + i, tep.pointpos.z].piece != null)
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
        }

        return true;
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
            if (Mathf.Abs(point.pointpos.z - piecePos.z) > 0 && Mathf.Abs(point.pointpos.x - piecePos.x) == 0 && CheckPath(point))
            {
                return true;
            }
            else if (Mathf.Abs(point.pointpos.x - piecePos.x) > 0 && Mathf.Abs(point.pointpos.z - piecePos.z) == 0 && CheckPath(point, false))
            {
                return true;
            }

            return false;
        }
        else
        {
            if (Mathf.Abs(point.pointpos.z - piecePos.z) > 0 && Mathf.Abs(point.pointpos.x - piecePos.x) == 0 && CheckPath(point))
            {
                return true;
            }
            else if (Mathf.Abs(point.pointpos.x - piecePos.x) > 0 && Mathf.Abs(point.pointpos.z - piecePos.z) == 0 && CheckPath(point, false))
            {
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
     
        if (Vector3.Distance(transform.position, _vec) > 0.01f)
        {         
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
