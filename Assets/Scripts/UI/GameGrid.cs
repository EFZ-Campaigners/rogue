using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid : MonoBehaviour
{
    private int height = 10;
    private int width = 10;
    private float gridSpaceSize = 5f;

    [SerializeField] private GameObject gridCellPrefab;
    private GameObject[,] gameGrid;

    private void Start()
    {
        CreateGrid();
    }

    private void CreateGrid()
    {
        if (gridCellPrefab == null) return;
        for (int x = 0; x < height; x++)
        {
            for (int y = 0; y < width; y++)
            {
                gameGrid[x, y] = Instantiate(gridCellPrefab, new Vector3(x * gridSpaceSize, y * gridSpaceSize), Quaternion.identity);
                gameGrid[x, y].transform.parent = transform;
                gameGrid[x, y].gameObject.name = "Grid(" + x + ", " + y + ")";
            }
        }
    }
}
