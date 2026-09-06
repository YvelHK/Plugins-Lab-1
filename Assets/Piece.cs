using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Piece : MonoBehaviour
{
    [SerializeField] piece pieceType;
    public color pieceColor;
    public PieceManager manager;

    private void OnDrawGizmos()
    {

        // Set the piece's color
        Color tint;
        if (pieceColor == color.white)
            tint = Color.white;
        else
            tint = Color.black + new Color(0.1f, 0.1f, 0.1f);

        // Draw the piece's icon
        switch (pieceType)
        {
            case piece.pawn:
                Gizmos.DrawIcon(transform.position, "Pawn", true, tint);
                break;
            case piece.knight:
                Gizmos.DrawIcon(transform.position, "Knight", true, tint);
                break;
            case piece.bishop:
                Gizmos.DrawIcon(transform.position, "Bishop", true, tint);
                break;
            case piece.rook:
                Gizmos.DrawIcon(transform.position, "Rook", true, tint);
                break;
            case piece.queen:
                Gizmos.DrawIcon(transform.position, "Queen", true, tint);
                break;
            case piece.king:
                Gizmos.DrawIcon(transform.position, "King", true, tint);
                break;
        }
    }

    public List<Vector2> GetAvailableSpaces()
    {
        List<Vector2> retlist = new List<Vector2>();
        Vector2 check;
        switch (pieceType)
        {
            case piece.pawn:
                // Check which direction the pawn is traveling
                Vector2 direction;
                if (pieceColor == color.white)
                    direction = Vector2.down;
                else
                    direction = Vector2.up;

                // Basic Move
                check = (Vector2) transform.position + direction;
                if (InBoundaries(check) && manager.IsOccupied(check, pieceColor) == 0)
                {
                    retlist.Add(check);

                    // Double First Move
                    if ((direction == Vector2.up && transform.position.y < -2) || (direction == Vector2.down && transform.position.y > 2))
                    {
                        check = (Vector2)transform.position + direction * 2;
                        if (InBoundaries(check) && manager.IsOccupied(check, pieceColor) == 0)
                            retlist.Add(check);
                    }
                }

                // Check Left Diagonal Capture
                check = (Vector2)transform.position + direction + Vector2.left;
                if (InBoundaries(check) && manager.IsOccupied(check, pieceColor) == 2)
                    retlist.Add(check);

                // Check Right Diagonal Capture
                check = (Vector2)transform.position + direction + Vector2.right;
                if (InBoundaries(check) && manager.IsOccupied(check, pieceColor) == 2)
                    retlist.Add(check);

                // I'm not checking En Passant
                return retlist;

            case piece.knight:
                for (int x = -2; x <= 2; x++)
                {
                    if (x == 0)
                        continue;
                    for (int y = -2; y <= 2; y++)
                    {
                        // If the move isn't how knights move, continue
                        if (y == 0 || math.abs(x) == math.abs(y))
                            continue;

                        check = (Vector2) transform.position + new Vector2(x, y);
                        if (InBoundaries(check) && manager.IsOccupied(check, pieceColor) != 1)
                            retlist.Add(check);
                    }
                }
                return retlist;

            case piece.bishop:
            case piece.queen:

                // Check all diagonal directions
                for (int x = -1;  x <= 1; x++)
                {
                    if (x == 0)
                        continue;
                    for (int y = -1; y <= 1; y++)
                    {
                        if (y == 0)
                            continue;

                        int dist = 1;
                        Vector2 dir = new Vector2(x, y);
                        while (true)
                        {
                            check = (Vector2) transform.position + (dir * dist);
                            int result = manager.IsOccupied(check, pieceColor);
                            
                            // If off the board or hitting an ally, stop
                            if (!InBoundaries(check) || result == 1)
                                break;
                            
                            // If hitting an enemy, add the hit then stop
                            if (result == 2)
                            {
                                retlist.Add(check);
                                break;
                            }

                            // Otherwise, add the empty space and keep going
                            retlist.Add(check);
                            dist++;
                        }
                    }
                }
                if (pieceType == piece.bishop)
                    return retlist;

                // Let the queen case fall through into the rook checks
                goto case piece.rook;


            case piece.rook:
                // Check all non-diagonal directions
                for (int x = -1; x <= 1; x++)
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        // Skip diagonals
                        if (math.abs(x) == math.abs(y))
                            continue;

                        int dist = 1;
                        Vector2 dir = new Vector2(x, y);
                        while (true)
                        {
                            check = (Vector2)transform.position + (dir * dist);
                            int result = manager.IsOccupied(check, pieceColor);

                            // If off the board or hitting an ally, stop
                            if (!InBoundaries(check) || result == 1)
                                break;

                            // If hitting an enemy, add the hit then stop
                            if (result == 2)
                            {
                                retlist.Add(check);
                                break;
                            }

                            // Otherwise, add the empty space and keep going
                            retlist.Add(check);
                            dist++;
                        }
                    }
                }
                return retlist;

            case piece.king:
                {
                    for (int x = -1; x <= 1; x++)
                    {
                        for (int y = -1; y <= 1; y++)
                        {
                            // Skip not moving
                            if (x == 0 && y == 0)
                                continue;

                            check = (Vector2)transform.position + new Vector2(x, y);
                            if (InBoundaries(check) && manager.IsOccupied(check, pieceColor) != 1)
                                retlist.Add(check);
                        }
                    }
                    // I'm not checking castling
                    break;
                }
        }
        return retlist;
    }

    bool InBoundaries(Vector2 coords) // Check if the move is within the board
    {
        if (coords.x < 4 && coords.x > -4 && coords.y < 4 && coords.y > -4)
            return true;
        return false;
    }
}
