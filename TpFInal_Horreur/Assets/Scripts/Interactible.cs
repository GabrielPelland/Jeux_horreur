using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactible : MonoBehaviour
{
    public SoundEvent onSoundEvent = new SoundEvent();

    public void Start()
    {
        Initialize();
    }

    [Button]
    public void OnTriggerSound()
    {
        onSoundEvent.Invoke(transform.position);
    }

    public void Initialize()
    {
        onSoundEvent.AddListener(GM.i.monster.SoundTriggered);
    }

}
