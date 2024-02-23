using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//allows us to right click and create new gameEvent assets 
[CreateAssetMenu(menuName ="GameEvent")]


/*acts as a channel or radio station which adds & removes event listeners*/
public class GameEvent : ScriptableObject
{
    public List<GameEventListener> listteners = new List<GameEventListener>();


    //different methods to brodcast or raise events
    public void Raise()
    {
        for (int i = 0; i < listteners.Count; i++)
        {
            listteners[i].OnEventRaised();
        }
    }

    //Manage listeners 
    public void RegisterListener(GameEventListener listener)
    {
        if (!listteners.Contains(listener))
        {
            listteners.Add(listener);
        }
        

    }

    public void UnregisterListener(GameEventListener listener)
    {
        if (listteners.Contains(listener))
        {
            listteners.Remove(listener);
        }
       

    }


}
