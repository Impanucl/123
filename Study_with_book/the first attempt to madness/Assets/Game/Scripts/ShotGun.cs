using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : MonoBehaviour {
    [SerializeField] private GameObject spawnShotPosition;
    [SerializeField] private GameObject bulletPrefab;

    public float intervalGun = 0.3f;

    private float coolingTime = 0.0f;
    private float _intervalGun;
    private GameObject _bullet;
    private bool tapRotationJoystick;
    private const float _coolingTime = 3.0f;

    // Use this for initialization
    void Start () {
        tapRotationJoystick = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0) && tapRotationJoystick && (_intervalGun < 0.0f) && (coolingTime <= 0.0f))
        {
            _bullet = Instantiate(bulletPrefab) as GameObject;
            _bullet.transform.position = spawnShotPosition.transform.position;
            _bullet.transform.rotation = transform.rotation;
            _intervalGun = intervalGun;
        }
        _intervalGun -= Time.deltaTime;

        if (coolingTime > 0.0f) {
            coolingTime -= Time.deltaTime;
        }
    }

    void Awake()
    {
        Messenger<bool>.AddListener(GameEvent.ON_TAP_ROTATION, SetTapJoystick);
        Messenger.AddListener(GameEvent.ON_TEMPERATURE_COOLING, SetCoolingTime);
    }

    void OnDestroy()
    {
        Messenger<bool>.RemoveListener(GameEvent.ON_TAP_ROTATION, SetTapJoystick);
        Messenger.RemoveListener(GameEvent.ON_TEMPERATURE_COOLING, SetCoolingTime);
    }

    private void SetTapJoystick(bool tap)
    {
        tapRotationJoystick = tap;
    }

    private void SetCoolingTime()
    {
        coolingTime = _coolingTime;
    }


}
