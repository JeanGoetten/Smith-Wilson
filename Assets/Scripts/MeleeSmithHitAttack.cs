using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSmithHitAttack : MonoBehaviour
{
    public bool towerDefenseMode = false;

    public GameObject cam01; 
    public GameObject cam02;

    private Animator anim;

    private AudioSource au; 

    public List<AudioClip> clipList;

    private bool attack; 

    private void Awake()
    {
        anim = GetComponent<Animator>();
        au = GetComponent<AudioSource>();

        cam01.SetActive(true);
        cam02.SetActive(false);


    }

    private void Start()
    {
        attack = false;
    }

    public void HitAttack()
    {
        if (towerDefenseMode)
        {
            cam01.SetActive(false);
            cam02.SetActive(true);
        }
        if (attack)
        {
            anim.SetBool("hit01", true);
            au.PlayOneShot(clipList[0]);
        }
        else
        {
            anim.SetBool("hit01", false);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            attack = true;
            HitAttack();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            attack = false;
            HitAttack();
        }
    }
}
