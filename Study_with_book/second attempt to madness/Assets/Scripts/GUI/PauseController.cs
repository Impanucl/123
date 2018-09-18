using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MYGUI{

	public class PauseController : MonoBehaviour {

		[SerializeField] public Joystick rotationJoystick;
		[SerializeField] public Joystick moveJoystick;
		public bool activeMenu = false;

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
				this.gameObject.SetActive (true);
				rotationJoystick.gameObject.SetActive (false);
				moveJoystick.gameObject.SetActive (false);
			} else 
			{
				Time.timeScale = 1;
				this.gameObject.SetActive (false);
				rotationJoystick.gameObject.SetActive (true);
				moveJoystick.gameObject.SetActive (true);
			}
		}
}
}
