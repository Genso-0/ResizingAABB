using UnityEngine;


class BoundsSizing : MonoBehaviour
{
    public Bounds bounds;
    Vector3[] boundPoints = new Vector3[8];
    public Vector3 rotateOnAngle;
    void Update()
    {
        transform.Rotate(rotateOnAngle);
    }
    public void UpdateBounds()
    {
        boundPoints[0] = bounds.min;
        boundPoints[1] = bounds.max;
        boundPoints[2] = new Vector3(boundPoints[0].x, boundPoints[0].y, boundPoints[1].z);
        boundPoints[3] = new Vector3(boundPoints[0].x, boundPoints[1].y, boundPoints[0].z);
        boundPoints[4] = new Vector3(boundPoints[1].x, boundPoints[0].y, boundPoints[0].z);
        boundPoints[5] = new Vector3(boundPoints[0].x, boundPoints[1].y, boundPoints[1].z);
        boundPoints[6] = new Vector3(boundPoints[1].x, boundPoints[0].y, boundPoints[1].z);
        boundPoints[7] = new Vector3(boundPoints[1].x, boundPoints[1].y, boundPoints[0].z);
    }
    public Bounds GetBoundsTranslated()
    {
        Vector3 min = (transform.rotation * boundPoints[0]) + transform.position;
        Vector3 max = (transform.rotation * boundPoints[0]) + transform.position;
        for (int i = 0; i < boundPoints.Length; i++)
        {
            Vector3 translatedPoint = (transform.rotation * boundPoints[i]) + transform.position;
            for (int n = 0; n < 3; n++)
            {
                if (translatedPoint[n] > max[n])
                    max[n] = translatedPoint[n];
                if (translatedPoint[n] < min[n])
                    min[n] = translatedPoint[n];
            }
        }
        Bounds translatedBounds = new Bounds
        {
            center = transform.position + bounds.center
        };
        translatedBounds.SetMinMax(min, max);
        return translatedBounds;
    }
    public Vector3[] GetRotatedBoundsPoints()
    {
        var points = new Vector3[8];

        for (int i = 0; i < boundPoints.Length; i++)
        {
            points[i] = (transform.rotation * boundPoints[i]) + transform.position;
        }
        return points;
    }
    public void OnDrawGizmos()
    {
        UpdateBounds();
        DrawBounds(GetBoundsTranslated(), new Color(1f, 0.2f, 1f, 0.4f));
        DrawBox(GetRotatedBoundsPoints(), new Color(1f, 1f, 0f, 0.4f));
    }
    void DrawBounds(Bounds bounds, Color color)
    {
        Gizmos.DrawWireSphere(bounds.center, 0.1f);
        Gizmos.color = color;
        Gizmos.DrawCube(bounds.center, bounds.size);
        Gizmos.DrawWireCube(bounds.center, bounds.size);
    }

    void DrawBox(Vector3[] points, Color color)
    {
        Gizmos.color = color;
        for (int i = 0; i < points.Length; i++)
        {
            Gizmos.DrawWireSphere(points[i], 0.1f);
        }
        //To have icons work. Place icons in Assets/Gizmos (you might have to make a Gizmos folder).
        Gizmos.DrawIcon(points[0], "Number0 7x10");
        Gizmos.DrawIcon(points[1], "Number1 7x10");
        Gizmos.DrawIcon(points[2], "Number2 7x10");
        Gizmos.DrawIcon(points[3], "Number3 7x10");
        Gizmos.DrawIcon(points[4], "Number4 7x10");
        Gizmos.DrawIcon(points[5], "Number5 7x10");
        Gizmos.DrawIcon(points[6], "Number6 7x10");
        Gizmos.DrawIcon(points[7], "Number7 7x10");
        Gizmos.DrawLine(points[0], points[3]);
        Gizmos.DrawLine(points[0], points[4]);
        Gizmos.DrawLine(points[7], points[4]);
        Gizmos.DrawLine(points[7], points[3]);
        Gizmos.DrawLine(points[2], points[5]);
        Gizmos.DrawLine(points[2], points[6]);
        Gizmos.DrawLine(points[1], points[6]);
        Gizmos.DrawLine(points[1], points[5]);
        Gizmos.DrawLine(points[1], points[7]);
        Gizmos.DrawLine(points[5], points[3]);
        Gizmos.DrawLine(points[2], points[0]);
        Gizmos.DrawLine(points[6], points[4]);

    }
}

