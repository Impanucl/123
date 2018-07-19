using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GUI;

namespace Player{

public class Fire : MonoBehaviour {
		 private GameObject bullet;
		[SerializeField] private GameObject spawnPosition;
		[SerializeField] private GameObject bulletPrefab;

		private bool isFire = false;
		[SerializeField] public Joystick rotationJoystick;


		public float speed = 10.0f;
		public float intervalGun = 0.3f;
		private float _intervalGun;

		private Vector2 handlePos;
		private float corner;
		private Vector2 gunTarget;

	// Use this for initialization
		void Start () {

		}
	
	// Update is called once per frame
		void Update () {
			RotationJoystick handlePosition = rotationJoystick.GetComponent<RotationJoystick>();
			handlePos = new Vector2(handlePosition.Xpos, handlePosition.Ypos);
			gunTarget =new Vector2 (handlePos.x - spawnPosition.transform.position.x, handlePos.y - spawnPosition.transform.position.y) ;

			if (Input.GetMouseButton (0) && isFire && (_intervalGun < 0.0f))
			{
				spawnBullet ();
			}
			_intervalGun -= Time.deltaTime;

			corner = 90 - Mathf.Atan2 (handlePos.x, handlePos.y) * Mathf.Rad2Deg;
		}

		void Awake()
		{
			Messenger<bool>.AddListener(GameEvent.ON_TAP_ROTATION, SetFire);
		}

		void OnDestroy()
		{
			Messenger<bool>.RemoveListener(GameEvent.ON_TAP_ROTATION, SetFire);
		}

		public void SetFire(bool fire){
			isFire = fire;
		}

		private void spawnBullet()
		{
			bullet = Instantiate(bulletPrefab, spawnPosition.transform.position, transform.rotation) as GameObject;
			bullet.transform.rotation = Quaternion.Euler(0,0,corner);
			bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * speed * Time.deltaTime,ForceMode2D.Impulse);
			_intervalGun = intervalGun;
		}
			
}
}
