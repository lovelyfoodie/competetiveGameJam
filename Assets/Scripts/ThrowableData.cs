using UnityEngine;

[CreateAssetMenu(fileName = "ThrowableData", menuName = "Throwables", order = 1)]
public class ThrowableData : ScriptableObject
{
    public float density;
    public float minSize = 1f;
    public float maxSize = 1f;
    public AnimationCurve sizeDistribution;
    public WwisePostEvent onThrownSound;
    public WwiseSetRTPC thrownObjectSize;

    public float Size(float ratio)
    {
        return (maxSize - minSize) * sizeDistribution.Evaluate(ratio) + minSize;
    }
}