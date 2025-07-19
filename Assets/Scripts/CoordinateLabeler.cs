using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler: MonoBehaviour
{
    [SerializeField] private Color defaultColor = Color.paleGreen;
    [SerializeField] private Color blockedColor = Color.brown;
    [SerializeField] private Color exploredColor = Color.darkOrange;
    [SerializeField] private Color pathColor = Color.yellowGreen;
    
    GridManager gridManager;

    public Color BlockedColor
    {
        get => blockedColor;
        set => blockedColor = value;
    }
        
    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    Waypoint waypoint;

    void Awake()
    {
        gridManager = FindFirstObjectByType<GridManager>();
        label = GetComponent<TextMeshPro>();
        label.enabled = false;

        DisplayCoordinates();
    }
    
    //TMP - convert, UI
    //TextMeshPro- generic use
    //TMPUGi

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
        }
        
            ToggleLabels();
            SetLabelColor();
        
    }

    void DisplayCoordinates()
    {
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);

        label.text = coordinates.x + "," + coordinates.y;
    }

    void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }

    void ToggleLabels()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            label.enabled = !label.IsActive();
        }
    }
    void SetLabelColor()
    {
        if (gridManager == null) return;
        
        Node node = gridManager.GetNode(coordinates);
        
        if (node == null) return;

        if (!node.isWalkable)
        {
            label.color = blockedColor;
        }
        else if (node.isPath)
            {
            label.color = pathColor;
            }
        else if (node.isExplored)
            {
            label.color = exploredColor;
            }
        
        
    }
}