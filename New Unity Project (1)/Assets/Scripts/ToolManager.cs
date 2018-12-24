using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ToolManager : MonoBehaviour
{

  
    //// 工具类：将坐标x的值转换为所在的列数
  
    //public static int xToCol(float x)
    //{
    //    int col = (int)(x / 5);
    //    col = col + 4;
    //    return col;
    //}

   
    //// 工具类：将坐标y的值转换为所在的行数
   
    //public static int zToRow(float z)
    //{
    //    int row;
    //    if (z > 0)
    //    {
    //        row = (int)(z / 5);
    //        row = 4 - row;
    //    }
    //    else
    //    {
    //        row = (int)(z / 5);
    //        row = 5 - row;
    //    }
    //    return row;
    //}

   
    //// 工具类：将所在的列数转换为对应的x坐标
    
    //public static float colToX(int col)
    //{
    //    float x;
    //    col = col - 4;
    //    x = col * 5;
    //    return x;
    //}

    ///// <summary>
    ///// 工具类：将所在的行数转换为对应的y坐标(因浮点数计算存在不精确问题，故先乘100，计算后再除100)
    ///// </summary>
    ///// <param name="row"></param>
    ///// <returns></returns>
    //public static float rowToZ(int row)
    //{
    //    float z;
    //    if (row < 5)
    //    {
    //        row = 4 - row;
    //        z = (float)(row * 5 + 2);
    //        //y = y / 100;
    //    }
    //    else
    //    {
    //        row = 5 - row;
    //        z = (float)(row * 5 - 3);
    //       // y = y / 100;
    //    }
    //    return z;
    //}


    
    // 计算选中的棋子的位置和要移动的位置之间的位置关系
    
    public static int Relation(int row1, int col1, int row, int col)
    {
        return Mathf.Abs(row1 - row) * 10 + Mathf.Abs(col1 - col);
    }

    
    // 工具类：通过行列数来判断该位置上是否有棋子，若有则返回棋子的ID，若没有则返回-1
   
    public static int GetPieceId(Point point)
    {
        float x = point.pointpos.x;
        float z = point.pointpos.z;

        for (int i = 0; i < 32; ++i)
        {
            if (x == PieceManager.p[i].x && z == PieceManager.p[i].z && PieceManager.p[i].dead == false)
                return i;
        }
        return -1;
    }

   
    // 工具类：判断该位置上是否有棋子，若有则返回棋子的ID，若没有则返回-1
   
    public static int GetPieceId(float x, float z)
    {
        for (int i = 0; i < 32; ++i)
        {
              //  Debug.Log("x"+x+"z:"+ z);
            if (x == PieceManager.p[i].x && z == PieceManager.p[i].z && PieceManager.p[i].dead == false)
                return i;
        }
        return -1;
    }

    
   /// 工具类：判断要移动的棋子是否在棋盘的下方
   
    public static bool IsBottomSide(int selectedId)
    {
        if (PieceManager.p[selectedId].initZ < 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

   
    // 工具类：判断两个位置所连成的一条直线上有多少个棋子
  
    public static int GetStoneCountAtLine(int row1, int col1, int row2, int col2)
    {
        int ret = 0;
        if (row1 != row2 && col1 != col2) return -1;
        if (row1 == row2 && col1 == col2) return -1;

        if (row1 == row2)
        {
            int min = col1 < col2 ? col1 : col2;
            int max = col1 < col2 ? col2 : col1;
            for (int col = min + 1; col < max; ++col)
            {
                if (GetPieceId(row1, col) != -1) ++ret;
            }
        }
        else
        {
            int min = row1 < row2 ? row1 : row2;
            int max = row1 < row2 ? row2 : row1;
            for (int row = min + 1; row < max; ++row)
            {
                if (GetPieceId(row, col1) != -1) ++ret;
            }
        }
        return ret;
    }

}
