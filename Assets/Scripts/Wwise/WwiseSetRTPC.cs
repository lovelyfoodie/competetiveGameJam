using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WwiseSetRTPC : ScriptableObject
{
    public string rtpcName;

    private float _min = float.MaxValue;
    private float _max = float.MinValue;

    public void SetValue(float value)
    {
        if (!string.IsNullOrEmpty(rtpcName))
            AkSoundEngine.SetRTPCValue(rtpcName, value);

        _min = Mathf.Min(_min, value);
        _max = Mathf.Max(_max, value);
    }

    public void SetValue(float value, GameObject go)
    {
        if (!string.IsNullOrEmpty(rtpcName))
            AkSoundEngine.SetRTPCValue(rtpcName, value, go);

        _min = Mathf.Min(_min, value);
        _max = Mathf.Max(_max, value);
    }
}
