using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

[RequireComponent(typeof(Volume))]
public class vin : MonoBehaviour
{
    private Volume volume;
    private Vignette viginette;

    private float timer;

    public float pulseIntensity = 0.8f;
    public float pulseSpeed = 2;

    private void Start() {
        volume = GetComponent<Volume>();
        volume.profile.TryGet(out viginette);
    }

    private void Update() {
        viginette.intensity.value = 0.5f - (PlayerHealth.Instance.health / PlayerHealth.Instance.maxHealth) *0.5f;

        timer += Time.deltaTime*pulseSpeed;
        viginette.intensity.value += pulseIntensity * Mathf.Sin(timer) * 1-( PlayerHealth.Instance.health / PlayerHealth.Instance.maxHealth );
    }
}
