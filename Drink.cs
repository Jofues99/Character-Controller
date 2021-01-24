using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drink : MonoBehaviour
{
    private Movement controller;

    public Animator anim;

    void Start()
    {
        controller = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && controller.onGround && !controller.drinking && controller.canMove && !controller.rolling && !controller.attacking && !controller.pushAndDragging && !controller.ropping)
        {
            controller.drinking = true;
            anim.SetBool("Drinking", controller.drinking);
            //controller.canMove = false;
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Drinking") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.90f)
        {
            controller.drinking = false;
            anim.SetBool("Drinking", controller.drinking);
            //controller.canMove = true;
        }
    }
}
