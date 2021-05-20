using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActivateRadio : MonoBehaviour
{
    private AudioSource source;
    public TextMeshProUGUI text;
    public bool canActivateRadio = false;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            text.gameObject.SetActive(true);
            canActivateRadio = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            text.gameObject.SetActive(false);
            canActivateRadio = false;
        }
    }

    public void PlayRadio()
    {
        source.Play();
        StartCoroutine(PlayMonsterScreamDelayed());
    }

    IEnumerator PlayMonsterScreamDelayed()
    {
        yield return new WaitForSeconds(Random.Range(2f, 3f));
        GM.i.monster.PlayScream();
    }

    private void Update()
    {
        if(canActivateRadio)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                GetComponentInParent<Interactible>().OnTriggerSound();
            }
        }
    }
}
