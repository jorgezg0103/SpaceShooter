using UnityEngine;

[CreateAssetMenu(fileName = "Waypoints", menuName = "ScriptableObjects/Waypoints", order = 1)]
public class Waypoints : ScriptableObject {
    public Vector2[][] paths = { zigzag, square, doubleV };
    
    public static Vector2[] zigzag = {
        new Vector2(-3, 6),
        new Vector2(-3, 4),
        new Vector2(3, 2),
        new Vector2(-3, 0),
        new Vector2(3, -2),
        new Vector2(-3, -4),
        new Vector2(-3, -6),
    };

    public static Vector2[] square = {
        new Vector2(-3, 6),
        new Vector2(-3, 4),
        new Vector2(3, 4),
        new Vector2(3, 0),
        new Vector2(-3, 0),
        new Vector2(-3, -4),
        new Vector2(3, -4),
        new Vector2(3, -6),
    };

    public static Vector2[] doubleV = {
        new Vector2(-3, 6),
        new Vector2(-3, 4),
        new Vector2(-1.5f, 1),
        new Vector2(0, 4),
        new Vector2(1.5f, 1),
        new Vector2(3, 4),
        new Vector2(3, -1),
        new Vector2(1.5f, -4),
        new Vector2(0, -1),
        new Vector2(-1.5f, -4),
        new Vector2(-3, -1),
        new Vector2(-3, -6)
    };

}