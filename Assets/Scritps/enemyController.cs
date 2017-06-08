using UnityEngine;
using System.Collections;

public class enemyController : MonoBehaviour {

	public GameObject ball;
	public float enemySpeed;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (ball != null) {
			if (ball.GetComponent<ballController> ().towardsPlayer == 1) {
				if (ball.transform.position.z > transform.position.z) {
					transform.Translate (Vector3.forward * enemySpeed * Time.deltaTime);
				}
				if (ball.transform.position.z < transform.position.z) {
					transform.Translate (-Vector3.forward * enemySpeed * Time.deltaTime);
				}
			} else {
				if (19.0f > transform.position.z) {
					transform.Translate (Vector3.forward * enemySpeed * Time.deltaTime);
				}
				if (19.0f < transform.position.z) {
					transform.Translate (-Vector3.forward * enemySpeed * Time.deltaTime);
				}
			}
		}
	}
}
