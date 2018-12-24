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
public struct PiecePos
{
    public float x, z;
    public PieceType type;
    public PiecePos(float x, float z, PieceType type)
    {
        this.x = x;
        this.z = z;
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
        public float z;
        public int initZ;
        public PieceType type;
        public void init(int id)
        {
            this.id = id;
            red = id < 16;
            dead = false;

            PiecePos[] pos =
            {
            new PiecePos(20f,23f,PieceType.JU),
            new PiecePos(15f,23f,PieceType.MA),
            new PiecePos(10f,23f,PieceType.XIANG),
            new PiecePos(5f,23f,PieceType.SHI),
            new PiecePos(0f,23f,PieceType.JIANG),
            new PiecePos(-5.1f,22f,PieceType.SHI),
            new PiecePos(-10.1f,22f,PieceType.XIANG),
            new PiecePos(-15.2f,22f,PieceType.MA),
            new PiecePos(-20.3f,22f,PieceType.JU),

            new PiecePos(15f,12f,PieceType.PAO),
            new PiecePos(-15f,12f,PieceType.PAO),

            new PiecePos(20f,8f,PieceType.BING),
            new PiecePos(10f,8f,PieceType.BING),
            new PiecePos(0f,8f,PieceType.BING),
            new PiecePos(-10f,8f,PieceType.BING),
            new PiecePos(-20f,8f,PieceType.BING),
        };
            if (id < 16)
            {
                x = pos[id].x;
                z = pos[id].z;
                type = pos[id].type;
            }
            else
            {
                x = -pos[id - 16].x;
                z = -pos[id - 16].z;
                type = pos[id - 16].type;
            }
        }
    }

    public GameObject GetPrefebs(int id, PieceType type)
    {
        if (id < 16)
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
            Piece.transform.position = new Vector3(p[i].x, 0.25f, p[i].z);
            Piece.AddComponent<BoxCollider>();

        }
    }
    void Awake()
    {
        PieceInit();
    }
}
