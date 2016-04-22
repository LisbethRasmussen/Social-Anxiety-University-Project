using UnityEngine;
using System.Collections;

public class CodedAnimation : MonoBehaviour {

	private MainCharScript MCS;
	public Texture[] Drawings;
	public Texture InvisibleTexture;
	public float SecondsOfDisplay = 0;

	private Renderer MyRendere;
	private float counter = 0;
	private int CurrentTexture = 0;

	private bool IsPlayer = false;

	// Use this for initialization
	void Start () {

		MyRendere = GetComponent<Renderer>();
		if (gameObject.name == "Player"){
			IsPlayer = true;
			MCS = this.GetComponent<MainCharScript>();
		}
	}
	
	// Update is called once per frame
	void Update () {

		counter += 1 * Time.deltaTime;

		if (IsPlayer == false){
			MyRendere.material.mainTexture = Drawings [CurrentTexture];	
		}
		if (IsPlayer == true){
			if (MCS.GetIsHiding() == false){
				MyRendere.material.mainTexture = Drawings [CurrentTexture];
			}
			if (MCS.GetIsHiding() == true){
				MyRendere.material.mainTexture = InvisibleTexture;
			}
		}


		if (counter >= SecondsOfDisplay) {
			counter = 0;
			CurrentTexture++;
			if (CurrentTexture >= Drawings.Length) {
				CurrentTexture = 0;
			}
		}
	
	}
}
