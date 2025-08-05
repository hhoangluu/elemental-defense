using System.Collections.Generic;
using UnityEngine;

public class GridCell
{
    public Vector2Int gridPos;
    public Vector3 worldPos;
    public bool isOccupied;
    public bool isBlocked;
}

public class TowerGridSystem
{
    private int width;
    private int height;
    private float cellSize;
    private GridCell[,] grid;

    public TowerGridSystem(int width, int height, float cellSize, Vector3 origin)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        grid = new GridCell[width, height];

        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                Vector3 worldPos = origin + new Vector3((x - width / 2) * cellSize + cellSize / 2, (y - height / 2) * cellSize + cellSize / 2, 0);
                grid[x, y] = new GridCell
                {
                    gridPos = new Vector2Int(x, y),
                    worldPos = worldPos,
                    isOccupied = false,
                    isBlocked = false
                };
            }
    }

    public bool TryPlaceTower(Vector3 worldPos, out GridCell placedCell)
    {
        var coord = WorldToGrid(worldPos);
        placedCell = null;

        if (!IsInBounds(coord)) return false;
        var cell = grid[coord.x + width / 2, coord.y + height / 2];
        if (cell.isOccupied) return false;
        if (cell.isBlocked) return false;
        cell.isOccupied = true;
        placedCell = cell;
        return true;
    }

    public Vector2Int WorldToGrid(Vector3 worldPos)
    {
        return new Vector2Int(
            Mathf.FloorToInt(worldPos.x / cellSize),
            Mathf.FloorToInt(worldPos.y / cellSize));
    }

    public Vector3 GridToWorld(Vector2Int gridPos)
    {
        return grid[gridPos.x, gridPos.y].worldPos;
    }

    public bool IsInBounds(Vector2Int gridPos)
    {
        return gridPos.x >= -width / 2 && gridPos.y >= -height / 2 && gridPos.x < width / 2 && gridPos.y < height / 2;
    }

    public void BakeBlockedCells(List<Vector2> waypoints)
    {
        for (int i = 0; i < waypoints.Count - 1; i++)
        {
            Vector2 start = waypoints[i];
            Vector2 end = waypoints[i + 1];
            AddLineToBlockedCells(start, end);
        }
    }

    void AddLineToBlockedCells(Vector2 start, Vector2 end)
    {
        float distance = Vector2.Distance(start, end);
        int steps = Mathf.CeilToInt(distance); // lấy mẫu đường đi
        for (int i = 0; i <= steps; i++)
        {
            Vector2 point = Vector2.Lerp(start, end, i / (float)steps);
            var coord = WorldToGrid(point);
            var cell = grid[coord.x + width / 2, coord.y + height / 2];
            cell.isBlocked = true;
        }
    }

}
