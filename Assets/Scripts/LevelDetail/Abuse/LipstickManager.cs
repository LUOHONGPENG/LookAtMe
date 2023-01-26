using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LipstickManager : MonoBehaviour
{
    public LineRenderer lipstickLine;

    List<Vector2> points;

    public void UpdateLine(Vector2 position)
    {
        if(points == null)
        {
            points = new List<Vector2>();
            SetPoint(position);
            return;
        }

        if(Vector2.Distance(points[points.Count-1],position) > 0.1f)
        {
            SetPoint(position);
        }
    }

    void SetPoint(Vector2 point)
    {
        points.Add(point);
        lipstickLine.positionCount = points.Count;
        lipstickLine.SetPosition(points.Count - 1, point);
    }

}
