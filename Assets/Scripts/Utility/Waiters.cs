using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Source: https://forum.unity.com/threads/c-coroutine-waitforseconds-garbage-collection-tip.224878/
/// </summary>

public static class Waiters
{

    static Dictionary<float, WaitForSeconds> timeInterval = new Dictionary<float, WaitForSeconds>(100);

    static WaitForEndOfFrame endOfFrame = new WaitForEndOfFrame();

    static WaitForFixedUpdate fixedUpdate = new WaitForFixedUpdate();

    public static WaitForEndOfFrame EndOfFrame
    {
        get
        {
            return endOfFrame;
        }
    }

    public static WaitForFixedUpdate FixedUpdate
    {
        get
        {
            return fixedUpdate;
        }
    }

    public static WaitForSeconds WaitForSeconds(float _seconds)
    {
        if (timeInterval.ContainsKey(_seconds) == false)
        {
            timeInterval.Add(_seconds, new WaitForSeconds(_seconds));
        }
            
        return timeInterval[_seconds];
    }

}
