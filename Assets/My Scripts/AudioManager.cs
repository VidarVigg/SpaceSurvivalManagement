using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioConfig audioConfig = new AudioConfig();
    public static AudioManager instance;

    public enum EventType
    {
        ButtonSound,
        EngineSound,
        ShipIntegrityDamage,
        Warning,
        Alarm,
        PanelAcivated,
        PanelDeactivated,
        NumberFeedback,
        NumberFeedbackBad,
        Evasion,
        CounterMeasuresAutomated,
        DrainOxygen,
        Shields,
        MuteAll,

    }
    private Dictionary<EventType, SoundEventStruct> dictionary = new Dictionary<EventType, SoundEventStruct>();


    [System.Serializable]
    public struct SoundEventStruct
    {
        public EventType type;
        [FMODUnity.EventRef]
        public string sound;
        public FMOD.Studio.EventInstance soundEv;
        public Queue<FMOD.Studio.EventInstance> eventQueue;
        public float duration;

        public void Initialize()
        {
            soundEv = FMODUnity.RuntimeManager.CreateInstance(sound);
            eventQueue = new Queue<FMOD.Studio.EventInstance>();
        }


    }
    private void Awake()
    {

        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        dictionary.Clear();
        for (int i = 0; i < audioConfig.soundEventStructs.Length; i++)
        {
            audioConfig.soundEventStructs[i].Initialize();
            dictionary.Add(audioConfig.soundEventStructs[i].type, audioConfig.soundEventStructs[i]);
        }

    }


    public void PlayOneShot(EventType soundType)
    {
        FMOD.Studio.EventInstance eventInstance = dictionary[soundType].soundEv;
        eventInstance.start();
        dictionary[soundType].eventQueue.Enqueue(eventInstance);
        StartCoroutine(StopOneShot(dictionary[soundType].eventQueue, dictionary[soundType].duration));
    }

    private IEnumerator StopOneShot(Queue<FMOD.Studio.EventInstance> queue, float duration)
    {
        yield return new WaitForSeconds(duration);
        FMOD.Studio.EventInstance eventInstance = queue.Dequeue();
        eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        yield break;
    }
    public void PlayLoop(EventType soundType)
    {
        FMOD.Studio.EventInstance eventInstance = dictionary[soundType].soundEv;
        eventInstance.start();
        dictionary[soundType].eventQueue.Enqueue(eventInstance);

    }
    public void StopLoop(EventType soundType, FMOD.Studio.STOP_MODE stopMode = FMOD.Studio.STOP_MODE.ALLOWFADEOUT)
    {
        FMOD.Studio.EventInstance eventInstance = dictionary[soundType].eventQueue.Dequeue();
        eventInstance.stop(stopMode);
    }
    public void StopLoops(EventType soundType, FMOD.Studio.STOP_MODE stopMode = FMOD.Studio.STOP_MODE.ALLOWFADEOUT)
    {
        while (dictionary[soundType].eventQueue.Count > 0)
        {
            StopLoop(soundType, stopMode);
        }
    }
    public void TryStopLoop(EventType soundType, FMOD.Studio.STOP_MODE stopMode = FMOD.Studio.STOP_MODE.ALLOWFADEOUT)
    {

        try
        {
            FMOD.Studio.EventInstance eventInstance = dictionary[soundType].eventQueue.Dequeue();
            eventInstance.stop(stopMode);
        }
        catch
        {

        }


    }


}
