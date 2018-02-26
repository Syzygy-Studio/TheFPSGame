﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MenuHumanAnimation : MonoBehaviour
{
    public Animator Animator;


    #region api

    #region The Button named Equip
    public void Equip()
    {
        Animator.SetBool("isStand_Equip", true);
        Animator.gameObject.transform.DOLocalMove(new Vector3(0, 0.4f, 0), 0.1f);
    }
    public void Equip_Back() => Animator.SetBool("isStand_Equip", false);
    public void Equip_Color() => Animator.SetBool("isStand_Color", true);
    public void Equip_Color_Back()
    {
        Animator.SetBool("isStand_Color", false);
        Animator.gameObject.transform.DOLocalMove(new Vector3(0, 0.4f, 0), 0.1f);
    }
    public void Equip_Weapon() => Animator.SetBool("isStand_Gun", true);
    public void Equip_Weapon_Back()
    {
        Animator.SetBool("isStand_Gun", false);
        Animator.gameObject.transform.DOLocalMove(new Vector3(0, 0.4f, 0), 0.1f);
    }
    public void Equip_Equip() => Animator.SetBool("isStand_Equip_Equip", true);
    public void Equip_Equip_Back()
    {
        Animator.SetBool("isStand_Equip_Equip", false);
        Animator.gameObject.transform.DOLocalMove(new Vector3(0, 0.4f, 0), 0.1f);
    }
    #endregion

    #region The Button named Course
    public void Course() => Animator.SetBool("isReady", true);
    #endregion

    public void Play() => Animator.SetBool("isReady", true);

    public void About() => Animator.SetBool("isStand_About", true);
    public void About_Back() => Animator.SetBool("isStand_About", false);

    #endregion
}
