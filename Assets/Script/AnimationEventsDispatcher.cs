using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventsDispatcher : MonoBehaviour
{
    public delegate void ShootEvent();
    public ShootEvent shootEventStart;
    public ShootEvent shootEventEnd;

    public void ShootStart()
    {
        Debug.Log("ShootStart");
        if (shootEventStart != null) shootEventStart.Invoke();
    }

    public void ShootEnd()
    {
        Debug.Log("ShootEnd");
        if (shootEventEnd != null) shootEventEnd.Invoke();
    }
}
