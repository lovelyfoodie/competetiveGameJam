using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WwiseSetRTPC : ScriptableObject
{
    public string rtpcName;

    public void SetValue(float value)
    {
        if (!string.IsNullOrEmpty(rtpcName))
            AkSoundEngine.SetRTPCValue(rtpcName, value);
    }
}
