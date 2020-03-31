using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameUtility
{
    private static readonly Camera MainCam = Camera.main;
    
    private static readonly float SceneWidth = MainCam.orthographicSize * 2 * MainCam.aspect;
    private static readonly float SceneHeight = MainCam.orthographicSize * 2;

    private static readonly float SceneRightEdge = SceneWidth / 2;
    private static readonly float SceneLeftEdge = SceneRightEdge * -1;
    private static readonly float SceneTopEdge = SceneHeight / 2;
    private static readonly float SceneBottomEdge = SceneTopEdge * -1;
    public static Vector3 CheckPositionAndTeleport(Vector3 position)
    {
        if (position.x > SceneRightEdge)
        {
            position = new Vector2(SceneLeftEdge, position.y);
        }

        if (position.x < SceneLeftEdge)
        {
            position = new Vector2(SceneRightEdge, position.y);
        }
        
        if (position.y > SceneTopEdge)
        {
            position = new Vector2(position.x, SceneBottomEdge);
        }
        if (position.y < SceneBottomEdge)
        {
            position = new Vector2(position.x, SceneTopEdge);
        }

        return position;
    }
    
    public static Vector3 CheckPositionAndTeleport(Vector3 position, Collider2D collider)
    {
        var size = collider.bounds.size / 2;

        if (position.x-size.x  > SceneRightEdge)
        {
            position = new Vector2(SceneLeftEdge+size.x , position.y);
        }

        if (position.x+size.x  < SceneLeftEdge)
        {
            position = new Vector2(SceneRightEdge-size.x , position.y);
        }
        
        if (position.y -size.y > SceneTopEdge)
        {
            position = new Vector2(position.x, SceneBottomEdge+size.y );
        }
        if (position.y+size.y  < SceneBottomEdge)
        {
            position = new Vector2(position.x, SceneTopEdge-size.y );
        }

        return position;
    }

}
