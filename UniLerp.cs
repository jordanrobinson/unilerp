using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class UniLerp
{
    private static readonly List<Lerpable> Lerpables = new List<Lerpable>();

    /// <summary>
    /// Checks all lerpable items set up and steps through each, applying the movement required.
    /// </summary>
    public static void Tick()
    {

        if (IsLerping())
        {
            foreach (var lerpable in Lerpables.Where(lerpable => lerpable.Lerping))
            {
                LerpStep(lerpable);
            }
        }
    }

    private static bool IsLerping()
    {
        return Lerpables.Any(lerpable => lerpable.Lerping);
    }

    /// <summary>
    /// Starts a lerp transition for a given game object
    /// </summary>
    /// <param name="gameObject">The game object to move</param>
    /// <param name="targetPosition">The target position of the game object, can be omitted if no transition required</param>
    /// <param name="targetRotation">The target rotation of the game object, can be omitted if no transition required</param>
    /// <param name="targetScale">The target scale of the game object, can be omitted if no transition required</param>
    public static void StartLerp(GameObject gameObject, Vector3 targetPosition = new Vector3(), Vector3 targetRotation = new Vector3(), Vector3 targetScale = new Vector3())
    {
        var lerpable = new Lerpable(gameObject);

        lerpable.StartPosition = lerpable.InnerGameObject.transform.position;
        lerpable.TargetPosition = targetPosition;

        lerpable.StartAngle = lerpable.InnerGameObject.transform.eulerAngles;
        lerpable.TargetAngle = targetRotation;

        lerpable.StartScale = lerpable.InnerGameObject.transform.localScale;
        lerpable.TargetScale = targetScale;

        lerpable.StartTime = Time.time;

        lerpable.JourneyLength = Vector3.Distance(lerpable.StartPosition, lerpable.TargetPosition);
        lerpable.AngleLength = Vector3.Distance(lerpable.StartAngle, lerpable.TargetAngle);
        lerpable.ScaleLength = Vector3.Distance(lerpable.StartScale, lerpable.TargetScale);

        lerpable.Lerping = true;

        Lerpables.Add(lerpable);
    }

    private static void LerpStep(Lerpable lerpable)
    {
        // position
        var distCovered = (Time.time - lerpable.StartTime) * lerpable.PositionSpeed;
        var fracJourney = distCovered / lerpable.JourneyLength;
        lerpable.InnerGameObject.transform.position = Vector3.Lerp(lerpable.StartPosition, lerpable.TargetPosition, fracJourney);
        // angle
        var angleCovered = (Time.time - lerpable.StartTime) * lerpable.AngleSpeed;
        var angleJourney = angleCovered / lerpable.AngleLength;
        lerpable.InnerGameObject.transform.eulerAngles = Vector3.Lerp(lerpable.StartAngle, lerpable.TargetAngle, angleJourney);
        // scale
        var scaleCovered = (Time.time - lerpable.StartTime) * lerpable.ScaleSpeed;
        var scaleJourney = scaleCovered / lerpable.ScaleLength;
        lerpable.InnerGameObject.transform.localScale = Vector3.Lerp(lerpable.StartScale, lerpable.TargetScale, scaleJourney);

        if (angleJourney > 1.0f
            && lerpable.InnerGameObject.transform.position == lerpable.TargetPosition
            && lerpable.InnerGameObject.transform.localScale == lerpable.TargetScale)
        {
            lerpable.Lerping = false;
        }
    }
}

public class Lerpable
{
    public Lerpable(GameObject gameObject, float positionSpeed = 10.0f, float angleSpeed = 60.0f, float scaleSpeed = 100.0f)
    {
        InnerGameObject = gameObject;

        PositionSpeed = positionSpeed;
        AngleSpeed = angleSpeed;
        ScaleSpeed = scaleSpeed;
    }

    public float StartTime { get; set; }

    public float JourneyLength { get; set; }
    public float AngleLength { get; set; }
    public float ScaleLength { get; set; }

    public readonly float PositionSpeed;
    public readonly float AngleSpeed;
    public readonly float ScaleSpeed;
    public bool Lerping { get; set; }

    public Vector3 StartPosition { get; set; }
    public Vector3 TargetPosition { get; set; }

    public Vector3 StartAngle { get; set; }
    public Vector3 TargetAngle { get; set; }

    public Vector3 StartScale { get; set; }
    public Vector3 TargetScale { get; set; }

    public readonly GameObject InnerGameObject;
}
