using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Hangar : MonoBehaviour
{
    [SerializeField]
    private UnityEvent _parkEvent, _driveEvent;

    public GameObject MyAirplane { get; set; }
    public int MyNumber { get; set; }

    // Start is called before the first frame update
    private void Start()
    {
        AddListenerToParkEvent();
        AddListenerToDriveEvent();
    }

    private void AddListenerToParkEvent()
    {
        _parkEvent.AddListener(HangarTextParked);
        _parkEvent.AddListener(SetTextAirplaneToEmpty);
        _parkEvent.AddListener(BlinkLights);
    }

    private void AddListenerToDriveEvent()
    {
        _driveEvent.AddListener(SetHangarTextNumber);
        _driveEvent.AddListener(SetAirplaneTextNumber);
        _driveEvent.AddListener(BlinkLights);
    }

    private void SetTextGameObject(GameObject gameObject, string text)
    {
        gameObject.GetComponentInChildren<TextMeshPro>().text = text;
    }

    public void EmptyText()
    {
        GetComponent<TextMeshPro>().text = "";
    }

    private void HangarTextParked()
    {
        SetTextGameObject(this.gameObject, TextStrings._TEXT_PARKED);
    }

    private void SetHangarTextNumber()
    {
        SetTextGameObject(this.gameObject, MyNumber.ToString());
    }

    private void SetTextAirplaneToEmpty()
    {
        SetTextGameObject(MyAirplane, "");
    }
    private void SetAirplaneTextNumber()
    {
        SetTextGameObject(MyAirplane, MyNumber.ToString());
    }

    private void BlinkLights()
    {
        StartCoroutine(MyAirplane.GetComponent<Airplane>().BlinkLights());
    }
    
    private void OnTriggerEnter()
    {
        _parkEvent.Invoke();
    }

    private void OnTriggerExit()
    {
        _driveEvent.Invoke();
    }

}// end class
