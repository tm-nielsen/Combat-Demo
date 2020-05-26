﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEnemies : MonoBehaviour
{
    public Vector2 hitForce;
    public GameObject burst;

    private Transform pPos;
    private Sword sword;

    void Start()
    {
        pPos = transform.parent.parent;
        pPos = GameObject.Find("Player").transform;
        sword = GetComponentInParent<Sword>();
    }

    [System.Obsolete]
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))// && !sword.contact)
        {
            GameObject enemy = collision.gameObject;
            enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(pPos.localScale.x * hitForce.x, hitForce.y), ForceMode2D.Impulse);
            if (!sword.contact)
            {
                enemy.GetComponent<EnemyBehavior>().GetHit();
                if(burst != null)
                {
                    GameObject burstInst = Instantiate(burst, transform.position, Quaternion.identity);
                    burstInst.transform.localScale = transform.parent.localScale;
                    burstInst.GetComponent<ParticleSystem>().scalingMode = ParticleSystemScalingMode.Shape;
                }
                sword.OnHit();
            }
            sword.contact = true;
        }
    }
}