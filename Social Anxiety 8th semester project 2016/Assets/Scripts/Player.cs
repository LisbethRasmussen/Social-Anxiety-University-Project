using UnityEngine;
using System.Collections;

/*
IMPORTANT
Player will only work with 3D colliders and triggers!!!
Player need a 3D collider
Player need a ridig body component. Disable the gravety and tick all the constraints on ---------------- check with Steffen's scripts

Stairs need tag either "StairL" or "StairR".
"StairL" is for those that go up from bottom right to top left, and vice versa with "StairR".

There has to be objects on the scene for every bottom of a staircase and every top of a staircase.
These objects MUST HAVE A TAG either "TOP1"/"TOP2" or "BOTTOM0"/"BOTTOM1" (case sensitive).
	"TOP" refers to the top of a staircase, and "BOTTOM" to the bottom of a staircase.
	The number refers to which floor the object is located.

The object must be placed at the exact height that the player are ment to walk at the floor on which they are placed.
Example: if the player is meant to walk at height 0.5 on the floor, the object which is placed on that specific floor, must have the same height applied.

*/

public class Player : MonoBehaviour {

	public float ForwardSpeed = 5;
	public float SidewardsSpeed = 0.1f;
	public float UpSpeed = 0;

	private bool OnStairs = false;

	private bool ConstrainOnBottomYaxis = false;
	private bool ConstrainOnTopYaxis = false;
	private float MyConstraintOnY;

	public float BottomFloorY;
	public float FirstFloorY;
	public float SecondFloorY;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		//Put movement in here

		if (Input.GetKey(KeyCode.W) && transform.position.z != 2 && OnStairs == false) {
			gameObject.transform.position += new Vector3 (0, 0, SidewardsSpeed*Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.A)){
			gameObject.transform.position += new Vector3 (-ForwardSpeed * Time.deltaTime, UpSpeed* Time.deltaTime, 0);
		}

		if (Input.GetKey(KeyCode.S) && transform.position.z != 0 && OnStairs == false) {
			gameObject.transform.position -= new Vector3 (0, 0, SidewardsSpeed*Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.D)){
			gameObject.transform.position += new Vector3 (ForwardSpeed * Time.deltaTime, -1*UpSpeed* Time.deltaTime, 0);
		}
		//We do not want the player to fly when going up the stairs, or going below the floors when going down the stairs--
		if (ConstrainOnBottomYaxis == true && transform.position.y <= MyConstraintOnY) {
			transform.position = new Vector3 (transform.position.x, MyConstraintOnY, transform.position.z);
		}
		if (ConstrainOnTopYaxis == true && transform.position.y >= MyConstraintOnY) {
			transform.position = new Vector3 (transform.position.x, MyConstraintOnY, transform.position.z);
		}
	
	}

	//All code below is about the staircases--------------------------------------------------------------------------------

	void OnCollisionEnter (Collision other){
		if (other.gameObject.tag == "StairsL") {
			UpSpeed = ForwardSpeed;
			OnStairs = true;
		}
		if (other.gameObject.tag == "StairsR") {
			UpSpeed = -1*ForwardSpeed;
			OnStairs = true;
		}
	}
	void OnCollisionExit (Collision other){
		if (other.gameObject.tag == "StairsL" || other.gameObject.tag == "StairsR") {
			UpSpeed = 0;
			OnStairs = false;
		}
	}
	void OnTriggerEnter (Collider other){

		if (other.tag == "TOP1") {
			ConstrainOnTopYaxis = true;
			MyConstraintOnY = FirstFloorY;
			print ("Enter");
		}
		if (other.tag == "TOP2") {
			ConstrainOnTopYaxis = true;
			MyConstraintOnY = SecondFloorY;
		}

		if (other.tag == "BOTTOM0") {
			ConstrainOnBottomYaxis = true;
			MyConstraintOnY = BottomFloorY;
			print ("Enter");
		}
		if (other.tag == "BOTTOM1") {
			ConstrainOnBottomYaxis = true;
			MyConstraintOnY = FirstFloorY;
		}
	}
	void OnTriggerExit (Collider other){

		if (other.tag == "TOP1") {
			ConstrainOnTopYaxis = false;
			print ("Exit");
		}
		if (other.tag == "TOP2") {
			ConstrainOnTopYaxis = false;
		}

		if (other.tag == "BOTTOM0") {
			ConstrainOnBottomYaxis = false;
			print ("Exit");
		}
		if (other.tag == "BOTTOM1") {
			ConstrainOnBottomYaxis = false;
		}
	}
}
