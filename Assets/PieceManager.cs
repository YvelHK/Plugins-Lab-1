using System;
using Unity.VisualScripting;
using UnityEngine;

public class PieceManager : MonoBehaviour
{
    [SerializeField] Piece[] pieces;
    color[][] grid;

    const float OFFSET = 3.5f;
    public void Setup()
    {
        foreach (Piece p in pieces)
        {
            int newX = (int) (Math.Round(p.transform.position.x + 0.5) - 0.5f + OFFSET);
            int newY = (int)(Math.Round(p.transform.position.y + 0.5) - 0.5f + OFFSET);
            grid[newX][newY] = p.pieceColor;
        }
    }

    public int IsOccupied(Vector2 coords) // Returns 0 for unoccupied, 1 for occupied by ally, and 2 for occupied by enemy
    {
        return 0;
    }
}
