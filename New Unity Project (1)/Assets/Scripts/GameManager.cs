using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public LayerMask mask = 1 << 8;

    public int selectedID;

    public GameObject fab_selected;
    public GameObject Selected;

    public GameObject chessBoard;
    public Point[,] points;
    public int width = 9;
    public int length = 10;

    public string dataPath = null;

    void Awake()
    {
        Selected = Instantiate(fab_selected, transform.localPosition, Quaternion.identity) as GameObject;
        Selected.transform.position = new Vector3(0, 0, 0);
        Selected.name = "Selected";
        Selected.SetActive(false);
    }
     void Start()
    {
        points = new Point[width, length];
        Point[] temp = chessBoard.transform.GetComponentsInChildren<Point>();
        int index = 0;
        string[] data = FileReader().Split(',');

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < length; z++)
            {
                temp[index].pointType = (PointType)int.Parse(data[index]);
                points[x, z] = temp[index];
                if (temp[index].pointType == PointType.Move)
                {
                    temp[index].ChangeMoveColor();
                }
                index++;
            }
        }

    }
    // Update is called once per frame
    void Update () {
        if (Input.GetKey(KeyCode.Space))
        {
            SaveData();
        }
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            Debug.Log(selectedID);

            if(Physics.Raycast(ray,out hit) && hit.point.x > -22 && hit.point.x < 22 && hit.point.z > -22 && hit.point.z < 22)
            {
                //如果当前没有选中的棋子
                if (selectedID == -1)
                {
                    //如果点击的位置上有棋子
                    //获取选中棋子ID
                    //生成一个selceted 当做被选中的棋子
                    if(hit.collider.gameObject.name!= "chessboard")
                    {
                        selectedID = int.Parse(hit.collider.gameObject.name);

                        Selected.transform.position = new Vector3(PieceManager.p[selectedID].x, 0, PieceManager.p[selectedID].y);
                        Selected.SetActive(true);
                    }
                }

                //当前手中有选中的棋子
                else
                {
                    //如果点击的位置上没有棋子
                    //将选中的棋子移动到当前位置
                    //将selcetID=-1
                    //隐藏Selceted
                    if(hit.collider.gameObject.name== "chessboard")
                    {
                        //Debug.Log("点击位置" + hit.point);
                        GameObject Piece = GameObject.Find(selectedID.ToString());
                        Piece.transform.position = Center(hit.point);


                        selectedID = -1;
                        Selected.SetActive(false);
                    }
                    //如果点击的位置上有棋子
                    //判断当前位置上的棋子是不是当前选中的棋子
                    //将点击位置上的棋子吃掉
                    //将选中的棋子移动到当前位置
                    //selectedID=-1
                    //隐藏Selceted
                    else
                    {
                        int destroyID = int.Parse(hit.collider.gameObject.name);
                        //Debug.Log("destoryId:" + destoryId);
                        //Debug.Log("selectedId:" + selectedId);
                        if (destroyID != selectedID)
                        {
                            PieceManager.p[destroyID].dead = true;
                            hit.collider.gameObject.SetActive(false);

                            GameObject Piece = GameObject.Find(selectedID.ToString());
                            Piece.transform.position = Center(hit.point);

                            selectedID = -1;
                            Selected.SetActive(false);
                        }
                    }

                }
                
            }
        }
	}
    Vector3 Center(Vector3 point)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Transform p=null;
        if(Physics.Raycast(ray,out hit, 500, mask))
        {          
                p = gameObject.transform;
            return new Vector3(p.transform.position.x, 0.5f, p.transform.position.z);
        }

        return Vector3.zero;
    }
    public void SaveData()
    {
        string data = "";
        for (int y = 0; y < width; y++)
        {
            for (int x = 0; x < length; x++)
            {
                data += ((int)points[x, y].pointType).ToString() + ",";
            }
        }
        FileWriter(data);
    }
    private string FileReader()
    {
        string temp = null;
        try
        {
            using (StreamReader sr = new StreamReader(dataPath, Encoding.UTF8))
            {
                temp = sr.ReadToEnd();
            }
            return temp;
        }
        catch (Exception)
        {
            return temp;
        }
    }

    private bool FileWriter(string data)
    {
        try
        {
            using (StreamWriter sw = new StreamWriter(dataPath, false, Encoding.UTF8))
            {
                sw.Write(data);
            }
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
