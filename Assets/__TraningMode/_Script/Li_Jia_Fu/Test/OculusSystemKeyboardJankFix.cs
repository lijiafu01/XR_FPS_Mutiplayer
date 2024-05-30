using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class OculusSystemKeyboardJankFix : MonoBehaviour
{
    private void Start()
    {
        OpenKeyboard();
    }
    public void OpenKeyboard()
    {
        TouchScreenKeyboard.Open("");
    }
}