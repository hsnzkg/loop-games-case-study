using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoystickSetterExample : MonoBehaviour
{
    public VariableJoystick variableJoystick;
    public Text valueText;
    public Image background;
    public Sprite[] axisSprites;

    public void ModeChanged(int index)
    {
        switch(index)
        {
            case 0:
                variableJoystick.SetMode(JoystickType.Fixed);
                break;
            case 1:
                variableJoystick.SetMode(JoystickType.Floating);
                break;
            case 2:
                variableJoystick.SetMode(JoystickType.Dynamic);
                break;
            default:
                break;
        }     
    }

    public void AxisChanged(int index)
    {
        switch (index)
        {
            case 0:
                variableJoystick.SetAxisOptions(AxisOptions.Both);
                background.sprite = axisSprites[index];
                break;
            case 1:
                variableJoystick.SetAxisOptions(AxisOptions.Horizontal);
                background.sprite = axisSprites[index];
                break;
            case 2:
                variableJoystick.SetAxisOptions(AxisOptions.Vertical);
                background.sprite = axisSprites[index];
                break;
            default:
                break;
        }
    }

    public void SnapX(bool value)
    {
        variableJoystick.SetSnapX(value);
    }

    public void SnapY(bool value)
    {
        variableJoystick.SetSnapY(value);
    }

    private void Update()
    {
        valueText.text = "Current Value: " + variableJoystick.GetDirection();
    }
}