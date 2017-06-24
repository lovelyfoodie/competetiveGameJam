using UnityEngine;

[CreateAssetMenu(fileName = "ThrowableData", menuName = "Throwables", order = 1)]
public class ThrowableData : ScriptableObject
{
    public float density;
    public WwisePostEvent onThownSound;
}