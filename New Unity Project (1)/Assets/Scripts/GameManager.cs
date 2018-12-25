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
    public LayerMask pointMask;
    public LayerMask RedpieceMask = 1 << 10;
    public LayerMask BlackpieceMask = 1 << 10;

    public int selectedID;
    public bool beRedTurn = true;

    public GameObject chessboard;

    public Point[,] points;
    public int width = 9;
    public int length = 10;
    public Point _Selected;

    PieceManager pieceManager;

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
        pieceManager = GetComponent<PieceManager>();
        points = new Point[width, length];
        Point[] temp = chessboard.transform.GetComponentsInChildren<Point>();
        int index = 0;
        for (int z = 0; z < length; z++)
        {
            for (int x = 0; x < width; x++)
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
                //Debug.Log(hit.transform.gameObject.name);
                Point temp = hit.transform.gameObject.GetComponent<Point>();
                //temp = _Selected;
               // Debug.Log("x:" + _Selected.pointpos.x + "y:" + _Selected.pointpos.z);
                // Debug.Log("x:"+temp.pointpos.x + "z:"+temp.pointpos.z);
                Click(temp);
                //Debug.Log(temp.transform.name);
                //Debug.Log("方块：x:" + temp.pointpos.x +"y:"+temp.pointpos.z );
                //Debug.Log("棋子坐标x:" + temp.piece.GetPoisition().x + "y:" + temp.piece.GetPoisition().z);
            }
        }
    }
    public void Click(Point point)
    {
        if (point.piece != null)
        {
            TrySelectPiece(point);
        }
        else 
        {
            TryMovePiece(point);
        }

    }
    public void TrySelectPiece(Point point)
    {
        if (point.piece == null) return;

        //if (!Selceted(point)) return;          
        _Selected = point;
        
    }
    public void TryMovePiece(Point point)
    {
        point = _Selected;

        //if (/*Selceted(point) && */point.piece != null)
        //{
        //    TrySelectPiece(point);
        //    return;
        //}
        Debug.Log("1");
        MovePiece(point);

    }
    void MovePiece(Point point)
    {
        Debug.Log("2");
        point.piece.Move(point);    
    }
    //判断是否符合走棋规则
    //bool CanMove(Point point, PiecePos pos)
    //{

    //}
    bool IsRed()
    {
        return pieceManager.red;
    }
    bool Selceted(bool red)
    {
        return beRedTurn = red/*pieceManager.Init(true)*/;
    }

    #region 弃用
    /// <summary>
    /// 判断胜负
    /// </summary>
    void JudgeVictory()
    {
        // if (PieceManager.p[4].dead == true)
        // {
        //获胜
        //  WinPlane.SetActive(true);
        // PlayMusic_Win();
        //}

        //if (PieceManager.p[20].dead == true)
        // {
        //失败
        //LosePlane.SetActive(true);
        //PlayMusic_Lose();
        // }
    }


    /// <summary>
    /// 重新开始游戏
    /// </summary>
    public void Restart()
    {
        SceneManager.LoadScene("Main");
    }

    ///// <summary>
    ///// 设置上一步棋子走过的路径，即将上一步行动的棋子的位置留下标识，并标识该棋子
    ///// </summary>
    //void ShowPath(Vector3 oldPosition, Vector3 newPosition)
    //{
    //    Selected.transform.position = newPosition;
    //    Selected.SetActive(true);

    //    Path.transform.position = oldPosition;
    //    Path.SetActive(true);
    //}


    /// <summary>
    /// 隐藏路径
    /// </summary>
    //void HidePath()
    //{
    //    Selected.SetActive(false);
    //    Path.SetActive(false);
    //}


    /// <summary>
    /// 播放移动错误的动画特效
    /// </summary>
    void MoveError(int moveId, Vector3 position)
    {

    }

    //Vector3 Center(Point point)
    // {
    //     float x = 0;
    //     float y = 0;
    //     float z = 0;
    //     x = point.pointpos.x;
    //     z = point.pointpos.z;
    //     y = 0.5f;
    //     return new Vector3(x, y, z);

    // }


    //尝试选择棋子
    void TryChoosePiece(Point point)
    {
        //if (point.piece)
        //{
        //    return;
        //}
        //   if(!IsRed)
    }


    // 尝试移动棋子
    // 若要移动的目标位置有棋子（kiillId）且和当前选中的棋子同色，则换选择
    // 若可以移动，则移动；若不能移动，则播放移动错误的提示动画

    void TryMovePiece()
    {
        //  if()
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
        // 6.判断是否符合胜利或者失败的条件

    }


    // 将移动的棋子ID、吃掉的棋子ID以及棋子从A点的坐标移动到B点的坐标都记录下来

    //void SaveStep(int moveId, int killId, float bx, float bz)
    //{
    //    step tmpStep = new step();

    //  //  float ax = PieceManager.p[moveId].x;
    //   // float az = PieceManager.p[moveId].z;

    //    tmpStep.moveId = moveId;
    //    tmpStep.killId = killId;
    //    tmpStep.xFrom = ax;
    //    tmpStep.zFrom = az;
    //    tmpStep.xTo = bx;
    //    tmpStep.zTo = bz;

    //    steps.Add(tmpStep);
    //}

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
    //bool CanMove(int moveId, int killId, Vector3 clickPoint)
    //{
    //    if (SameColor(moveId, killId)) return false;

    // //   int col = ToolManager.xToCol(clickPoint.x);
    //  //  int row = ToolManager.zToRow(clickPoint.z);

    //    switch (PieceManager.p[moveId].type)
    //    {
    //        case PieceType.JIANG:
    //            return RuleManager.moveJiang(moveId, row, col, killId);
    //        case PieceType.SHI:
    //            return RuleManager.moveShi(moveId, row, col, killId);
    //        case PieceType.XIANG:
    //            return RuleManager.moveXiang(moveId, row, col, killId);
    //        case PieceType.JU:
    //            return RuleManager.moveJU(moveId, row, col, killId);
    //        case PieceType.MA:
    //            return RuleManager.moveMa(moveId, row, col, killId);
    //        case PieceType.PAO:
    //            return RuleManager.movePao(moveId, row, col, killId);
    //        case PieceType.BING:
    //            return RuleManager.moveBing(moveId, row, col, killId);
    //    }

    //    return true;
    //}

    /// <summary>
    /// 判断点击的棋子是否可以被选中，即点击的棋子是否在它可以移动的回合
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    bool CanSelect(int id)
    {
        //return beRedTurn == PieceManager.p[id].red;
        return true;

    }


    bool IsRed(int id)
    {
        // return PieceManager.p[id].red;        
        return true;

    }

    bool IsDead(int id)
    {
        if (id == -1) return true;
        // return PieceManager.p[id].dead;
        return true;

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

        // PieceManager.p[id].dead = true;
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
        //  PieceManager.p[id].dead = false;
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
        //PieceManager.p[moveId].x = point.x;
        //PieceManager.p[moveId].z = point.z;

        beRedTurn = !beRedTurn;
    }
    #endregion
}

