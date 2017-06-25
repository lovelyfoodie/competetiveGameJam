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

    public GameObject Peek(int numAhead)
    {
        Random.InitState(_seed + _spawnCount + numAhead);
        float r = Random.value;
        int index = (int)(r * _throwables.Count);

        GameObject go = Instantiate(_throwables[index], gameObject.transform.position, gameObject.transform.rotation) as GameObject;

        Throwable throwable = go.GetComponent<Throwable>();
        throwable.Size = throwable.data.Size(r);

        return go;
    }
    //public GameObject Peek(int numAhead)
    //{
    //    Random.InitState(_seed + _spawnCount + numAhead);
    //    float r = Random.value;
    //    int index = (int)(r * _throwables.Count);

    //    GameObject go = _throwables[index];

    //    Throwable throwable = go.GetComponent<Throwable>();
    //    throwable.Size = throwable.data.Size(r);

    //    return go;
    //}
    public GameObject GetThrowable()
    {
        GameObject throwable = Peek(0);
        _spawnCount++;

        return throwable;
    }
}



