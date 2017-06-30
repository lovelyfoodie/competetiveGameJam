
using UnityEngine;
using UnityEngine.Events;

public class InputListener : MonoBehaviour {
    public KeyCode key;

    public UnityEvent OnInputEvent;

    void Update()
    {
        if (Input.GetKeyDown(key))
            OnInputEvent.Invoke();
    }

}
