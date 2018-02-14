using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Instantiate (PlayerUnitPrefab);
	}

	public GameObject PlayerUnitPrefab;

	// Update is called once per frame
	void Update () {
		
	}
}
