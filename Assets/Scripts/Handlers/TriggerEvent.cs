using System.Threading.Tasks;
using UnityEngine;

public class TriggerEvent : MonoBehaviour, hasTrigger
{
    public enum EventTypes
    {
        OnTriggerEnter, 
        OnTriggerStay, 
        OnTriggerExit,
        OnStart,
        Queue
    };

    public GameObject EventObject;
    public string EventScript;
    public EventTypes EventType;    
    public string EventName;

    private void Start() {
        if(EventType == EventTypes.OnStart)
            EventObject.GetComponent(EventScript)
            .GetComponent<hasTrigger>()
            .Trigger(EventName);
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.CompareTag("Player") && EventType == EventTypes.OnTriggerEnter)
            EventObject.GetComponent(EventScript)
            .GetComponent<hasTrigger>()
            .Trigger(EventName);
    }

    private void OnTriggerStay2D(Collider2D collider) {
        if(collider.gameObject.CompareTag("Player") && EventType == EventTypes.OnTriggerStay)
            EventObject.GetComponent(EventScript)
            .GetComponent<hasTrigger>()
            .Trigger(EventName);
    }

    private void OnTriggerExit2D(Collider2D collider) {
        if(collider.gameObject.CompareTag("Player") && EventType == EventTypes.OnTriggerExit)
            EventObject.GetComponent(EventScript)
            .GetComponent<hasTrigger>()
            .Trigger(EventName);
    }

    public void Trigger(string EventName)
    {
        if(EventType == EventTypes.Queue)
            EventObject.GetComponent(EventScript)
           .GetComponent<hasTrigger>()
           .Trigger(EventName);
    }
}
