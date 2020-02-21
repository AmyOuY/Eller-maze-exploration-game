using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{

    public static int mazeX = 8, mazeZ = 1;
    public MazeCell cellPrefab;
    private MazeCell[,] cells = new MazeCell[mazeX, 100];
    private int[,] cellIDs = new int[mazeX, 100];
    public static bool firstRow;
    public static bool buildNewRow;
    public static bool lastRow;

    public void Generate()
    {

        if (firstRow)
        {
            firstRow = false;
            //create one row of cells with floor 
            for (int x = 0; x < mazeX; x++)
            {
                for (int z = 0; z < mazeZ; z++)
                {
                    MazeCell newCell = CreateCell(x, z);
                    cells[x, z] = newCell;
                    cellIDs[x, z] = x;
                }
            }

            //randomly group adjacent cells
            for (int i = 0; i < mazeX - 1; i++)
            {
                int r = Random.Range(0, 2);
                if (r == 0) //form a group
                {
                    cellIDs[i + 1, 0] = cellIDs[i, 0];
                }
                else //create wall
                {
                    CreateWall(cells[i, 0], cells[i + 1, 0], true, 0f);
                }
            }

            //create row wall
            for (int i = 0; i < mazeX - 1; i++)
            {
                if (cellIDs[i, 0] == cellIDs[i + 1, 0])
                {
                    CreateRowWall(cells[i, mazeZ - 1], cells[i + 1, mazeZ - 1], false, -0.5f);
                }
            }
            mazeZ++;
        }


        if (buildNewRow)
        {
            buildNewRow = false;
            //create one row of cells with floor
            for (int x = 0; x < mazeX; x++)
            {
                MazeCell newCell = CreateCell(x, mazeZ - 1);
                cells[x, mazeZ - 1] = newCell;
                cellIDs[x, mazeZ - 1] = x + (mazeZ - 1) * 8;
            }
            //form pass to previous group
            for (int i = 0; i < mazeX - 1; i++)
            {
                if (cellIDs[i, mazeZ - 2] != cellIDs[i + 1, mazeZ - 2])
                {
                    cellIDs[i, mazeZ - 1] = cellIDs[i, mazeZ - 2];
                }
            }
            //for cells in this row, randomly group adjacent cells that are not from the same group
            for (int i = 0; i < mazeX - 1; i++)
            {
                if (cellIDs[i, mazeZ - 1] != cellIDs[i + 1, mazeZ - 1])
                {
                    int r = Random.Range(0, 2);
                    if (r == 0) //group cells
                    {
                        cellIDs[i + 1, mazeZ - 1] = cellIDs[i, mazeZ - 1];
                    }
                    else  //create wall
                    {
                        CreateWall(cells[i, mazeZ - 1], cells[i + 1, mazeZ - 1], true, 0f);
                    }
                }
                else
                {
                    CreateWall(cells[i, mazeZ - 1], cells[i + 1, mazeZ - 1], true, 0f);
                }
            }
            //create row wall
            for (int i = 0; i < mazeX - 1; i++)
            {
                if (cellIDs[i, mazeZ - 1] == cellIDs[i + 1, mazeZ - 1])
                {
                    CreateRowWall(cells[i, mazeZ - 1], cells[i + 1, mazeZ - 1], false, -0.5f + -0.005f * (mazeZ - 1));
                }
            }
            mazeZ++;
        }

        //form end wall to terminate maze
        if (lastRow)
        {
            lastRow = false;
            for (int x = 0; x < mazeX; x++)
            {
                CreateCell(x, mazeZ - 1);
            }
        }
    }



    private MazeCell CreateCell(int x, int z)
    {
        MazeCell newCell = Instantiate(cellPrefab) as MazeCell;
        cells[x, z] = newCell;
        newCell.name = "Maze Cell " + x + ", " + z;
        newCell.transform.parent = transform;
        newCell.transform.localPosition = new Vector3(x - mazeX * 0.5f, 0f, z - 0.5f - (mazeZ - 1) * 2f);
        return newCell;
    }



    
    public CellWall wallPrefab;
    public CellWall wallbtRowPrefab;
   //create wall between cells inside one row
    private void CreateWall(MazeCell cell, MazeCell otherCell, bool direction, float offset)
    {
        CellWall wall = Instantiate(wallPrefab) as CellWall;
        wall.Initialize(cell, otherCell, direction, offset);
    }

    //create wall between rows
    private void CreateRowWall(MazeCell cell, MazeCell otherCell, bool direction, float offset)
    {
        CellWall wall = Instantiate(wallbtRowPrefab) as CellWall;
        wall.Initialize(cell, otherCell, direction, offset);
    }

}