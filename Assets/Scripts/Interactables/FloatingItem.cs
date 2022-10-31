using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingItem : MonoBehaviour
{
    private float bobTime;
    private bool bobUp = true;
    private void Update()
    {
        if (bobTime < 0.5f && bobUp)
            bobTime += Time.deltaTime;
        else if (bobTime >= 0.5f && bobUp)
            bobUp = false;

        if (bobTime > -0.5f && !bobUp)
            bobTime -= Time.deltaTime;
        else if (bobTime <= -0.5f)
            bobUp = true;

        transform.Translate(new Vector3(0, bobTime, 0) * Time.deltaTime);
        transform.Rotate(new Vector3(0, 90, 0) * Time.deltaTime);
    }
}
