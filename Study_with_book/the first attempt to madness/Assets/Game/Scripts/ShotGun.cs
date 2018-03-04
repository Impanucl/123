using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : MonoBehaviour {
    [SerializeField] private GameObject spawnShotPosition;
    [SerializeField] private GameObject bulletPrefab;
    private GameObject _bullet;
    public float intervalGun = 5.0f;

    private bool tapRotationJoystick;

	// Use this for initialization
	void Start () {
        tapRotationJoystick = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0) && tapRotationJoystick)
        {
            _bullet = Instantiate(bulletPrefab) as GameObject;
            _bullet.transform.position = spawnShotPosition.transform.position;
            _bullet.transform.rotation = transform.rotation;
            intervalGun = 5.0f;
        }
    }

    void Awake()
    {
        Messenger<bool>.AddListener(GameEvent.ON_TAP_ROTATION, SetTapJoystick); 
    }

    void OnDestroy()
    {
        Messenger<bool>.RemoveListener(GameEvent.ON_TAP_ROTATION, SetTapJoystick);
    }

    private void SetTapJoystick(bool tap)
    {
        tapRotationJoystick = tap;
    }


}
