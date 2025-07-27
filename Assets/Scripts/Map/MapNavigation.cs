using System.Collections.Generic;
using UnityEngine;

public class MapNavigation : MonoBehaviour
{
    [SerializeField] List<Transform> _navPoints;
    public List<Transform> navPoints => _navPoints;
    void Awake()
    {
        
    }
    
    
}
