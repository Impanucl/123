using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GUI{

	public class PauseController : MonoBehaviour {

		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {

		}

		public void SetTimeScale (bool active)
		{
			if (active)
			{
				Time.timeScale = 0;
			} else 
			{
				Time.timeScale = 1;
			}
		}
}
}
