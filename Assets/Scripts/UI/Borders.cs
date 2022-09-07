using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Borders : MonoBehaviour
{
    public Vector3 fullHealthPos;
    public Vector3 minHealthPos;

    private void Update() {
        transform.localPosition = Vector3.Lerp(transform.localPosition, minHealthPos + (fullHealthPos - minHealthPos) * (PlayerHealth.Instance.health / PlayerHealth.Instance.maxHealth), 0.5f);
    }
}
