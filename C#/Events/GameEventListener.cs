using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class GameEventListener<T, E, UER> : MonoBehaviour, IGameEventListener<T> where E : GameEvent<T> where UER : UnityEvent<T>
{
    [SerializeField]
    public E gameEvent;
    [SerializeField]
    public UER response;

    private void OnEnable()
    {
        gameEvent.RegisterListener(this);
    }

    private void OnDisable()
    {
        gameEvent.UnregisterListener(this);
    }

    public virtual void OnEventRaised(T item)
    {
        response.Invoke(item);
    }
}