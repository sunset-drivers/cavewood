using UnityEngine;

public class TriggerEvent : MonoBehaviour
{
    public enum EventTypes
    {
        OnTriggerEnter, 
        OnTriggerStay, 
        OnTriggerExit
    };

    public GameObject EventObject;
    public string EventScript;
    public EventTypes EventType;    
    public string EventName;

    private void OnTriggerEnter2D(Collider2D collider) {
        if(EventType == EventTypes.OnTriggerEnter)
            EventObject.GetComponent(EventScript)
            .GetComponent<hasTrigger>()
            .Trigger(EventName);
    }

    private void OnTriggerStay2D(Collider2D collider) {
        if(EventType == EventTypes.OnTriggerStay)
            EventObject.GetComponent(EventScript)
            .GetComponent<hasTrigger>()
            .Trigger(EventName);
    }

    private void OnTriggerExit2D(Collider2D collider) {
        if(EventType == EventTypes.OnTriggerExit)
            EventObject.GetComponent(EventScript)
            .GetComponent<hasTrigger>()
            .Trigger(EventName);
    }
    
}
