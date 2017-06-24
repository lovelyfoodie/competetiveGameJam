using UnityEngine;

public class ThrowableSpawnerData : ScriptableObject
{
    [SerializeField]
    [HideInInspector]
    public SpawnData[] spawnData;
}

[System.Serializable]
public struct SpawnData
{
    public int weight;
    public GameObject prefabRef;
}