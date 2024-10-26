using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Groccerybox : MonoBehaviour
{
    public Animator animbox;
    public TextMeshPro can;
    public TextMeshPro milk;
    public TextMeshPro tp;

    private bool canin = false;
    private bool tpin = false;
    private bool milkin = false;

    private Coroutine canCoroutine;
    private Coroutine tpCoroutine;
    private Coroutine milkCoroutine;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 9)
        {
            if (canCoroutine != null) StopCoroutine(canCoroutine);
            canCoroutine = StartCoroutine(CheckIfStillOverlapping("can"));
        }
        if (other.gameObject.layer == 10)
        {
            if (tpCoroutine != null) StopCoroutine(tpCoroutine);
            tpCoroutine = StartCoroutine(CheckIfStillOverlapping("tp"));
        }
        if (other.gameObject.layer == 11)
        {
            if (milkCoroutine != null) StopCoroutine(milkCoroutine);
            milkCoroutine = StartCoroutine(CheckIfStillOverlapping("milk"));
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.layer == 9)
        {
            if (canCoroutine != null) StopCoroutine(canCoroutine);
            canin = false;
            can.color = Color.white;
        }
        if (other.gameObject.layer == 10)
        {
            if (tpCoroutine != null) StopCoroutine(tpCoroutine);
            tpin = false;
            tp.color = Color.white;
        }
        if (other.gameObject.layer == 11)
        {
            if (milkCoroutine != null) StopCoroutine(milkCoroutine);
            milkin = false;
            milk.color = Color.white;
        }
    }

    private IEnumerator CheckIfStillOverlapping(string itemType)
    {
        yield return new WaitForSeconds(2f); // Wait for 2 seconds

        if (itemType == "can")
        {
            canin = true;
            can.color = Color.green;
        }
        else if (itemType == "tp")
        {
            tpin = true;
            tp.color = Color.green;
        }
        else if (itemType == "milk")
        {
            milkin = true;
            milk.color = Color.green;
        }
    }

    private void Update()
    {
        if (canin && tpin && milkin)
        {
            move();
        }
    }

    public void move()
    {
        animbox.enabled = true;
    }
}
