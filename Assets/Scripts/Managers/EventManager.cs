using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public void TriggerEvent(string eventName)
    {

        var subscribers = OnEventOccuredRegistrations.Keys.Where(k => k.Item2 == eventName).ToList();
        foreach (var s in subscribers)
        {

            OnEventOccuredRegistrations[s].Invoke();

        }

        var oneTimeSubscribers = OnEventOccuredSingleRegistrations.Keys.Where(k => k.Item2 == eventName).ToList();

        foreach (var s in oneTimeSubscribers)
        {
            OnEventOccuredSingleRegistrations[s].Invoke();
            OnEventOccuredSingleRegistrations.Remove(s);
        }


    }

    public void ClearAllRegistrations()
    {
        OnEventOccuredRegistrations.Clear();
        OnEventOccuredSingleRegistrations.Clear();
    }



    public Dictionary<(UnityEngine.Object, string), Action> OnEventOccuredRegistrations = new Dictionary<(UnityEngine.Object, string), Action>();

    public void RegisterForOnEventOccured((UnityEngine.Object, string) subscriber, Action call)
    {
        if (!OnEventOccuredRegistrations.ContainsKey(subscriber)) OnEventOccuredRegistrations.Add(subscriber, call);
    }

    public void UnRegisterForOnEventOccured((UnityEngine.Object, string) subscriber)
    {
        if (OnEventOccuredRegistrations.ContainsKey(subscriber)) OnEventOccuredRegistrations.Remove(subscriber);
    }


    public Dictionary<(UnityEngine.Object, string), Action> OnEventOccuredSingleRegistrations = new Dictionary<(UnityEngine.Object, string), Action>();

    public void RegisterForSingleOnEventOccured((UnityEngine.Object, string) subscriber, Action call)
    {
        if (!OnEventOccuredSingleRegistrations.ContainsKey(subscriber)) OnEventOccuredSingleRegistrations.Add(subscriber, call);
    }

    public void UnRegisterForSingleOnEventOccured((UnityEngine.Object, string) subscriber)
    {
        if (OnEventOccuredSingleRegistrations.ContainsKey(subscriber)) OnEventOccuredSingleRegistrations.Remove(subscriber);
    }


}