using System.Collections.Generic;
using System.Linq;
using _ElementalDefense;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerPlacerController : MonoBehaviour
{
    public TowerGridSystem gridSystem;
    public GameObject towerPrefab;
    public GridVisualizer gridVisualizer;
    public void Init(Map map)
    {
        gridSystem = new TowerGridSystem(map.mapHeight, map.mapWidth, 1f, Vector3.zero);
        List<Vector2> navPoints = map.mapNavigation.navPoints.Select(x => (Vector2)x.position).ToList();
        gridSystem.BakeBlockedCells(navPoints);
        gridVisualizer.Init(gridSystem.grid);
        gridVisualizer.SetEnable(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && gridSystem != null)
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null)
            {
                if (gridSystem.TryPlaceTower(hit.point, out var cell))
                {
                    Instantiate(towerPrefab, cell.worldPos, Quaternion.identity);
                }
            }
        }
    }
}
