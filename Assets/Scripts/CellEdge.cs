using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CellEdge : MonoBehaviour
{

    public MazeCell cell, otherCell;

    public void Initialize(MazeCell cell, MazeCell otherCell, bool rightEdge, float offset)
    {
        this.cell = cell;
        this.otherCell = otherCell;
        transform.parent = cell.transform;

        if (rightEdge) //form wall between cells in one row
        {
            transform.localPosition = new Vector3(0.5f, 1.5f, 0f + offset);
            transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
        }
        else  //form wall between rows
        {
            transform.localPosition = new Vector3(-0.5f, 1.5f, 0f + offset);
            transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }

}
