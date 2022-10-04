using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class Airplane : MonoBehaviour
{
    public readonly string _airplaneType = "Cesna 172";
    public readonly string _airplaneBrand = "KLM";

    private NavMeshAgent _navMashAgent;

    private TextMeshPro _textMeshPro;

    [SerializeField]
    private List<Light> _lights;
    private readonly int _amountBlinkLights = 2;

    //wander behaviour variables
    private readonly float _wanderRadius = 50;
    private readonly float _wanderTimerLimit_Low = 1.75f;
    private readonly float _wanderTimerLimit_high = 3f;

    private float _timer = 0;
    private bool _wanderBehaviourActive = true;

    public GameObject MyHangar { get; set; }

    private void Start()
    {
        _navMashAgent = GetComponent<NavMeshAgent>();
        _lights = new List<Light>();
        _timer = GetRandomWanderTime();
        GetAllLights();

        _textMeshPro = GetComponentInChildren<TextMeshPro>();
    }

    private void Update()
    {
        if(_wanderBehaviourActive)
        {
            WanderBehaviour();
        }
    }

    private void GetAllLights()
    {
        foreach(Transform child in this.transform)
        {
            if(child.CompareTag("Light"))
            {
                _lights.Add(child.gameObject.GetComponent<Light>());
            }
        }
    }

    /*
     * Finds a new random position to move to a after a random interval
     */
    private void WanderBehaviour()
    {
        _timer += Time.deltaTime;

        if (_timer >= GetRandomWanderTime())
        {
            Vector3 newPos = RandomNavSphere(transform.position, _wanderRadius, 1);
            MoveTo(newPos);
            _timer = 0;
        }
    }

    private float GetRandomWanderTime()
    {
        return Random.Range(_wanderTimerLimit_Low, _wanderTimerLimit_high);
    }

    /*
     * Parameters: 
     * - Origin: is the current position
     * - dist: is the distance in of the sphere where the new position
     *      will be searched in
     * - layermask: specifies which layer to use.
     * 
     * The function gets a randompostion within a sphere
     * Then gets the closets position to it on navmash
     * Return that position
     */
    private Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    private void OnMouseOver()
    {
        ShowAirplaneDetails();
    }

    private void OnMouseExit()
    {
        ShowAirplaneNumber();
    }
    private void ShowAirplaneDetails()
    {
        _textMeshPro.text = $"{_airplaneType} FROM {_airplaneBrand}";
    }

    private void ShowAirplaneNumber()
    {
        _textMeshPro.text = MyHangar.GetComponent<Hangar>().MyNumber.ToString();
    }

    public void GoWanderAround()
    {
        _wanderBehaviourActive = true;
    }

    public void GoToHangar()
    {
        _wanderBehaviourActive = false;
        MoveTo(MyHangar.transform.position);
    }

    public void MoveTo(Vector3 targetDestination)
    {
        _navMashAgent.SetDestination(targetDestination);
    }

    public void ToggleLights()
    {
        foreach(Light light in _lights)
        {
            light.enabled = !light.enabled;
        }
    }

    public void TurnLightsOff()
    {
        foreach(Light light in _lights)
        {
            light.enabled = false;
        }
    }

    public void EmptyTextMeshPro()
    {
        _textMeshPro.text = "";
    }

    public IEnumerator BlinkLights()
    {
        for (int i = 0; i < _amountBlinkLights; i++)
        {
            ToggleLights();
            yield return new WaitForSeconds(0.05f);
        }
    }

}// end class
