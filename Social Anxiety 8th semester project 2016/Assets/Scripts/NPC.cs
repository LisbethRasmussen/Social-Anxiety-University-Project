using UnityEngine;
using System.Collections;

//When putting all the things together, create a public static variable telling whether or not the player is in a "prøverum"

/* WHAT TO KNOW
The script will find the patrolling points by itself. No need to assign them in the Inspector.

Be aware that the automatic navigation Unity creates will make a fluent path, like S shapes. If more square paths are wanted, create more patrolling points,
which can be accessed in a direct line from one to another.
--------------------------------------------------------------------------------------------------
WHAT YOU NEED TO DO
The GameObject must have a navigation agent assigned, which can be done from the Inspector: Add Component-> Navigation -> Nav Mesh Agent
The player object name MUST BE "Player" (case sensitive). If you insist on calling your player something different, change the name in line 67

How many patrolling points there is, needs to be set in the Inspector, where it says "size"
How many stopping points there is, needs to be set in the Inspector, where it says "size"

The stopping points need to be assigned manyally in the Inspector
The player object in the scene needs to be assigned manually in the Inspector
The Staffmembers need to be manually assigned the tag "STAFF" in Inspector (case sensitive), create the tag if it is not there.

The name of the NPC must apply to the patrolling point. Example:
An NPC is named "Violet". The patrollingpoints must therefore be named as follows: "Violet0" , "Violet1" , "Violet2"

In this script the normal speed, chasing speed and wait time (in seconds) can be assigned. Save yourself the trouble of repetition and assign the values here.

*/

public class NPC : MonoBehaviour {

	NavMeshAgent agent;

	private bool StaffMember = false;
	public int floorNumber = 0;
	public Transform[] PatrollingPoints = new Transform[0];
	public Transform[] StopAtThisPoint = new Transform[0];
	private int n = -1; //I have et this to -1 instead of zero, because when running the code, the beginning point will be the second, unless this is set to -1. forloop stuf you know.
	private Transform Player;

	private int ArrayCounter = 0;

	private bool ChasingPlayer = false;
	public float ChasingSpeed = 5f; //You are allowed to twerk this, recall to put an "f" after the number e.g. 3.5f
	public float NormalSpeed = 3.5f; //You are allowed to twerk this, recall to put an "f" after the number e.g. 3.5f

	private bool StopHere = false;
	private bool ResetStoppingPoint = false;
	private float WaitingTime = 5f; //You are allowed to twerk this, recall to put an "f" after the number e.g. 3.5f

	private float counter = 0;

	private float MyPosX = 0;
	private float MyPosY = 0;

	// Use this for initialization
	void Start () {
		if (gameObject.tag == "STAFF") {
			StaffMember = true;
		}

		MyPosY = transform.position.y;

		agent = GetComponent<NavMeshAgent> ();

		Player = GameObject.Find ("Player").transform;

		for (int i = 0; i < PatrollingPoints.Length; i++) {
			PatrollingPoints [i] = GameObject.Find ("Floor" + floorNumber + "_" + i).transform;
		}
		agent.angularSpeed = 0;
	}

	// Update is called once per frame
	void Update () {


		MyPosX = transform.position.x;

		/*if (ChasingPlayer == false && StopHere == false) {
			if (agent.remainingDistance <= 0.1) {
				n++;
				agent.speed = NormalSpeed;
				int randomInt = Random.Range(0, PatrollingPoints.Length);
				ResetStoppingPoint = false;
				if (n >= PatrollingPoints.Length) {
					n = 0;
				}
			}
			agent.SetDestination (PatrollingPoints [n].position);
		}

		if (ArrayCounter <= StopAtThisPoint.Length && StopHere == false) {
			ArrayCounter++;
			if (ArrayCounter == StopAtThisPoint.Length) {
				ArrayCounter = 0;
			}
		}
			
		if (StopAtThisPoint [ArrayCounter].position == PatrollingPoints [n].position && ChasingPlayer == false && ResetStoppingPoint == false){
			StopHere = true;
			counter += 1f * Time.deltaTime;
			if (counter >= WaitingTime) {
				StopHere = false;
				counter = 0;
				ResetStoppingPoint = true;
			}
		}*/

		if (agent.remainingDistance <= 0.5f){
			counter += Time.deltaTime;
			if (counter > WaitingTime){
				counter = 0;
				agent.SetDestination(PatrollingPoints[Random.Range(0, PatrollingPoints.Length)].position);
			}
		}

		if (Mathf.Abs (MyPosY - Player.position.y) <= 0.5 && Mathf.Abs (MyPosX-Player.position.x) <= 8 /*&& Player not in saferoom*/ && StaffMember == true) {
			ChasingPlayer = true;
			agent.speed = ChasingSpeed;
			agent.SetDestination (Player.position);
		}
		if (Mathf.Abs (MyPosX - Player.position.x) >= 8.1f && ChasingPlayer == true) {
			agent.SetDestination (PatrollingPoints [0].position);
			if (agent.remainingDistance <= 0.5) {
				ChasingPlayer = false;
			}
		}

	}
}
