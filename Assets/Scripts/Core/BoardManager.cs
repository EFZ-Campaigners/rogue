using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BoardManager
{
    static Vector3 pivot = new Vector3(-2.25f, -1.25f, 0);
    static Vector2 cellSize = Vector2.one * 0.5f;
    static Vector2Int boardSize = new Vector2Int(9, 6);

    public static Vector2Int ScreenToBoardPoint()
    {
        Vector3 vector = new Vector3(Input.mousePosition.x, Input.mousePosition.y, pivot.z - Camera.main.transform.position.z);
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(vector);

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            worldPoint = hit.point;
        }

        return WorldToBoardPoint(worldPoint);
    }

    public static Vector3 ScreenToWorldPointRegulized()
    {
        Vector2Int vector = ScreenToBoardPoint();
        return BoardToWorldPoint(vector);
    }

    public static Vector2Int WorldToBoardPoint(Vector3 position)
    {
        Vector2Int boardPoint = new Vector2Int();
        boardPoint.x = Mathf.RoundToInt((position.x - pivot.x) / cellSize.x);
        boardPoint.x = (int)Mathf.Clamp(boardPoint.x, 0, boardSize.x - 1);
        boardPoint.y = Mathf.RoundToInt((position.y - pivot.y) / cellSize.y);
        boardPoint.y = (int)Mathf.Clamp(boardPoint.y, 0, boardSize.y - 1);

        return boardPoint;
    }

    public static Vector3 BoardToWorldPoint(Vector2Int position)
    {
        Vector3 worldPoint = new Vector3();
        worldPoint.x = position.x * cellSize.x + pivot.x;
        worldPoint.y = position.y * cellSize.y + pivot.y;
        worldPoint.z = pivot.z;

        return worldPoint;
    }

    public static List<Vector2Int> GetBoardPointRange(Vector2Int position, int range)
    {
        List<Vector2Int> result = new List<Vector2Int>();

        for (int x = -range + position.x; x <= range + position.x; x++)
        {
            int yStart = Mathf.Abs(x - position.x) - range;
            for (int y = yStart + position.y; y <= -yStart + position.y; y++)
            {
                Vector2Int pos = new Vector2Int(x, y);
                if (x >= 0 && x < boardSize.x && y >= 0 && y < boardSize.y)
                {
                    result.Add(new Vector2Int(x, y));
                }
            }
        }

        result.Add(position);

        return result;
    }
}

