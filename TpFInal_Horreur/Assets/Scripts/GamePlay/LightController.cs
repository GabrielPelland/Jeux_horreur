/* Adapte de:
 * https://youtu.be/BLfNP4Sc_iA (Slider)
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightController : MonoBehaviour
{
    //VARIABLES
    //Parametres
    float startTimeLight;
    float currentTimeLight;

    //Slider
    public Slider lightSlider;

    //Lumieres
    public Light light;

    float startIntensityLight;
    float secondeIntensityLight;
    float subSecondeIntensityLight;
    float tempsEcoule = 0;


    void Start()
    {
        TimeLightParms();
    }

    //GameManager - Parametres lumieres et temps
    public void TimeLightParms()
    {
        //Envoye du GameManager
        startTimeLight = GM.i.timeLight;

        //Default start time
        currentTimeLight = startTimeLight;

        //Assignation slider
        lightSlider.maxValue = startTimeLight;
        lightSlider.value = startTimeLight;

        //Calcul intensite lumiere
        startIntensityLight = (GM.i.startIntensityLight * GM.i.maxIntensityLight / GM.i.startIntensityLight);

        secondeIntensityLight = GM.i.secondesIntensityLight;
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
        currentTimeLight = currentTimeLight - Time.deltaTime;

        SetLightSlider(currentTimeLight);
        SetLightIntensity();
    }

    //Afficher slider lumiere
    void SetLightSlider(float sliderTimeLight)
    {
        lightSlider.value = sliderTimeLight;
    }

    //Niveau lumiere
    void SetLightIntensity()
    {
        //Avant certain temps
        if (currentTimeLight >= secondeIntensityLight)
        {
            light.intensity = startIntensityLight;
        }
        //Decroissance progressive 
        else if(currentTimeLight < secondeIntensityLight)
        {
            tempsEcoule = tempsEcoule + Time.deltaTime;
            light.intensity = startIntensityLight - (subSecondeIntensityLight * tempsEcoule);
        }
        //Fin du temps
        else if(currentTimeLight == 0)
        {
            light.intensity = 0;
            //FIN DU JEU (TP FINAL)
        }
    }
}
