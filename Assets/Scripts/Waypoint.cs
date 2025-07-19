using UnityEngine;

public class Waypoint: MonoBehaviour
{
    [SerializeField] bool isPlaceable;
    [SerializeField] Tower towerPrefab;
    
    GridManager gridManager;
    Vector2Int coordinates = new Vector2Int();
    PathFinder pathfinder;

    public bool IsPlaceable
    {
        get { return isPlaceable; }
    }

    void Awake()
    {
        gridManager = FindFirstObjectByType<GridManager>();
        pathfinder = FindFirstObjectByType<PathFinder>();
    }

    void Start()
    {
        if (gridManager != null)
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);

            if (!isPlaceable)
            {
                gridManager.BlockNode(coordinates);
            }
        }
    }
    void OnMouseDown()
    {
        if (gridManager.GetNode(coordinates).isWalkable && !pathfinder.WillBlockPath(coordinates))
        {
            //Debug.Log("Placing:" + transform.name);
            
            bool isSuccessful = towerPrefab.CreateTower(towerPrefab, transform.position);
            if (isSuccessful)
            {
                gridManager.BlockNode(coordinates);
                pathfinder.NotifyReceivers();
            }


        }
    }
}
