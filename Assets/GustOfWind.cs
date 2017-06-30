using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor.Animations;

public class GustOfWind : MonoBehaviour
{
    public WwisePostEvent gustSound;
    public float minTime = 7f;
    public float maxTime = 14f;
    public float minForce = 50f;
    public float maxForce = 100f;
    public GameObject[] spawnPoints;

    private TowerControl _tower;

    void Awake()
    {
        _tower = FindObjectOfType<TowerControl>();
    }
    void Start()
    {
        StartCoroutine(StartGust());
    }

    IEnumerator StartGust()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minTime, maxTime));
            Gust();
        }
    }

    void Gust()
    {
        int spawnPoint = Random.Range(0, 4);
        spawnPoints[spawnPoint].GetComponent<ParticleSystem>().Play();

        float directionMod = (spawnPoint == 2 || spawnPoint == 3) ? -1f : 1f;
        _tower.AddXForce(Random.Range(minForce, maxForce) * directionMod);

        // Play gust sound.
        if (gustSound != null)
            gustSound.Post(gameObject);
    }
}