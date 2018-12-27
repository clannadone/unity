using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PieceType
{
    JIANG,
    SHI,
    XIANG,
    MA,
    JU,
    PAO,
    BING
}
[System.Serializable]
public class PiecePos
{
    public int x, z;

    public PiecePos(int x, int z)
    {
        this.x = x;
        this.z = z;
    }
}

public class PieceManager : MonoBehaviour
{
    public GameManager gamemanager;
    public Transform PieceParent;
    //初始化14种棋子的预制体
    public GameObject ju_black;
    public GameObject ma_black;
    public GameObject xiang_black;
    public GameObject shi_black;
    public GameObject jiang_black;
    public GameObject pao_black;
    public GameObject bing_black;

    public GameObject ju_red;
    public GameObject ma_red;
    public GameObject xiang_red;
    public GameObject shi_red;
    public GameObject jiang_red;
    public GameObject pao_red;
    public GameObject bing_red;

    public PointPos pointPos;
    public PieceType pieceType;

    public bool red;
    Dictionary<Vector2, PieceType> RedPiece = new Dictionary<Vector2, PieceType>
    {
        {new Vector2(0,3),PieceType.BING },
        {new Vector2(2,3),PieceType.BING },
        {new Vector2(4,3),PieceType.BING },
        {new Vector2(6,3),PieceType.BING },
        {new Vector2(8,3),PieceType.BING },

        {new Vector2(1,2),PieceType.PAO },
        {new Vector2(7,2),PieceType.PAO },

        {new Vector2(0,0),PieceType.JU },
        {new Vector2(1,0),PieceType.MA },
        {new Vector2(2,0),PieceType.XIANG },
        {new Vector2(3,0),PieceType.SHI },
        {new Vector2(4,0),PieceType.JIANG },
        {new Vector2(5,0),PieceType.SHI },
        {new Vector2(6,0),PieceType.XIANG },
        {new Vector2(7,0),PieceType.MA },
        {new Vector2(8,0),PieceType.JU },

        };
    Dictionary<Vector2, PieceType> BlackPiece = new Dictionary<Vector2, PieceType>
    {
        {new Vector2(0,6),PieceType.BING },
        {new Vector2(2,6),PieceType.BING },
        {new Vector2(4,6),PieceType.BING },
        {new Vector2(6,6),PieceType.BING },
        {new Vector2(8,6),PieceType.BING },

        {new Vector2(1,7),PieceType.PAO },
        {new Vector2(7,7),PieceType.PAO },

        {new Vector2(0,9),PieceType.JU },
        {new Vector2(1,9),PieceType.MA },
        {new Vector2(2,9),PieceType.XIANG },
        {new Vector2(3,9),PieceType.SHI },
        {new Vector2(4,9),PieceType.JIANG },
        {new Vector2(5,9),PieceType.SHI },
        {new Vector2(6,9),PieceType.XIANG },
        {new Vector2(7,9),PieceType.MA },
        {new Vector2(8,9),PieceType.JU },

        };
    private void Start()
    {
        Init(true); Init(false);
    }
    public void Init(bool red)
    {
        Point[,] points = gamemanager.points;
        Dictionary<Vector2, PieceType> temp = null;
        if (red)
        {
            temp = RedPiece;
        }
        else
        {
            temp = BlackPiece;
        }
        foreach (var item in temp)
        {
            Point tep = points[(int)item.Key.x, (int)item.Key.y];
            GameObject obj = Instantiate(GetPrefebs(red, item.Value), tep.transform.position, Quaternion.identity,PieceParent);
            tep.piece = obj.GetComponent<IPiece>();
            tep.piece.SetTurn(red);
            tep.piece.SetPoisition((int)item.Key.x, (int)item.Key.y);
          //  Debug.Log("x" + (int)item.Key.x + "y" + (int)item.Key.y);
        }
    }
    public GameObject GetPrefebs(bool red, PieceType type)
    {
        if (red)
        {
            switch (type)
            {
                case PieceType.JU:
                    return ju_red;
                case PieceType.MA:
                    return ma_red;
                case PieceType.XIANG:
                    return xiang_red;
                case PieceType.SHI:
                    return shi_red;
                case PieceType.JIANG:
                    return jiang_red;
                case PieceType.PAO:
                    return pao_red;
                case PieceType.BING:
                    return bing_red;
            }
        }
        else
        {
            switch (type)
            {
                case PieceType.JU:
                    return ju_black;
                case PieceType.MA:
                    return ma_black;
                case PieceType.XIANG:
                    return xiang_black;
                case PieceType.SHI:
                    return shi_black;
                case PieceType.JIANG:
                    return jiang_black;
                case PieceType.PAO:
                    return pao_black;
                case PieceType.BING:
                    return bing_black;
            }
        }
        return null;
    }



}
