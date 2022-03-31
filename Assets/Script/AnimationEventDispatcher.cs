using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventDispatcher : MonoBehaviour
{
    public delegate void ShootEvent();
    public ShootEvent ShootStarted;
    public ShootEvent ShootEnded;

    public void ShootStart()
    {
        Debug.Log("ShootStart");
        if (ShootStarted != null) ShootStarted.Invoke();
    }

    public void ShootEnd()
    {
        Debug.Log("ShootEnd");
        if (ShootEnded != null) ShootEnded.Invoke();
    }
}
