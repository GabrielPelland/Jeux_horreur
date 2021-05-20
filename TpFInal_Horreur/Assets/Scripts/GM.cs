using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class GM : MonoBehaviour
{
	public GameObject player;
	public MonsterFiniteStateMachine monster;

	//FPS Controller
	public float sensX;
	public float sensY;
	public float multiplier;

	//Inventaire
	public int nbSlot;
	public int tailleInventaire = 1000;

	public int nbKeyTrack = 0;
	public TextMeshProUGUI keytext;

    private void Update()
    {
		keytext.text = nbKeyTrack.ToString() + " / 5 Keys";
		
    }

    public Canvas DeadScreen;
	public Camera cam;

	private bool stressed = false;
	public void EndScreen()
    {
		DeadScreen.enabled = true;
		DeadScreen.GetComponent<DeadScreen>().image.DOFade(1, 2.5f);
		DeadScreen.GetComponent<DeadScreen>().image2.DOFade(1, 3.5f).SetDelay(1f);
		StartCoroutine(RestartScene());
    }

	public int fovChange;

	public void SetStress()
    {
		if (!stressed) {
			cam.DOFieldOfView(95, 0.7f);
			stressed = true;
			AudioManager.i.scaryEncounter.Play();
		}

		
    }
	public void DiludeStress()
	{
	cam.DOFieldOfView(60, 2f);
	stressed = false;
		


	}

	IEnumerator RestartScene()
    {
		yield return new WaitForSeconds(4f);
		DeadScreen.GetComponent<DeadScreen>().Restart();

	}

	//Singleton - GameManager
	public static GM i;
	private void Awake()
	{
		// If there is not already an instance of SoundManager, set it to this.
		if (i == null)
		{
			i = this;
		}
		//If an instance already exists, destroy whatever this object is to enforce the singleton.
		else if (i != this)
		{
			Destroy(gameObject);
		}

		//Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
		DontDestroyOnLoad(gameObject);

		sensX = 100f;
		sensY = 100f;
		multiplier = 0.01f;
		nbSlot = 6;


	}
}
