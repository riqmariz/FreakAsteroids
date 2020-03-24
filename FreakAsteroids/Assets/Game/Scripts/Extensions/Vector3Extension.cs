using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameUtility
{
    private static readonly Camera mainCam = Camera.main;
    public static Vector3 CheckPositionAndTeleport(Vector3 position)
    {
        float sceneWidth = mainCam.orthographicSize * 2 * mainCam.aspect;
        float sceneHeight = mainCam.orthographicSize * 2;

        float sceneRightEdge = sceneWidth / 2;
        float sceneLeftEdge = sceneRightEdge * -1;
        float sceneTopEdge = sceneHeight / 2;
        float sceneBottomEdge = sceneTopEdge * -1;

        if (position.x > sceneRightEdge)
        {
            position = new Vector2(sceneLeftEdge, position.y);
        }

        if (position.x < sceneLeftEdge)
        {
            position = new Vector2(sceneRightEdge, position.y);
        }
        
        if (position.y > sceneTopEdge)
        {
            position = new Vector2(position.x, sceneBottomEdge);
        }
        if (position.y < sceneBottomEdge)
        {
            position = new Vector2(position.x, sceneTopEdge);
        }

        return position;
    }
    
}
