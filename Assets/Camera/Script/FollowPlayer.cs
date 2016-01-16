using UnityEngine;
using System;
using System.Collections;

public class FollowPlayer : MonoBehaviour {
	public float speed;

	private GameObject player, enemy;
	private Camera cam;

	void Start() { // initialize all variables
		cam = Camera.main; // get the main camera instance
		player = GameObject.FindGameObjectWithTag ("Player"); // get player's gameobject
		enemy = GameObject.FindGameObjectWithTag ("Enemy"); // get enemy's gameobject
	}

	void FixedUpdate () {
		float distance = Vector3.Distance (player.transform.position, enemy.transform.position);
		Vector3 middle = (player.transform.position + enemy.transform.position) / 2; // find the middle coordinate between player and enemy

		middle.z = -10; // set the z value of camera to be fixed
		transform.position = Vector3.Lerp (transform.position, middle, speed); // camera follow both player and enemy movement (middle coordinate)
	
		cam.orthographicSize = distance; // change camera size(or zoom) based on distance between player & enemy
		if (cam.orthographicSize <= 80) // 80 <= cam size <= 150
			cam.orthographicSize = 80;
		else if (cam.orthographicSize >= 150)
			cam.orthographicSize = 150;
	}
}
