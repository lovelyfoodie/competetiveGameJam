using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadClouds : MonoBehaviour {

    public int numClouds = 6;
    public GameObject prefab;
    public Transform spawnPointTop;
    public Transform spawnPointBottom;

    void Awake () {
        for (int i = 0; i < numClouds; i++)
        {
            GameObject go = Instantiate(prefab);
            go.transform.position = Vector3.Lerp(spawnPointTop.position, spawnPointBottom.position, Random.value);
        }
	}
}
