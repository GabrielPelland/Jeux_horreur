using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RandomCuesGenerator : MonoBehaviour
{
    public AudioSource source;
    public List<AudioClip>clips;

    private int index = 0;
    public float interval1 = 12;
    public float interval2 = 30;
    private void Start()
    {
        ShuffleClips();
        StartCoroutine(RandomCues());
        
    }

    public IEnumerator RandomCues()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(interval1, interval2));
            source.clip = clips[index];
            source.Play();
            index++;
            if (index > clips.Count-1)
            {
                print("oh");
                ShuffleClips();
                index = 0;
            }
        }
    }

    public void ShuffleClips()
    {
        for (int i = 0; i < clips.Count; i++)
        {
            AudioClip temp = clips[i];
            int randomIndex = Random.Range(i, clips.Count);
            clips[i] = clips[randomIndex];
            clips[randomIndex] = temp;
        }
    }

    



}
