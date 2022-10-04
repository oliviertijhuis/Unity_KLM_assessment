using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _directionalLight;

    [SerializeField]
    private GameObject _lightsAirport;

    [SerializeField]
    private ParticleSystem _rain;

    [SerializeField]
    private GameObject[] _airplanes;

    [SerializeField]
    private GameObject[] _hangars;

    [SerializeField]
    private GameObject _buttonLightsAirplanes,
        _buttonLightsAirport,
        _buttonPark,
        _buttonDayNight,
        _buttonRain;

    private bool _areLightsOn_Airplanes = false;
    private bool _areLightsOn_Airport = false;
    private bool _isParked = false;

    private DayNight _dayNight;
    private bool _isDayTime = true;

    private void Start()
    {
        _dayNight = _directionalLight.GetComponent<DayNight>();
        _rain.Stop();

        AssignNumbers(_hangars);
        AssignNumbers(_airplanes);
        AssignPlaneToHangar();

        _lightsAirport.SetActive(false);
    }

    private void ChangeButtonText(GameObject button, string OnText, string OffText, bool statement)
    {
        if (statement)
        {
            button.GetComponent<TextMeshProUGUI>().text = OffText;
        }
        else
        {
            button.GetComponent<TextMeshProUGUI>().text = OnText;
        }
    }

    private void AssignNumbers(GameObject[] arrayOfObjects)
    {
        for (int i = 0; i < arrayOfObjects.Length; i++)
        {
            arrayOfObjects[i].GetComponentInChildren<TextMeshPro>().text = (i + 1).ToString();
        }
    }

    private void AssignPlaneToHangar()
    {
        for (int i = 0; i < _airplanes.Length; i++)
        {
            _airplanes[i].GetComponent<Airplane>().MyHangar = _hangars[i].gameObject;
            _hangars[i].GetComponent<Hangar>().MyAirplane = _airplanes[i].gameObject;

            _hangars[i].GetComponent<Hangar>().MyNumber = i + 1;
        }
    }

    private void ToggleButtonTextLigthsOn()
    {
        _areLightsOn_Airplanes = !_areLightsOn_Airplanes;
        ChangeButtonText(_buttonLightsAirplanes, TextStrings._BUTTON_AIRPLANES_LIGHTS_ON_TEXT,
            TextStrings._BUTTON_AIRPLANES_LIGHTS_OFF_TEXT, _areLightsOn_Airplanes);
    }

    public void ToggleButtonTextParkDrive()
    {
        _isParked = !_isParked;
        ChangeButtonText(_buttonPark, TextStrings._BUTTON_PARK_TEXT,
            TextStrings._BUTTON_DRIVE_AROUND_TEXT, _isParked);
    }

    public void ToggleLights_Airplanes()
    {
        foreach (GameObject airplane in _airplanes)
        {
            airplane.GetComponent<Airplane>().ToggleLights();
        }
        ToggleButtonTextLigthsOn();
    }

    public void ToggleLights_Airport()
    {
        _areLightsOn_Airport = !_areLightsOn_Airport;
        if (_areLightsOn_Airport)
        {
            _lightsAirport.SetActive(true);
        }
        else
        {
            _lightsAirport.SetActive(false);
        }
        ChangeButtonText(_buttonLightsAirport, TextStrings._BUTTON_AIRPORT_LIGHTS_OFF_TEXT,
            TextStrings._BUTTON_AIRPORT_LIGHTS_ON_TEXT, _areLightsOn_Airport);
    }

    public void ParkAirplanes()
    {
        if (_isParked)
        {
            foreach (GameObject airplane in _airplanes)
            {
                airplane.GetComponent<Airplane>().GoWanderAround();
            }
        }
        else
        {
            foreach (GameObject airplane in _airplanes)
            {
                airplane.GetComponent<Airplane>().GoToHangar();
            }
        }
        ToggleButtonTextParkDrive();
    }

    public void ToggleNightDay()
    {
        if(_isDayTime)
        {
            _dayNight.ToNightTime();

            ChangeButtonText(_buttonDayNight, TextStrings._BUTTON_NIGHT_TEXT,
                TextStrings._BUTTON_DAY_TEXT, _isDayTime);
        }
        else
        {
            _dayNight.ToDayTime();

            ChangeButtonText(_buttonDayNight, TextStrings._BUTTON_DAY_TEXT,
                TextStrings._BUTTON_NIGHT_TEXT, _isDayTime);
        }

        _isDayTime = !_isDayTime;
    }

    public void ToggleRain()
    {
        if(_rain.isPlaying)
        {
            _rain.Stop();
            ChangeButtonText(_buttonRain, TextStrings._BUTTON_DRY_WEATHER_TEXT,
                TextStrings._BUTTON_RAIN_TEXT, _rain.isPlaying);
        }
        else
        {
            _rain.Play();
            ChangeButtonText(_buttonRain, TextStrings._BUTTON_RAIN_TEXT,
                TextStrings._BUTTON_DRY_WEATHER_TEXT, _rain.isPlaying);
        }
    }

}// end class
