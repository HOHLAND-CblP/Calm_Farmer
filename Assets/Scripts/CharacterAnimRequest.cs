using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterAnimRequest : MonoBehaviour
{
    public UnityEvent mowIsReady;
    public UnityEvent endMow;
    

    public void MowIsReady()
    {
        mowIsReady.Invoke();
    }

    public void EndMow()
    {
        endMow.Invoke();
    }
}