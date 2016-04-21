using UnityEngine;
using UnityEngine.UI;	// NEED TO ADD THIS TO MAKE TEXT WORK!!
using System.Collections;
using System.Collections.Generic; // NEED TO ADD THIS TO MAKE LISTS WORK!!

public class MainCharScript : MonoBehaviour {

	// Text variables
	public Transform canvas;
	public Text textElement;
	private float stress = 0;
	private float stressMultiplier = 1.0f;

	// Text variables
	private int numberOfTexts = 50;
	private Text[] text;
	private float[,] textPos;
	private float cercleProcentage = 0.50f;
	private float[] cercleRange = new float[2];
	private float textRotRange = -15.0f;
	private string[] words = {"Hello","Sup!?","Too dark man!"};
	// Text shake effect variables
	private float textShakeFrames = 0.0f;
	private float textShakeTimer = 1.0f;
	public float textShakeTimerValue = 1.0f;
	public float textShakeValue = 2.5f;
	// Text change effect variables
	private float textChangeFrames = 0.0f;
	private float textChangeimer = 2.0f;

	// Fitting room variables
	private bool isHiding = false;
	public bool debugFittingRoomRay = false;
	private Vector3 rayDirection = Vector3.back;	// Or whatever way the ray shall be directed
	private float rayDistance = 1.2f;

	// Use this for initialization
	void Start () {
		// Setting up the text elements
		text = new Text[numberOfTexts];
		textPos = new float[2,numberOfTexts];

		cercleRange[0] = (Screen.width/2) * cercleProcentage;
		cercleRange[1] = (Screen.height/2) * cercleProcentage;

		for (int i = 0; i < text.Length; i++){

			// Limits the text's positions around the player 
			while (textPos[0,i] < cercleRange[0] && textPos[0,i] > -cercleRange[0] && textPos[1,i] < cercleRange[1] && textPos[1,i] > -cercleRange[1]){
				textPos[0,i] = Random.Range(-Screen.width/2, Screen.width/2);
				textPos[1,i] = Random.Range(-Screen.height/2, Screen.height/2);
			}

			Quaternion rotation = Quaternion.identity;
			rotation.eulerAngles = new Vector3(0,0, Random.Range(-textRotRange, textRotRange));
			text[i] = Instantiate(textElement, new Vector3(textPos[0,i], textPos[1,i], 0.0f), rotation) as Text;
			text[i].transform.SetParent(canvas, false);
			text[i].fontSize = 18;
			text[i].fontStyle = FontStyle.Bold;
			text[i].color = Color.black;	// Should be changed to white color when we add the black background
			text[i].text = words[Random.Range(0, words.Length)];
			//text[i].gameObject.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		// To hide in a Fitting room
		RaycastHit hit;
		if (Physics.Raycast(this.transform.position, rayDirection, out hit, rayDistance)){	// Or whatever way the ray shall be directed
			if (Input.GetKeyDown(KeyCode.Space) && hit.collider.gameObject.name == "Fitting room"){	// Could alternativly check for a tag instead
				isHiding = !isHiding;
			} 
		}
		if (debugFittingRoomRay)
			Debug.DrawRay(this.transform.position, rayDirection * rayDistance, Color.blue);	// Just to draw the ray, can only be seen in scene view!

		// Text stuff!
		// Shaking the text to make it more "alive" (need adjustments)
		stress = Mathf.Clamp(stress,0f, (float)numberOfTexts);
		for (int i = 0; i < (int)stress; i++){
			if (!text[i].IsActive()){
				text[i].gameObject.SetActive(true);
			}
		}

		textShakeFrames += Time.deltaTime;
		if (textShakeFrames > textShakeTimer){
			textShakeFrames = 0;
			textShakeTimer = Random.Range(textShakeTimerValue-0.5f, textShakeTimerValue);
			for (int i = 0; i < text.Length; i++){
				if (text[i].IsActive()){
					float xRandom = Random.Range(-textShakeValue, textShakeValue);
					float yRandom = Random.Range(-textShakeValue, textShakeValue);
					text[i].transform.position = canvas.position + new Vector3(textPos[0,i] + xRandom, textPos[1,i] + yRandom, 0.0f);
				}
			}
		}

		// Changing words slowly
		textChangeFrames += Time.deltaTime;
		if (textChangeFrames > textChangeimer){
			textChangeFrames = 0;
			for (int i = 0; i < text.Length; i++){
				if (text[i].IsActive()){
					text[i].text = words[Random.Range(0, words.Length)];
				}
			}
		}
	}

	void OnCollisionStay(Collision other){
		// When hitting a row of cloth
		if (other.collider.gameObject.name == "Cloth"){	// Again, a tag could also be used
			stress += Time.deltaTime * stressMultiplier;
		}
	}

}
