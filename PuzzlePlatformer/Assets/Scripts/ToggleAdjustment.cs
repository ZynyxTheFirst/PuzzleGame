using UnityEngine.Rendering.PostProcessing;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ToggleAdjustment : MonoBehaviour
{
    private Volume v;
    private ColorAdjustments ca;
    void Start()
    {
        v = GetComponent<Volume>();
        v.profile.TryGet(out ca);
        SetAdjustments(false);//true or false
    }

    public void SetAdjustments(bool value)
    {
        ca.active = value;
    }
}