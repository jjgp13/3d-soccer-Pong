using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	//Camaras
	public Camera middleCamera;
	public Camera frontCamera;
	public Camera playerCamera;
	public Camera beginCamera;
	public Camera upCamera;
	public GameObject fieldSound;

	//Variables para las luces
	public Light sunLight;
	public GameObject stadiumLight;
	public float speedRotationLight;
	bool sunlight;

	//variables para saber el marcador
	public int playerScore;
	public int enemyScore;

	//UI objects
	public GameObject scoreUI;
	public GameObject title;
	public GameObject playBtn;
	public Text statusGame;

	//Variables para actualizar UI
	public Text playerScoreText;
	public Text enemyScoreText;
	public Text golText;
	
	//Obener las propiedades de la pelota;
	public GameObject ball;

	//Obtener el control del enemigo
	public GameObject enemy;
	public GameObject player;


	// Use this for initialization
	void Start () {
		//Inicializadar los marcadores
		playerScore = 0;
		enemyScore = 0;
		//Inicializar las camaras
		beginCamera.enabled = true;
		middleCamera.enabled = false;
		frontCamera.enabled = false;
		playerCamera.enabled = false;
		upCamera.enabled = false;
		//Inicializar la lus
		sunlight = true;
		//Ocular el score
		scoreUI.SetActive (false);
		title.SetActive (true);
		playBtn.SetActive (true);
		//Establecer velocidad de los jugadores
		enemy.GetComponent<enemyController> ().enemySpeed = 20;
		player.GetComponent<playerController> ().moveSpeed = 20;
	}

	//Funcion para empezar a jugar
	public void onPressPlay(){
		//Ocultar Titulo y boton.
		title.SetActive (false);
		playBtn.SetActive (false);
		//Mostrar el score
		scoreUI.SetActive (true);
		statusGame.enabled = false;
		//Inicializar las camaras
		middleCamera.enabled = true;
		beginCamera.enabled = false;
		frontCamera.enabled = false;
		playerCamera.enabled = false;
		upCamera.enabled = false;
		//Inicializar el score;
		playerScoreText.text = "PLAYER SCORE: ";
		enemyScoreText.text = "ENEMY SCORE: ";
		//mandar pelota
		pelota ();
	}

	// Update is called once per frame
	void Update () {
		//Rotacion de la luz del dia
		sunLight.GetComponent<Transform> ().Rotate (Vector3.right * speedRotationLight * Time.deltaTime);

		if((sunLight.GetComponent<Transform> ().rotation.x > 0.98f || sunLight.GetComponent<Transform> ().rotation.x < -0.98f) && sunlight) {
			stadiumLight.SetActive(true);
			sunLight.enabled = false;
			sunlight = false;
		}

		if(sunLight.GetComponent<Transform> ().rotation.x >= -0.05f && sunLight.GetComponent<Transform> ().rotation.x <= 0.05f && !sunlight) {
			stadiumLight.SetActive(false);
			sunLight.enabled = true;
			sunlight = true;
		}

		if(Input.GetKey(KeyCode.M)){
			middleCamera.enabled = true;
			frontCamera.enabled = false;
			playerCamera.enabled = false;
			upCamera.enabled = false;
		}

		if(Input.GetKey(KeyCode.U)){
			upCamera.enabled = true;
			middleCamera.enabled = true;
			frontCamera.enabled = false;
			playerCamera.enabled = false;
		}

		if(Input.GetKey(KeyCode.F)){
			frontCamera.enabled = true;
			middleCamera.enabled = false;
			playerCamera.enabled = false;
			upCamera.enabled = false;
		}

		if (Input.GetKey (KeyCode.P)) {
			playerCamera.enabled = true;
			middleCamera.enabled = false;
			frontCamera.enabled = false;
			upCamera.enabled = false;
		}

	}

	
	void OnTriggerExit(Collider other){
		if (other.GetComponent<ballController> ().towardsPlayer == -1) {
			enemyScore++;
			enemyScoreText.text = "ENEMY SCORE: " + enemyScore;
			StartCoroutine("showGolText");
			IncrementarDificultad(1);
		}
		if (other.GetComponent<ballController> ().towardsPlayer == 1) {
			playerScore++;
			playerScoreText.text = "PLAYER SCORE: " + playerScore;
			StartCoroutine("showGolText");
			IncrementarDificultad(-1);
		}
		Destroy (other.gameObject);
		fieldSound.GetComponent<AudioSource> ().Play ();
		StartCoroutine ("spawnBalls");
		enemy.GetComponent<enemyController>().ball = null;
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.name == "ball(Clone)") {
			enemy.GetComponent<enemyController>().ball = other.gameObject;
		}
	}

	void IncrementarDificultad(int point){
		if (point == -1) {
			//Punto a favor
			//Aumentar la velocidad de la computadora
			enemy.GetComponent<enemyController> ().enemySpeed = enemy.GetComponent<enemyController> ().enemySpeed + 4;
			//Decrementar la velocidad el jugador.
			player.GetComponent<playerController> ().moveSpeed = player.GetComponent<playerController> ().moveSpeed - 4;
		} else {
			//Punto en contra
			//Decrementar la velocidad de la computadora
			enemy.GetComponent<enemyController> ().enemySpeed = enemy.GetComponent<enemyController> ().enemySpeed - 2;
			//Aumentar la velocidad el jugador.
			player.GetComponent<playerController> ().moveSpeed = player.GetComponent<playerController> ().moveSpeed + 2;
		}
	}

	IEnumerator showGolText(){
		golText.enabled = true;
		yield return new WaitForSeconds (3.0f);
		golText.enabled = false;
	}

	//Sacar pelota
	void pelota(){
		if (playerScore < 5 && enemyScore < 5) {
			Vector3 spawnPosition = new Vector3 (34.64f, 1.0f, 18.94f);
			Quaternion spawnRotation = Quaternion.identity;
			Instantiate (ball, spawnPosition, spawnRotation);
		} else {
			restarGame();
		}
	}

	//Restar game
	void restarGame(){
		statusGame.enabled = true;
		if (playerScore == 5) {
			statusGame.text = "You Win";
			Start();
		}
		if (enemyScore == 5) {
			statusGame.text = "You Lose";
			Start();
		}
	}

	//Funcion para lanzar los globos.
	IEnumerator spawnBalls(){
		yield return new WaitForSeconds (3.0f);
		pelota ();
	}
}