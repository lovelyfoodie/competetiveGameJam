using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour {

    public GameObject leftPlayerPrefab;
    public GameObject rightPlayerPrefab;

    public Transform leftSpawnPosition;
    public Transform rightSpawnPosition;

    private void Start()
    {
        if (leftPlayerPrefab != null)
        {
            GameObject left = Instantiate(leftPlayerPrefab);
            left.transform.SetParent(leftSpawnPosition);
            left.transform.position = leftSpawnPosition.position;
        }

        if (rightPlayerPrefab != null)
        {
            GameObject right = Instantiate(rightPlayerPrefab);
            right.transform.SetParent(rightSpawnPosition);
            right.transform.position = rightSpawnPosition.position;
        }
    }
}
