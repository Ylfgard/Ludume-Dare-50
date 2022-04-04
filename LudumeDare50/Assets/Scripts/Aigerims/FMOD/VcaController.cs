using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VcaController : MonoBehaviour
{
    private FMOD.Studio.VCA _vcaController;
    private Slider _slider;
    public string VcaName;

    private void Awake()
    {
        _vcaController = FMODUnity.RuntimeManager.GetVCA("vca:/" + VcaName);
        _slider = GetComponent<Slider>();
    }
}
