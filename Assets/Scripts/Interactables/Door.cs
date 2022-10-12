using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Door : MonoBehaviour
{
    public bool locked;
    public GameObject lockIndicator;
    private MeshRenderer lockIndicatorColour;
    private bool up;

    private Animator animator;

    public GameObject Player;

    public Material unlockedMat;
    public Material lockedMat;

    public float doorDistance;

    private void Start()
    {
        animator = GetComponent<Animator>();
        lockIndicatorColour = lockIndicator.GetComponent<MeshRenderer>();
        lockIndicatorColour.material = unlockedMat;
    }

    private void LateUpdate()
    {
        if (locked)
        {
            if (up == true)
            {
                animator.SetBool("OpenDoor", false);
                up = false;
            }
            lockIndicatorColour.material = lockedMat;
        }
        else
        {
            float i = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(Player.transform.position.x, Player.transform.position.z));
            if (!up && i < doorDistance)
            {
                //animator.Play("Base Layer.DoorReverse", 0, -1);
                up = true;
                animator.SetBool("OpenDoor", true);
            }
            else if (up && i > doorDistance + 0.1f)
            {
                up = false;
                //animator.Play("Base Layer.Door", 0, -1);
                animator.SetBool("OpenDoor", false);
            }
            lockIndicatorColour.material = unlockedMat;
        }
    }
}