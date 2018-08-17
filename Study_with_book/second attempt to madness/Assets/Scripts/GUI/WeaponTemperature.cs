using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GUI{

public class WeaponTemperature : MonoBehaviour {
    [SerializeField] public GameObject cursor;
    private float _temperature = 0.0f;
    public float maxTemperature = -260.0f;
    public float minTemperature = 0.0f;
    public float cooling = 1.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
		{
			if ((_temperature > maxTemperature) && (_temperature < minTemperature))
			{
				cursor.transform.rotation = Quaternion.Euler(0, 0, _temperature);
				_temperature += cooling * Time.deltaTime;
			} else if (_temperature < maxTemperature)
			{
				_temperature += cooling * Time.deltaTime;
				Messenger.Broadcast(GameEvent.ON_TEMPERATURE_COOLING);
			}
    }

    void Awake()
    {
        Messenger<float>.AddListener(GameEvent.SPAWN_BULLET, SetTemperature);
    }

    void OnDestroy()
    {
        Messenger<float>.RemoveListener(GameEvent.SPAWN_BULLET, SetTemperature);
    }

    private void SetTemperature(float temperature)
    {
        _temperature -= temperature;
    }
}
}
