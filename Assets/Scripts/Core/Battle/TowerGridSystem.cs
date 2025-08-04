using UnityEngine;

public class GridCell
{
    public Vector2Int gridPos;
    public Vector3 worldPos;
    public bool isOccupied;
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
                    isOccupied = false
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
}
