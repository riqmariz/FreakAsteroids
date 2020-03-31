using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class Timers
{
    public static void CreateTicker(GameObject obj, float ttl, float delta, UnityAction act) {
        var timer_object = obj.AddComponent<TickerObject>();
        timer_object.TimeToLive = ttl;
        timer_object.Delay = delta;
        timer_object.Action = act;
        timer_object.DelayCount = delta;
    }

    public static void CreateClock(GameObject obj, float clock_time, UnityAction initial_act, UnityAction end_act)
    {
        var clock_object = obj.AddComponent<ClockObject>();
        clock_object.Duration = clock_time;
        clock_object.StartAction = initial_act;
        clock_object.EndAction = end_act;
    }
}

internal class TickerObject : MonoBehaviour
{
    public float TimeToLive { get; set; }
    public float Delay { get; set; }
    public UnityAction Action { get; set; }
    public float DelayCount { get; set; }

    private float _timeAlive = 0f;

    private void Update()
    {
        if (_timeAlive > TimeToLive)
        {
            Debug.Log("Timer Finished!");
            Destroy(this);
        }

        if (DelayCount >= Delay)
        {
            Action.Invoke();
            DelayCount = 0f;
        }

        DelayCount += Time.deltaTime;
        _timeAlive += Time.deltaTime;
    }
}

internal class ClockObject : MonoBehaviour
{
    public float Duration { get; set; }
    public UnityAction StartAction { get; set; }
    public UnityAction EndAction { get; set; }

    private IEnumerator EndClock() {
        yield return new WaitForSeconds(Duration);
        EndAction.Invoke();
        Destroy(this);
    }

    private void Start()
    {
        if (StartAction != null)
            StartAction.Invoke();

        StartCoroutine(EndClock());
    }
}