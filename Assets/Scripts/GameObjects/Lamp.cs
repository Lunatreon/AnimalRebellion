using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * the logic of the lamps (tbh one of the worst scripts in this project because i was to lazy to bring good structure to this xd)
 */
public class Lamp : MonoBehaviour
{
    [Tooltip("toogle if the lamp could change color")]
    [SerializeField]
    bool couldChangeColor = false;

    [Tooltip("the point light of the lamp")]
    [SerializeField]
    private Light lightSource;

    //Shader of the lightmaterial of the lamp model
    private Material lightShader;

    [Tooltip("Color of the normal Lights")]
    [SerializeField]
    static private Color normalLightColor = Color.yellow;

    [Tooltip("Color of the Lights which can switch between two colors")]
    [SerializeField]
    private Color[] colorChange = { Color.red, Color.green };


    public bool debug = false;
    void Start()
    {
        lightShader = GetComponent<Renderer>().material;
        if (couldChangeColor)
        {
            lightShader.SetColor("_LampColor", colorChange[0]);
            lightSource.color = colorChange[0];
        } else
        {
            lightShader.SetColor("_LampColor", normalLightColor);
            lightSource.color = normalLightColor;
        }
    }

    private void Update()
    {
        if (debug)
        {
            changeColor();
        }
    }
    /*
     * if the lamp could change color change it to the second one (was to lazy to make it more dynamic) Sorry :/
     */
    public void changeColor()
    {
        if (couldChangeColor)
        {
            lightShader.SetColor("_LampColor", colorChange[1]);
            lightSource.color = colorChange[1];
        }
    }
}
