using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public Room room;
    [System.Serializable]
    public struct Grid
    {
        public int columns, rows;
        public float horizontalOffset, verticalOffset;
    }
    public Grid grid;
    public GameObject gridTile;
    public List<Vector2> availablePoints = new();

    void Awake()
    {
        room = GetComponentInParent<Room>();
        grid.columns = room.width - 2;
        grid.rows = room.height - 2;
        GenerateGrid();
    }

    public void GenerateGrid()
    {
        // grid.verticalOffset = room.transform.position.x;
        // grid.horizontalOffset = room.transform.position.y;

        for (int y = 0; y < grid.rows; y++)
        {
            for (int x = 0; x < grid.columns; x++)
            {
                GameObject go = Instantiate(gridTile, transform);
                go.transform.position = new Vector2(x - (grid.rows - grid.horizontalOffset), y - (grid.columns - grid.verticalOffset));
                go.name = "X: " + x + ",Y: " + y;
                availablePoints.Add(go.transform.position);
            }
        }
    }
}
