using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemLight : MonoBehaviour
{
    //VARIABLES
    //Parametres
    float startTimeLight;
    float currentTimeLight;

    //Slider
    public Slider lightSlider;

    //Lumieres
    public GameObject lightObject;
    public Light lightControl;

    float startIntensityLight;
    float secondeIntensityLight;
    float subSecondeIntensityLight;
    float tempsEcoule = 0;


    void Start()
    {
        LightReset();
    }

    public void FindLight()
    {
        lightObject = GameObject.Find("Light");
        lightControl = lightObject.GetComponent<Light>();
    }

    //GameManager - Parametres lumieres et temps
    public void LightReset()
    {
        //Envoye du GameManager
        startTimeLight = 60;

        //Default start time
        currentTimeLight = startTimeLight;

        //Assignation slider
        lightSlider.maxValue = startTimeLight;
        lightSlider.value = startTimeLight;

        //Calcul intensite lumiere
        startIntensityLight = 4000;

        secondeIntensityLight = 30;
        subSecondeIntensityLight = startIntensityLight / secondeIntensityLight;
    }

    //Mise a zero niveau lumiere
    public void ResetTimeLight()
    {
        print("RESET LIGHT");
        currentTimeLight = startTimeLight;
    }

    //Sur update
    void Update()
    {
        //Decompte temps
         if (lightControl != null)
        {
            currentTimeLight = currentTimeLight - Time.deltaTime;
            SetLightSlider(currentTimeLight);
            SetLightIntensity();
        }

    }

    //Afficher slider lumiere
    void SetLightSlider(float sliderTimeLight)
    {
        lightSlider.value = sliderTimeLight;
    }

    //Niveau lumiere
    void SetLightIntensity()
    {
        if (lightControl != null)
        {
            //Avant certain temps
            if (currentTimeLight >= secondeIntensityLight)
            {
                lightControl.intensity = startIntensityLight;
            }
            //Decroissance progressive 
            else if (currentTimeLight < secondeIntensityLight)
            {
                tempsEcoule = tempsEcoule + Time.deltaTime;
                lightControl.intensity = startIntensityLight - (subSecondeIntensityLight * tempsEcoule);
            }  
            //Fin du temps
            else if (currentTimeLight == 0)
            {
                lightControl.intensity = 0;
                //FIN DU JEU (TP FINAL)
            }
            
        }
    }
}
