﻿using UnityEngine;
using System.Collections;

// PLEASE NOTE! THIS SCRIPT IS FOR DEMO PURPOSES ONLY :) //

public class Plane : MonoBehaviour {
	public GameObject prop;
	public GameObject propBlured;

	public float speed = 20f;

	public bool engenOn;

	public GameObject projectile;
	private GameObject bullet;

	void Update () 
	{
		if (engenOn) {
			prop.SetActive (false);
			propBlured.SetActive (true);
			propBlured.transform.Rotate (1000 * Time.deltaTime, 0, 0);
		} else {
			prop.SetActive (true);
			propBlured.SetActive (false);
		}
	}
	public void Shoot()
	{
		Debug.Log("Shot fired");
		bullet = Instantiate(projectile, transform.position, Quaternion.identity);
		Vector3 vec = transform.parent.transform.position - transform.position ;
		vec.Normalize();
		
		bullet.GetComponent<Rigidbody>().velocity = vec * speed;
	}
}

// PLEASE NOTE! THIS SCRIPT IS FOR DEMO PURPOSES ONLY :) //