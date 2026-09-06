using UnityEngine;

public class DrawBoard : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        bool white = true;
        for (int x = -4; x < 4; x++)
        {
            white = !white;
            for (int y = -4; y < 4; y++)
            {
                if (white)
                    Gizmos.color = Color.white;
                else
                    Gizmos.color = Color.black;
                white = !white;
                Vector3 pos = new Vector3(x + 0.5f, y + 0.5f, 0);
                Gizmos.DrawCube(pos, Vector3.one);
            }
        }
    }
}
