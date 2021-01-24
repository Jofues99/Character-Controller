using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sit : MonoBehaviour
{

    public int cure = 5;
    public float WaitForNextCure = 1;

    private Movement controller;

    public Animator anim;

    private bool sitting;

    public float minTimeSitting = 1;
    private float timeSitting;
    private Drink drinkScript;


    public bool delay = false;
    public float delayTime = 0.00f;
    public float maxDelayTime = 0.5f;

    void Start()
    {
        drinkScript = GetComponent<Drink>();
        controller = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        controller.sitting = sitting;

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Sitting"))
        {
            sitting = true;
        }
        else
        {
            sitting = false;
        }

        if (sitting)
        {
            timeSitting += 1 * Time.deltaTime;
        }

        if (sitting && Input.anyKey && timeSitting >= minTimeSitting)
        {
            sitting = false;
            anim.SetBool("Sitting", sitting);
            delayTime = 0;
            delay = true;
        }

        if (delay)
        {
            DelayToMove();
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "SittingPoint" && Input.GetKeyDown(KeyCode.T) && !controller.onMovement && !sitting && !controller.rolling && controller.onGround && controller.canMove && !controller.attacking && !controller.drinking && !controller.ropping)
        {
            controller.canMove = false;

            timeSitting = 0.00f;
            anim.SetBool("Sitting", true);
            var sittingPosition = other.gameObject.transform.position;
            var sittingRotation = other.transform.transform.rotation;
            transform.position = new Vector3(sittingPosition.x, transform.position.y, sittingPosition.z);
            this.gameObject.transform.rotation = sittingRotation;
            sitting = true;
            StartCoroutine(heal());
        }
    }


    IEnumerator heal()
    {
        while (sitting && controller.hp < 100)
        {
            controller.hp += cure;
            yield return new WaitForSeconds(WaitForNextCure);
        }
    }

    void DelayToMove()
    {
        delayTime = delayTime + 1 * Time.deltaTime;

        if(delayTime >= maxDelayTime)
        {
            delay = false;
            controller.canMove = true;
        }
    }
}
