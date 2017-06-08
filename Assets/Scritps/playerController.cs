using UnityEngine;
using System.Collections;


public class playerController : MonoBehaviour {
	//Variables para cambiar la camara
	public Camera midCam;
	public Camera frontCam;
	public Camera playerCam;

	//movimiento del jugador
	public float moveSpeed;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		movePlayer ();
	}

	void movePlayer(){
		if (midCam.enabled == true) {
				if (Input.GetKey (KeyCode.UpArrow)) {
					transform.Translate (Vector3.forward * moveSpeed * Time.deltaTime);
				}
				if (Input.GetKey (KeyCode.DownArrow)){
					transform.Translate (-Vector3.forward * moveSpeed * Time.deltaTime);
				}
		} else {
				if (Input.GetKey (KeyCode.LeftArrow)) {
					transform.Translate (Vector3.forward * moveSpeed * Time.deltaTime);
				}
				if (Input.GetKey (KeyCode.RightArrow))
					transform.Translate (-Vector3.forward * moveSpeed * Time.deltaTime);
			}
	}


}
