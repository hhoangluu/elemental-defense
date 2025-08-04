using _ElementalDefense;
using UnityEngine;

public class TowerPlacerController : MonoBehaviour
{
    public TowerGridSystem gridSystem;
    public GameObject towerPrefab;
    public void Init(int mapHeight, int mapWidth)
    {
        gridSystem = new TowerGridSystem(mapHeight, mapWidth, 1f, Vector3.zero);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && gridSystem != null)
        {
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
