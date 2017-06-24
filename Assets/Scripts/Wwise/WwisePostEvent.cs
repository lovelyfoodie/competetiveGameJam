using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WwisePostEvent : ScriptableObject
{
    public bool isEnabled = false;
    public string eventName;

    public void Post(GameObject obj)
    {
        if (isEnabled && !string.IsNullOrEmpty(eventName))
            AkSoundEngine.PostEvent(eventName, obj);
    }
}
