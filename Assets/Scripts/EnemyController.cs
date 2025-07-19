using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class EnemyController: MonoBehaviour
{
    
    List<Node> path = new List<Node>();
    [SerializeField][Range(0f, 5f)] float speed = 1f;

    Enemy enemy;
    GridManager gridManager;
    PathFinder pathfinder;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        enemy = GetComponent<Enemy>();
        gridManager = FindFirstObjectByType<GridManager>();
        pathfinder = FindFirstObjectByType<PathFinder>();
    }
    void OnEnable()
    {  
        ReturnToStart();
        RecalculatePath(true);
    
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FollowPath()
    {
        for (int i = 0; i < path.Count; i++)
        {
            //Debug.Log(waypoint.name);
            Vector3 startPosition = transform.position;
            Vector3 endPosition = gridManager.GetPositionFromCoordinates(path[i].coordinates);
            float travelPercent = 0f;
            
            transform.LookAt(endPosition);

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
                
            }
        }
        FinishPath();
    }

    public void RecalculatePath(bool resetPath)
    {
        Vector2Int coordinates = new Vector2Int();
        if (resetPath)
        {
            coordinates = pathfinder.StartCoordinates;
        }
        else
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        }
        
        StopAllCoroutines(); //specify name of coroutine if implemented more //StopCoroutine(nameof(FollowPath) //Stopcoroutine(FollowPath())
        
        path.Clear();

        path = pathfinder.GetNewPath(coordinates);
        StartCoroutine(FollowPath());
    }
    
    void ReturnToStart()
    {
        
        transform.position = gridManager.GetPositionFromCoordinates(pathfinder.StartCoordinates);
    }

    void FinishPath()
    {
        enemy.PenalizeGold();
        gameObject.SetActive(false);
    }
    
}