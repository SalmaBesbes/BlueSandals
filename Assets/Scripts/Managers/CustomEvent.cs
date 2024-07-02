using System;
using System.Collections.Generic;
using System.Linq;

public class CustomEvent<T>
{
    public string Handle;

    Dictionary<object, Action<T>> Registrations = new Dictionary<object, Action<T>>();
    Dictionary<object, Action<T>> SingleRegistrations = new Dictionary<object, Action<T>>();


    public CustomEvent(string handle)
    {
        Handle = handle;
    }

    public void Register(object Registree, Action<T> Action)
    {
        if (!Registrations.ContainsKey(Registree))  Registrations.Add(Registree, Action);
    }

    public void RegisterOnce(object Registree, Action<T> Action)
    {
        if (!SingleRegistrations.ContainsKey(Registree)) SingleRegistrations.Add(Registree, Action);
    }

    public void ClearAllRegistration()
    {
        Registrations.Clear();
        SingleRegistrations.Clear();
    }

    public void Call(T Payload)
    {
        GameManager.Instance.TriggerEvent(Handle);
        foreach (var registree in Registrations.Keys.ToList())
        {
            Registrations[registree].Invoke(Payload);
        }
        foreach (var registree in SingleRegistrations.Keys.ToList())
        {
            SingleRegistrations[registree].Invoke(Payload);
            SingleRegistrations.Remove(registree);
        }
    }
}