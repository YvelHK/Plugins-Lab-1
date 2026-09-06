using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Piece))]
public class PlacesToMove: Editor
{
    private void OnSceneGUI()
    {
        Piece piece = (Piece)target;
        List<Vector2> coordsToDraw = piece.GetAvailableSpaces();
        foreach (Vector2 coords in coordsToDraw)
        {
            // If the move is a capture, make it red, otherwise make it green
            if (piece.manager.IsOccupied(coords, piece.pieceColor) == 2)
                Handles.color = Color.red + new Color(0, 0, 0, -0.75f);
            else
                Handles.color = Color.green + new Color(0, 0, 0, -0.75f);

            // Draw the move
            Handles.DrawSolidDisc(coords, Vector3.back, 0.25f);
        }
    }
}
