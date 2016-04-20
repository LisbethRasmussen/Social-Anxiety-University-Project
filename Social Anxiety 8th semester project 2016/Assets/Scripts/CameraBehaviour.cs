using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {

	private GameObject Player;
	public float MyConstantPositionZ = 0;
	public float MyConstantDifferenceToPlayerOnAxisY = 0;

	// Use this for initialization
	void Start () {
		Player = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () {

		gameObject.transform.position = new Vector3 (Player.transform.position.x, Player.transform.position.y + MyConstantDifferenceToPlayerOnAxisY, MyConstantPositionZ);
	
	}
}
