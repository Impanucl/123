using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Enemy{

	public class RadiusCollider : MonoBehaviour {

		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
			
		}
			
		void OnTriggerEnter2D(Collider2D other)
		{
			string objectTag = other.tag;

			switch (objectTag) 
			{
			case "TowerPlayer": 
			case "PlayerBase": 
				gameObject.GetComponentInParent<EnemyController> ().Objects.Add (other.gameObject);
				break;
			default: ;
				break;
			}
		}

		void OnTriggerExit2D(Collider2D other)
		{
			string objectTag = other.tag;

			switch (objectTag) 
			{
			case "TowerPlayer": 
			case "PlayerBase": 
				gameObject.GetComponentInParent<EnemyController> ().Objects.Remove (other.gameObject);
				gameObject.GetComponentInParent<EnemyController> ().damageTargetObject = false;
				break;
			default: ;
				break;
			}
		}
	}
}
