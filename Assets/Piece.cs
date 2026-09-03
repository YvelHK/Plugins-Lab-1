using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Piece : MonoBehaviour
{
    [SerializeField] piece pieceType;
    public color pieceColor;
    [SerializeField] PieceManager manager;

    private void OnDrawGizmos()
    {
        Color tint;
        if (pieceColor == color.white)
            tint = Color.white;
        else
            tint = Color.black;
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

    List<Vector2> GetAvailableSpaces()
    {
        List<Vector2> retlist = new List<Vector2>();
        switch (pieceType)
        {
            case (piece.pawn):
                Vector2 direction;
                if (pieceColor == color.white)
                    direction = Vector2.down;
                else
                    direction = Vector2.up;

                // Basic Move
                Vector2 check = (Vector2) transform.position + direction;
                if (InBoundaries(check) && manager.IsOccupied(check) != 2)
                    retlist.Add(check);

                // Double First Move
                if ((direction == Vector2.up && transform.position.y < -2) || (direction == Vector2.down && transform.position.y > 2))
                {
                    check = (Vector2) transform.position + direction * 2;
                    if (InBoundaries(check) && manager.IsOccupied(check) != 2)
                        retlist.Add(check);
                }
                    
                // I'm not checking En Passant
                return retlist;

            case (piece.knight):
                break;
        }
        return retlist;
    }

    bool InBoundaries(Vector2 coords)
    {
        if (coords.x < 4 && coords.x > -4 && coords.y < 4 && coords.y > -4)
            return true;
        return false;
    }
}
