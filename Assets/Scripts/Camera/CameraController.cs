using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CameraController : MonoBehaviour {
    [TextArea]
    [SerializeField]
    [Tooltip("ReadMe")]
    private string ReadMe = "Requires an Audio Source to work\nApply this script to the main camera, and should be child of a gameobject holding the 'PlayerController' script.";

    [SerializeField]
    [Tooltip("Sensitivity of the mouse camera movements. Recommended around 2")]
    private float MouseSensitivity;

    [Header("- Footsteps -")]
    [SerializeField]
    [Tooltip("Enable footsteps while walking?")]
    private bool Footsteps = false;
    [SerializeField]
    [Tooltip("The audio to play when the player has taken a step")]
    private AudioClip[] audioClips;

    [Header("- Head Bob -")]
    [SerializeField]
    [Tooltip("Enable head bobbing while walking?")]
    private bool headBob = false;
    [SerializeField]
    [Range(-1.5f, 0)]
    [Tooltip("The maximum distance the head may bob downwards. Recommended at around -0.95")]
    private float MinHeadBob;
    [SerializeField]
    [Tooltip("The maximum distance the head may bob upwards. Recommended at around 0.9")]
    [Range(0, 1.5f)]
    private float MaxHeadBob;
    [SerializeField]
    [Tooltip("Total intensisty of the head bob. Recommended at around 0.1")]
    [Range(0, 0.5f)]
    private float HeadBobIntensity;

    private AudioSource audioSource;
    private bool PlayAgain = false;
    private float StartPos;
    private float HeadBobTimer;
    public float MouseYSum;

    private float yRotation;

    //Weapon rotation variable
    public Transform weaponRotation;

    private void Awake() {
        int i = ReadMe.Length;
        StartPos = transform.localPosition.y;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        
        float MouseX = (Input.GetAxis("Joystick Horizontal") + Input.GetAxis("Mouse X")) * MouseSensitivity * Time.timeScale;
        float MouseY = (Input.GetAxis("Joystick Vertical") + Input.GetAxis("Mouse Y")) * MouseSensitivity * Time.timeScale;
        if (MouseY + MouseYSum >= 75) {
            transform.localEulerAngles = new Vector3(-75, transform.localEulerAngles.y, transform.localEulerAngles.z);
            MouseY = 0;
            MouseYSum = 75;
        }
        else if (MouseY + MouseYSum <= -75) {
            transform.localEulerAngles = new Vector3(75, transform.localEulerAngles.y, transform.localEulerAngles.z);
            MouseY = 0;
            MouseYSum = -75;
        }
        else if (Cursor.lockState == CursorLockMode.Locked) {
            MouseYSum += MouseY;
        }

        if (Cursor.lockState == CursorLockMode.Locked) {
            transform.parent.Rotate(Vector3.up, MouseX, Space.World);
            transform.Rotate(transform.parent.right, -MouseY, Space.World);
        }

        if ((Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0) && GameManager.Instance.Grounded) {
            if (Input.GetKey(KeyCode.LeftShift)) {
                HeadBob(15);
            }
            else {
                HeadBob(10);
            }
        }

        //Get the yRotation and clamp it
        yRotation -= MouseY;
        yRotation = Mathf.Clamp(yRotation, -75, 75);
    }

    private void HeadBob(float Speed) {
        HeadBobTimer += Time.deltaTime * Speed;

        if (headBob) { 
            transform.localPosition = new Vector3(transform.localPosition.x, StartPos + Mathf.Sin(HeadBobTimer) * HeadBobIntensity, transform.localPosition.z);
        }

        if (Footsteps) {
            if (Mathf.Sin(HeadBobTimer) < MinHeadBob && PlayAgain) {
                audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
                audioSource.Play();
                PlayAgain = false;
            }
            else if (Mathf.Sin(HeadBobTimer) > MaxHeadBob && !PlayAgain) {
                PlayAgain = true;
            }
        }
    }


    //Functions for options menu

    public void UpdateSensitivity(float sensivity) {
        MouseSensitivity = sensivity;
    }

    public void HeadBobSwitch(bool toggle) {
        headBob = toggle;
    }

    public void UpdateHeadBobIntensity(float intensity) {
        HeadBobIntensity = intensity;
    }
}