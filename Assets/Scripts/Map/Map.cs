using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] MapNavigation _mapNavigation;
    public MapNavigation mapNavigation => _mapNavigation;
    // Todo: Tìm cách để lấy data từ scene
    [SerializeField] int _mapHeight;
    public int mapHeight => _mapHeight;
    [SerializeField] int _mapWidth;
    public int mapWidth => _mapWidth;
    
}
