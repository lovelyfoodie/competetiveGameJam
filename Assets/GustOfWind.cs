using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor.Animations;

public class GustOfWind : MonoBehaviour {


	[SerializeField]
	private GameObject [] spawnPoints; 
	private const float MIN_TIME = 7;
	private const float MAX_TIME = 14;
	[SerializeField]
	private GameObject gustPrefab; 
	private Tower tower; 
	float xForce;

	// Use this for initialization
	void Start () {

		float randomTime = Random.Range (MIN_TIME, MAX_TIME);
		Invoke ("Gust", randomTime);
		tower = FindObjectOfType<Tower> ();
		
	}
	
	// Update is called once per frame
	void Gust () {

		float randomTime = Random.Range (MIN_TIME, MAX_TIME);

		int spawnPoint = Random.Range (0, 4);

		GameObject currSpawn = spawnPoints [spawnPoint];
		Debug.Log ("Spawn " + spawnPoint);

		GameObject gustPrefabInstance = Instantiate (gustPrefab, currSpawn.gameObject.transform.position, currSpawn.gameObject.transform.rotation);

		Animator gustAnimator = gustPrefabInstance.gameObject.GetComponent<Animator> ();


		//Todo: play animation
		if (spawnPoint == 2 || spawnPoint == 3) {
			xForce = Random.Range (20, 40) * -1;
			Debug.Log ("LeftGust: "+xForce.ToString ());
			
		} 
		else {
			xForce = Random.Range (20, 40);
			Debug.Log ("RightGust"+xForce.ToString ());
		}


		tower.addForce (xForce);


		Invoke ("Gust", randomTime);




		
	}
}
