using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GUI;

namespace Player{

	public class ShotGunFire : MonoBehaviour {
		private GameObject bullet;
		[SerializeField] private GameObject spawnPosition;
		[SerializeField] private GameObject bulletPrefab;
		public float damage = 0.0f;
		public float CornerShootGun = 0.0f;
		public float GunFraction = 15;
		private float RealCorner;
		private float RealSpeed;
		public float lifeTime = 3.0f;

		private bool isFire = false;
		[SerializeField] public Joystick rotationJoystick;

		public float speed = 10.0f;
		public float intervalGun = 0.3f;
		private float _intervalGun;

		private float coolingTime = 0.0f;
		private const float _coolingTime = 3.0f;
		public float temperaturePerShoot = 1.0f;

		private Vector2 handlePos;
		private float corner;

		private float minCorner;
		private float maxCorner;

		// Use this for initialization
		void Start () {

		}

		// Update is called once per frame
		void Update () {

			//получение позиции джойстика поворота оружия
			RotationJoystick handlePosition = rotationJoystick.GetComponent<RotationJoystick>();
			handlePos = new Vector2(handlePosition.Xpos, handlePosition.Ypos);

			//интервал между выстрелами
			_intervalGun -= Time.deltaTime;

			//охлаждение оружия
			if (coolingTime > 0.0f) {
				coolingTime -= Time.deltaTime;
			}
		}

		void FixedUpdate()
		{
			//проверка условий перед выстрелом
			if (Input.GetMouseButton (0) && isFire && (_intervalGun < 0.0f) && (coolingTime <= 0.0f))
			{
				spawnBullet ();
			}
		}

		void Awake()
		{
			Messenger<bool>.AddListener(GameEvent.ON_TAP_ROTATION, SetFire);
			Messenger.AddListener(GameEvent.ON_TEMPERATURE_COOLING, SetCoolingTime);
		}

		void OnDestroy()
		{
			Messenger<bool>.RemoveListener(GameEvent.ON_TAP_ROTATION, SetFire);
			Messenger.RemoveListener(GameEvent.ON_TEMPERATURE_COOLING, SetCoolingTime);
		}

		public void SetFire(bool fire){
			isFire = fire;
		}

		private void SetCoolingTime()
		{
			coolingTime = _coolingTime;
		}

		//создание пули и добавление ей импулса
		private void spawnBullet()
		{
			for (float count = 1; count <= GunFraction; count++) {
				updateParametersToFraction();
				bullet = Instantiate (bulletPrefab, spawnPosition.transform.position, transform.rotation) as GameObject; 		 
				bullet.transform.rotation = Quaternion.Euler (0, 0, corner - RealCorner);													    
				bullet.GetComponent<Rigidbody2D> ().AddForce (bullet.transform.up * RealSpeed * Time.deltaTime, ForceMode2D.Impulse);
				SetBulletParameters(damage, lifeTime);
			}
			Messenger<float>.Broadcast(GameEvent.SPAWN_BULLET, temperaturePerShoot);	
			_intervalGun = intervalGun;	

		}

		private void SetBulletParameters(float damagePoint, float lifeTime)
		{
			bullet.GetComponent<BulletDamagePoint>().damage = damagePoint;
			bullet.GetComponent<BulletDamagePoint>().timeLife = lifeTime;
		}

		public void updateParametersToFraction()
		{
			corner = 360 - Mathf.Atan2 (handlePos.x, handlePos.y) * Mathf.Rad2Deg;
			RealCorner = Random.Range(-CornerShootGun, CornerShootGun);
			RealSpeed = Random.Range (speed-(speed/2), speed+(speed/2));
		}

	}
}



