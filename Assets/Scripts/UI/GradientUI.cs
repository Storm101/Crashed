using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
[ExecuteInEditMode]

public class GradientUI : MonoBehaviour {

    [SerializeField] private Gradient _gradient = null;
    [SerializeField] private Image _image = null;
    [SerializeField] private Slider _slider = null;

    private void Awake() {
        _slider = GetComponent<Slider>();
    }

    private void Update() {
        _image.color = _gradient.Evaluate(_slider.value);
    }

}
