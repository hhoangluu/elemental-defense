using System;
using UnityEngine;

public class GridVisualizer : MonoBehaviour
{
    [SerializeField] private GameObject cellPrefab;
    GameObject[,] cells;
    [SerializeField] private GameObject lineRendererPrefab;

    public void Init(GridCell[,] gridCells)
    {
        DrawGridLines(gridCells);
    }

    public void SetEnable(bool isEnable)
    {
        gameObject.SetActive(isEnable);
    }

    void DrawLine(Vector3 start, Vector3 end)
    {
        GameObject line = Instantiate(lineRendererPrefab, transform);
        LineRenderer lr = line.GetComponent<LineRenderer>();
        lr.positionCount = 2;
        lr.startWidth = 0.02f;
        lr.endWidth = 0.02f;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
    }

    public void DrawGridLines(GridCell[,] gridCells)
    {
        int rows = gridCells.GetLength(0);
        int cols = gridCells.GetLength(1);

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                Vector3 center = gridCells[row, col].worldPos;

                Vector3 topLeft = center + new Vector3(-1 / 2f, 1 / 2f, 0);
                Vector3 topRight = center + new Vector3(1 / 2f, 1 / 2f, 0);
                Vector3 bottomRight = center + new Vector3(1 / 2f, -1 / 2f, 0);
                Vector3 bottomLeft = center + new Vector3(-1 / 2f, -1 / 2f, 0);

                DrawLine(topLeft, topRight);       // top
                DrawLine(topRight, bottomRight);   // right
                DrawLine(bottomRight, bottomLeft); // bottom
                DrawLine(bottomLeft, topLeft);     // left
            }
        }
    }

}
