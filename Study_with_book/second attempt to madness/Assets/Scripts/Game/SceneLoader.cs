using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadMenu(){
        Application.LoadLevel("MainMenu");
    }

    public void LoadFirstLevel()
    {
        Application.LoadLevel("FirstLevel");
    }

    public void LoadPlayerConstruct()
    {
        Application.LoadLevel("PlayConstructMenu");
    }
}
