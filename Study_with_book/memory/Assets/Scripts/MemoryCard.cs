using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCard : MonoBehaviour {
    [SerializeField] private GameObject cardBack;
    public void OnMouseDown()
    {
       if (cardBack.activeSelf)
        {
            cardBack.SetActive(false);
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
