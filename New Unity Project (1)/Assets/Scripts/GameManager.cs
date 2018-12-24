using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public LayerMask pointMask ;
    public LayerMask RedpieceMask = 1 << 10;
    public LayerMask BlackpieceMask = 1 << 10;

    public int selectedID;
    public bool beRedTurn = true;

    public GameObject Path;

    public GameObject fab_selected;
    public GameObject Selected;

    public GameObject chessboard;

    public Point[,] points;
    public int width = 9;
    public int length = 10;

    public int[,] test;



    //public string dataPath = null;
    /// <summary>
    /// 保存每一步走棋
    /// </summary>
    public struct step
    {
        public int moveId;
        public int killId;

        public float xFrom;
        public float zFrom;
        public float xTo;
        public float zTo;

        public step(int moveId, int killId, float xFrom, float zFrom, float xTo, float zTo)
        {
            this.moveId = moveId;
            this.killId = killId;
            this.xFrom = xFrom;
            this.zFrom = zFrom;
            this.xTo = xTo;
            this.zTo = zTo;
        }
    }
    public List<step> steps = new List<step>();

    void Awake()
    {

    }
    void Start()
    {
        points = new Point[width, length];
        Point[] temp = chessboard.transform.GetComponentsInChildren<Point>();
        int index = 0;
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < length; z++)
            {
                points[x, z] = temp[index];
                temp[index].pointpos = new PointPos(x, z);
                index++;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        MainProcess();
    }
    /// <summary>
    /// 象棋的主要流程
    /// </summary>
    public void MainProcess()
    {
        //当鼠标点击时
        if (Input.GetMouseButtonDown(0))
        {
            //摄像机到点击位置的射线
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            //若点击的位置在棋盘内
            if (Physics.Raycast(ray, out hit, pointMask))
            {
                Debug.Log(hit.transform.gameObject.name);
                Point temp = hit.transform.gameObject.GetComponent<Point>();
                Debug.Log("x:"+temp.pointpos.x + "z:"+temp.pointpos.z);
                
            }
        }
    }


    /// <summary>
    /// 判断胜负
    /// </summary>
    void JudgeVictory()
    {
        if (PieceManager.p[4].dead == true)
        {
            //获胜
            //  WinPlane.SetActive(true);
            // PlayMusic_Win();
        }

        if (PieceManager.p[20].dead == true)
        {
            //失败
            //LosePlane.SetActive(true);
            //PlayMusic_Lose();
        }
    }


    /// <summary>
    /// 重新开始游戏
    /// </summary>
    public void Restart()
    {
        SceneManager.LoadScene("Main");
    }

    // 当鼠标选择棋子时，改变棋子的Sprite

   


    /// <summary>
    /// 设置上一步棋子走过的路径，即将上一步行动的棋子的位置留下标识，并标识该棋子
    /// </summary>
    void ShowPath(Vector3 oldPosition, Vector3 newPosition)
    {
        Selected.transform.position = newPosition;
        Selected.SetActive(true);

        Path.transform.position = oldPosition;
        Path.SetActive(true);
    }


    /// <summary>
    /// 隐藏路径
    /// </summary>
    void HidePath()
    {
        Selected.SetActive(false);
        Path.SetActive(false);
    }


    /// <summary>
    /// 播放移动错误的动画特效
    /// </summary>
    void MoveError(int moveId, Vector3 position)
    {
        GameObject Piece = GameObject.Find(moveId.ToString());
        Vector3 oldPosition = new Vector3(PieceManager.p[moveId].x, 0, PieceManager.p[moveId].z);
        Vector3[] paths = new Vector3[3];
        paths[0] = oldPosition;
        paths[1] = position;
        paths[2] = oldPosition;
        Piece.transform.DOPath(paths, 0.8f);
    }

   Vector3 Center(Point point)
    {
        float x = 0;
        float y = 0;
        float z = 0;
        x = point.pointpos.x;
        z = point.pointpos.z;
        y = 0.5f;
        return new Vector3(x, y, z);
        ////x,y,z为要返回的三维坐标
        ////将象棋分为9列（i）和10行（j）
        ////计算距离鼠标所指坐标点的最近的行列的序号（tpmi、tmpj）
        ////通过行列的序号算出位于该行该列的中心点的坐标位置并返回

        //float x = 0;
        //float y = 0;
        //float z = 0;
        //int i, tmpi = 1, j, tmpj = 1;
        //float min = 5f;

        //for (i = 0; i < 9; ++i)
        //{
        //    if (Math.Abs(point.x - ToolManager.colToX(i)) < min)
        //    {
        //        // Debug.Log(point.x);
        //        min = Math.Abs(point.x - ToolManager.colToX(i));
        //        tmpi = i;
        //    }
        //}
        //x = ToolManager.colToX(tmpi);

        //// Debug.Log("列:"+ tmpi);
        //min = 5f;
        //for (j = 0; j < 10; ++j)
        //{
        //    if (Math.Abs(point.z - ToolManager.rowToZ(j)) < min)
        //    {
        //        // Debug.Log(point.z);
        //        min = Math.Abs(point.z - ToolManager.rowToZ(j));
        //        tmpj = j;
        //    }
        //}
        //z = ToolManager.rowToZ(tmpj);
        ////Debug.Log("行:"+ tmpj);
        //return new Vector3(x, y, z);
    }

    // 获取点击的中心位置,若该位置上有棋子，则获取该棋子ID，否则id为-1

    void Click(Point point)
    {
        float x = Center(point).x;
        float z = Center(point).z;
        int id = ToolManager.GetPieceId(x, z);
        Debug.Log(id);
        Click(id, x, z);
    }
    // 若当前没有选中棋子，则尝试选中点击的棋子；若当前已有选中的棋子，则尝试移动棋子

    void Click(int id, float x, float z)
    {
        if (selectedID == -1)
        {
            TrySelectPiece(id);
        }
        else
        {
            TryMovePiece(id, x, z);
        }
    }

    // 尝试选择棋子；若id=-1或者不是处于移动回合的棋子，则返回；否则，将该棋子设为选中的棋子，并更新图片

    void TrySelectPiece(int id)
    {
        if (id == -1) return;

        if (!CanSelect(id)) return;

        selectedID = id;

        // ChangeSpriteToSelect(id);
    }


    // 尝试移动棋子
    // 若要移动的目标位置有棋子（kiillId）且和当前选中的棋子同色，则换选择
    // 若可以移动，则移动；若不能移动，则播放移动错误的提示动画

    void TryMovePiece(int killId, float x, float z)
    {
        if (killId != -1 && SameColor(killId, selectedID))
        {
            // ChangeSpriteToNormal(selectedID);
            TrySelectPiece(killId);
            return;
        }

        bool ret = CanMove(selectedID, killId, new Vector3(x, 0, z));

        if (ret)
        {
            MovePiece(selectedID, killId, new Vector3(x, 0, z));
            selectedID = -1;
        }
        else
        {
            MoveError(selectedID, new Vector3(x, 0, z));
        }
    }

    /// <summary>
    /// 走棋并吃棋
    /// </summary>
    /// <param name="hit"></param>
    void MovePiece(int moveId, int killId, Vector3 position)
    {
        // 1.若移动到的位置上有棋子，将其吃掉
        // 2.将移动棋子的路径显示出来
        // 3.将棋子移动到目标位置
        // 4.播放音效
        // 5.改变精灵的渲染图片
        // 6.判断是否符合胜利或者失败的条件

        SaveStep(moveId, killId, position.x, position.z);

        KillPiece(killId);

        ShowPath(new Vector3(PieceManager.p[moveId].x, PieceManager.p[moveId].z, 0), position);

        MovePiece(moveId, position);

        // PlayMusic_Move();

        //ChangeSpriteToNormal(moveId);

        JudgeVictory();
    }


    // 将移动的棋子ID、吃掉的棋子ID以及棋子从A点的坐标移动到B点的坐标都记录下来

    void SaveStep(int moveId, int killId, float bx, float bz)
    {
        step tmpStep = new step();

        float ax = PieceManager.p[moveId].x;
        float az = PieceManager.p[moveId].z;

        tmpStep.moveId = moveId;
        tmpStep.killId = killId;
        tmpStep.xFrom = ax;
        tmpStep.zFrom = az;
        tmpStep.xTo = bx;
        tmpStep.zTo = bz;

        steps.Add(tmpStep);
    }

    /// <summary>
    /// 通过记录的步骤结构体来返回上一步
    /// </summary>
    /// <param name="_step"></param>
    //void Back(step _step)
    //{
    //    ReliveJUss(_step.killId);
    //    MovePiece(_step.moveId, new Vector3(_step.xFrom, _step.zFrom, 0));
    //    HidePath();
    //    if (selectedID != -1)
    //    {
    //        ChangeSpriteToNormal(selectedID);
    //        selectedID = -1;
    //    }
    //}

    /// <summary>
    /// 悔棋，退回一步
    /// </summary>
    //public void BackOne()
    //{
    //    if (steps.Count == 0) return;

    //    step tmpStep = steps[steps.Count - 1];
    //    steps.RemoveAt(steps.Count - 1);
    //    Back(tmpStep);
    //}






    /// <summary>
    /// 判断走棋是否符合走棋的规则
    /// </summary>
    /// <param name="selectedId"></param>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <returns></returns>
    bool CanMove(int moveId, int killId, Vector3 clickPoint)
    {
        if (SameColor(moveId, killId)) return false;

     //   int col = ToolManager.xToCol(clickPoint.x);
      //  int row = ToolManager.zToRow(clickPoint.z);

        switch (PieceManager.p[moveId].type)
        {
            case PieceType.JIANG:
                return RuleManager.moveJiang(moveId, row, col, killId);
            case PieceType.SHI:
                return RuleManager.moveShi(moveId, row, col, killId);
            case PieceType.XIANG:
                return RuleManager.moveXiang(moveId, row, col, killId);
            case PieceType.JU:
                return RuleManager.moveJU(moveId, row, col, killId);
            case PieceType.MA:
                return RuleManager.moveMa(moveId, row, col, killId);
            case PieceType.PAO:
                return RuleManager.movePao(moveId, row, col, killId);
            case PieceType.BING:
                return RuleManager.moveBing(moveId, row, col, killId);
        }

        return true;
    }

    /// <summary>
    /// 判断点击的棋子是否可以被选中，即点击的棋子是否在它可以移动的回合
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    bool CanSelect(int id)
    {
        return beRedTurn == PieceManager.p[id].red;
    }


    bool IsRed(int id)
    {
        return PieceManager.p[id].red;
    }

    bool IsDead(int id)
    {
        if (id == -1) return true;
        return PieceManager.p[id].dead;
    }

    bool SameColor(int id1, int id2)
    {
        if (id1 == -1 || id2 == -1) return false;

        return IsRed(id1) == IsRed(id2);
    }

    /// <summary>
    /// 设置棋子死亡
    /// </summary>
    /// <param name="id"></param>
    void KillPiece(int id)
    {
        if (id == -1) return;

        PieceManager.p[id].dead = true;
        GameObject Piece = GameObject.Find(id.ToString());
        Piece.SetActive(false);
    }

    /// <summary>
    /// 复活棋子
    /// </summary>
    /// <param name="id"></param>
    void ReliveJUss(int id)
    {
        if (id == -1) return;

        //因GameObject.Find();函数不能找到active==false的物体，故先找到其父物体，再找到其子物体才可以找到active==false的物体
        PieceManager.p[id].dead = false;
        GameObject Background = GameObject.Find("Background");
        GameObject Piece = Background.transform.Find(id.ToString()).gameObject;
        Piece.SetActive(true);
    }

    /// <summary>
    /// 移动棋子到目标位置
    /// </summary>
    /// <param name="point"></param>
    void MovePiece(int moveId, Vector3 point)
    {
        GameObject Piece = GameObject.Find(moveId.ToString());
        Piece.transform.DOMove(point, 0.5f);
        // Piece.transform.position = Vector3.Lerp(transform.position, point, Time.deltaTime);
        PieceManager.p[moveId].x = point.x;
        PieceManager.p[moveId].z = point.z;

        beRedTurn = !beRedTurn;
    }

    /// <summary>
    /// 判断点击的位置是否在棋盘内
    /// </summary>
    /// <param name="hit"></param>
    /// <returns></returns>
    bool InsideJUssbord(RaycastHit hit)
    {
        if ((hit.point.x > -24 && hit.point.x < 24) && ((hit.point.z > -23.5 && hit.point.z < 23.5)))
            return true;
        else
            return false;
    }



}
public class Node
{

    public int _x;
    public int _y;

    public Node(int _x, int _y)
    {



        this._x = _x;
        this._y = _y;
    }
}
