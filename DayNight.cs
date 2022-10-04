using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNight : MonoBehaviour
{
    [SerializeField]
    private AnimationClip _fromDaytoNightAnimation, _fromNightToDayAnimation;
    private Animation _animation;

    private void Start()
    {
        _animation = GetComponent<Animation>();
    }

    public void ToNightTime()
    {
        _animation.clip = _fromDaytoNightAnimation;
        _animation.Play();
    }

    public void ToDayTime()
    {
        _animation.clip = _fromNightToDayAnimation;
        _animation.Play();
    }
}
