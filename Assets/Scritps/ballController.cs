using UnityEngine;
using System.Collections;

public class ballController : MonoBehaviour {

	int invert;
	public int towardsPlayer;
	private float velX;//Velocidad hacia el jugador
	private float velZ;//Velocidad hacia los muros

	/// <summary>
	/// Ejes de lanzamiento de la pelota
	/// invert 1 y tp 1 -> Hacia arriba y a la computadora
	/// invert -1 y tp 1 -> Hacia abajo y a la computadora
	/// invert 1 y tp -1 -> Hacia arriba y al jugador
	/// invert -1 y tp -1 -> Hacia arriba y al jugador
	/// 
	/// </summary>

	// Use this for initialization
	void Start () {
		//Velocidad aleatoria
		velX = Random.Range(10.0f, 20.0f);
		velZ = Random.Range(5.0f, 15.0f);
		/*invert = 1;
		towardsPlayer = -1;
		GetComponent<Rigidbody> ().velocity = new Vector3 (-velX, 0.0f, velZ);*/

		//Numero aleatorios para saber las direcciones
		int rand1 = Random.Range (1, 20);
		int rand2 = Random.Range (1, 20);

		//Ir a hacia arriba o hacia abajo
		if (rand1 <= 10) invert = -1;
		else invert = 1;

		//Hacia adelante o hacia atras
		if (rand2 <= 10) towardsPlayer = -1;
		else towardsPlayer = 1;

		//Agregar la velocidad
		GetComponent<Rigidbody> ().velocity = new Vector3 (velX * towardsPlayer, 0.0f, velZ * invert);
	}
	
	// Update is called once per frame
	void Update () {
		//Aumentar la velocidad de la pelota
		velX = velX + Time.deltaTime;
		velZ = velZ + Time.deltaTime;
	}

	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.name == "wall") {
			invert = invert * -1;
			GetComponent<Rigidbody> ().velocity = new Vector3 (velX * towardsPlayer, 0.0f, velZ * invert);
		} else if (collision.gameObject.name == "player") {
			GetComponent<AudioSource>().Play();
			towardsPlayer = 1;
			GetComponent<Rigidbody> ().velocity = new Vector3 (velX * towardsPlayer, 0.0f, velZ * invert);
		} else if (collision.gameObject.name == "computer") {
			GetComponent<AudioSource>().Play();
			towardsPlayer = -1;
			GetComponent<Rigidbody> ().velocity = new Vector3 (velX * towardsPlayer, 0.0f, velZ * invert);
		}
	}
}
