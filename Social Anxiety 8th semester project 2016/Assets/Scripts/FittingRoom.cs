using UnityEngine;
using System.Collections;

public class FittingRoom : MonoBehaviour {

	public GameObject Player;
	private MainCharScript MCS;

	public Texture OpenCurtain;
	public Texture ClosedCurtain;

	private Renderer MyRendere;
	private bool ThePlayerIsTrigging = false;
	public void SetThePlayerIsTriggering(bool x){ThePlayerIsTrigging= x;}


	// Use this for initialization
	void Start () {
		
		MCS = Player.GetComponent<MainCharScript>();
		MyRendere = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {


		if (!MCS.GetIsHiding()){
			MyRendere.material.mainTexture = OpenCurtain;
		}
		if (MCS.GetIsHiding() && ThePlayerIsTrigging){
			MyRendere.material.mainTexture = ClosedCurtain;
		}
	
	}
}
