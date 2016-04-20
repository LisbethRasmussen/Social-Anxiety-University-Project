using UnityEngine;
using System.Collections;

public class CodedAnimation : MonoBehaviour {

	public Texture[] Drawings;
	public float SecondsOfDisplay = 0;

	private Renderer MyRendere;
	private float counter = 0;
	private int CurrentTexture = 0;

	// Use this for initialization
	void Start () {

		MyRendere = GetComponent<Renderer>();
	
	}
	
	// Update is called once per frame
	void Update () {

		counter += 1 * Time.deltaTime;

		MyRendere.material.mainTexture = Drawings [CurrentTexture];

		if (counter >= SecondsOfDisplay) {
			counter = 0;
			CurrentTexture++;
			if (CurrentTexture >= Drawings.Length) {
				CurrentTexture = 0;
			}
		}
	
	}
}
