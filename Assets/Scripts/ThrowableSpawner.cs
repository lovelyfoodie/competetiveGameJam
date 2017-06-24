using System.Collections.Generic;
using UnityEngine;

public class ThrowableSpawner : MonoBehaviour
{
    public ThrowableSpawnerData spawnProfile;

    private int _seed = 0;
    private int _spawnCount = 0;
    private List<GameObject> _throwables = new List<GameObject>();
    private List<GameObject> _throwablesBase = new List<GameObject>();

    private void Awake()
    {
        _seed = (int)(Random.value * int.MaxValue);
        Init();
    }

    private void Init()
    {
        List<GameObject> list = new List<GameObject>();
        foreach (var item in spawnProfile.spawnData)
        {
            for (int i = 0; i < item.weight; i++)
            {
                list.Add(item.prefabRef);
            }
        }
        list.Shuffle();

        _throwablesBase = _throwables = list;
    }

    public void LoadNewProfile(ThrowableSpawnerData data)
    {
        spawnProfile = data;
        Init();
    }

    public GameObject GetThrowable()
    {
        Random.InitState(_seed + _spawnCount);
        int index = Random.Range(0, _throwables.Count);

        GameObject throwable = _throwables[index];
        _throwables.Remove(throwable);
        _throwables.Add(throwable);

        _spawnCount++;

        return throwable;
    }
}



