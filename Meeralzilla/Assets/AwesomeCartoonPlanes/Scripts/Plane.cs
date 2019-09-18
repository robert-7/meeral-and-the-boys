using UnityEngine;
using System.Collections;

// PLEASE NOTE! THIS SCRIPT IS FOR DEMO PURPOSES ONLY :) //

public class Plane : MonoBehaviour {
	public GameObject prop;
	public GameObject propBlured;

	public bool dead = false;
	public float speed = 20f;

	public bool engenOn;

	public GameObject projectile;
	private GameObject bullet;
	private float timer = 0.0f;

    private NetworkManager nm;

	void Start() {
		 gameObject.GetComponent<ParticleSystem>().enableEmission = false;
        nm = GameObject.Find("GameManager").GetComponent<NetworkManager>();
    }

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

		if(dead) {
			Debug.Log("Plane has died :(");
			gameObject.GetComponent<ParticleSystem>().enableEmission = true;
			timer += Time.deltaTime;
			if(timer > 2.0f) {
				timer = 0f;
				Destroy(gameObject);
			}

		}
	}
	public void Shoot()
	{
		Debug.Log("Shot fired");
		bullet = Instantiate(projectile, transform.position, Quaternion.identity);
		Vector3 vec = transform.parent.transform.position - transform.position ;
		vec.Normalize();
		
		bullet.GetComponent<Rigidbody>().velocity = vec * speed;

        this.nm.ShootBullet();
	}

	public void Die() {
		 dead = true;
	}
}

// PLEASE NOTE! THIS SCRIPT IS FOR DEMO PURPOSES ONLY :) //