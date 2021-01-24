using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combos : MonoBehaviour
{

    public Animator playerAnim;
    public bool comboPossible;
    public int comboStep;

    public Movement move;

    public GameObject groundCrack;
    public GameObject groundCrackPos;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && move.canMove && move.onGround && !move.rolling && !move.drinking && !move.pushAndDragging && !move.ropping)
        {
            Attack();
   
        }


        if (!move.onGround)
        {
            ComboRest();
        }
    }

    public void Attack()
    {
        if(comboStep == 0)
        {
            playerAnim.Play("AttackA");
            move.attacking = true;
            comboStep = 1;
            return;
        }

        if(comboStep != 0)
        {
            if (comboPossible)
            {
                comboPossible = false;
                comboStep += 1;
            }
        }

        if (comboStep == 2)
        {
            playerAnim.Play("AttackB");
        }

        if (comboStep == 3)
        {
            playerAnim.Play("AttackC");

        }
    }

    public void ComboPossible()
    {
        comboPossible = true;
    }


    public void ComboRest()
    {
        comboPossible = false;
        comboStep = 0;
        move.attacking = false;
    }

    public void GroundCrack()
    {
        Instantiate(groundCrack, groundCrackPos.transform.position, Quaternion.identity);
    }
}
