using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Type
{
    JIANG,
    SHI,
    XIANG,
    MA,
    JU,
    PAO,
    BING
}
public struct PiecePos
{
    public float x, y;
    public Type type;
    public PiecePos(float x, float y, Type type)
    {
        this.x = x;
        this.y = y;
        this.type = type;
    }
}

public class PieceManager : MonoBehaviour
{

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


    public static Piece[] p = new Piece[32];
    /// <summary>
    ///
    /// </summary>
    public struct Piece
    {
        public int id;

        public bool red;

        public bool dead;

        public float x;
        public float y;

        public Type type;
        public void init(int id)
        {
            this.id = id;
            red = id < 16;
            dead = false;

            PiecePos[] pos =
            {
            new PiecePos(4.3f,4.5f,Type.JU),
            new PiecePos(3.2f,4.5f,Type.MA),
            new PiecePos(2.1f,4.5f,Type.XIANG),
            new PiecePos(1.1f,4.5f,Type.SHI),
            new PiecePos(0f,4.5f,Type.JIANG),
            new PiecePos(-1.1f,4.5f,Type.SHI),
            new PiecePos(-2.1f,4.5f,Type.XIANG),
            new PiecePos(-3.2f,4.5f,Type.MA),
            new PiecePos(-4.3f,4.5f,Type.JU),

            new PiecePos(3.2f,2.5f,Type.PAO),
            new PiecePos(-3.2f,2.5f,Type.PAO),

            new PiecePos(4.3f,1.5f,Type.BING),
            new PiecePos(2.1f,1.5f,Type.BING),
            new PiecePos(0f,1.5f,Type.BING),
            new PiecePos(-2.1f,1.5f,Type.BING),
            new PiecePos(-4.3f,1.5f,Type.BING),
        };
            if (id < 16)
            {
                x = pos[id].x;
                y = pos[id].y;
                type = pos[id].type;
            }
            else
            {
                x = -pos[id - 16].x;
                y = -pos[id - 16].y;
                type = pos[id - 16].type;
            }
        }
    }

    public GameObject GetPrefebs(int id, Type type)
    {
        if (id < 16)
        {
            switch (type)
            {
                case Type.JU:
                    return ju_red;
                case Type.MA:
                    return ma_red;
                case Type.XIANG:
                    return xiang_red;
                case Type.SHI:
                    return shi_red;
                case Type.JIANG:
                    return jiang_red;
                case Type.PAO:
                    return pao_red;
                case Type.BING:
                    return bing_red;
            }
        }
        else
        {
            switch (type)
            {
                case Type.JU:
                    return ju_black;
                case Type.MA:
                    return ma_black;
                case Type.XIANG:
                    return xiang_black;
                case Type.SHI:
                    return shi_black;
                case Type.JIANG:
                    return jiang_black;
                case Type.PAO:
                    return pao_black;
                case Type.BING:
                    return bing_black;
            }
        }
        return bing_black;
    }

    public void PieceInit()
    {
        for (int i = 0; i < 32; i++)
        {
            p[i].init(i);
        }
        for (int i = 0; i < 32; i++)
        {
            GameObject fabs = GetPrefebs(i, p[i].type);

            GameObject Piece = Instantiate(fabs, transform.localPosition, Quaternion.identity, PieceParent) as GameObject;
            Piece.name = i.ToString();
            Piece.transform.position = new Vector3(p[i].x, 0.25f, p[i].y);
            Piece.AddComponent<BoxCollider>();

        }
    }
    void Awake()
    {
        PieceInit();
    }
}
