using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PieceManager : MonoBehaviour
{
    [SerializeField] Piece[] pieces;
    Dictionary<Vector2, color> board = new Dictionary<Vector2, color>();

    private void OnDrawGizmos()
    {
        board.Clear();
        foreach (Piece p in pieces)
        {
            board.Add(p.transform.position, p.pieceColor);
        }
    }

    public int IsOccupied(Vector2 coords, color pieceColor) // Returns 0 for unoccupied, 1 for occupied by ally, and 2 for occupied by enemy
    {
        
        if (!board.ContainsKey(coords))
            return 0;
        color getColor = board[coords];
        if (getColor == pieceColor)
            return 1;
        return 2;
    }
}
