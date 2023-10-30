using UnityEngine;

public static class BoardExtension {
    public static bool IsOverlap(this MergeCircle circle1, MergeCircle circle2) {
        return Vector2.Distance(circle1.transform.position, circle2.transform.position) <= (circle1.Radius + circle2.Radius);
    }

    public static bool IsUpper(this MergeCircle circle, float y) {
        return circle.transform.position.y + circle.Radius >= y;
    }
}