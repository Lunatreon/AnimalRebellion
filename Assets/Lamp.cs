using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    // Start is called before the first frame update
    enum lampTypes
    {
        normalLamp,
        colorChangeLamp
    }
    [SerializeField]
    bool couldChangeColor = false;

    [SerializeField]
    private Light lightSource;

    [SerializeField]
    private Material lightShader;

    [SerializeField]
    private Color normalLightColor = Color.yellow;

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
    public void changeColor()
    {
        if (couldChangeColor)
        {
            lightShader.SetColor("_LampColor", colorChange[1]);
            lightSource.color = colorChange[1];
        }
    }
}
