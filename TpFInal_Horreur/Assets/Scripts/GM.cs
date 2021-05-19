using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

	//Light
	public float timeLight = 60f;
    public float startIntensityLight = 100f;
    public float maxIntensityLight = 50f;
    public float secondesIntensityLight = 20f;

	public bool lightOpen = false;
	public Canvas DeadScreen;
	public Camera cam;

	public void EndScreen()
    {
		DeadScreen.enabled = true;
		DeadScreen.GetComponent<DeadScreen>().image.DOFade(1, 2.5f);
		StartCoroutine(RestartScene());
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
