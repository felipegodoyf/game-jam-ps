﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private float respawnTime = 30.0f;
    [SerializeField] private GameObject collectParticles;
    private MeshRenderer[] meshRenderers;
    private SpriteRenderer spriteRenderer;
    private Collider[] colliders;
    public bool spawnedOnMap = true;

    void Start ()
    {
        meshRenderers = GetComponentsInChildren<MeshRenderer>();
        colliders = GetComponentsInChildren<Collider>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void Collect ()
    {
        if (collectParticles) Instantiate(collectParticles, transform.position, transform.rotation);

        // Respawn / Destroy
        if (spawnedOnMap)
        {
            StartCoroutine(Respawn());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Respawn()
    {
        foreach (MeshRenderer mr in meshRenderers)
        {
            mr.enabled = false;
        }
        foreach (Collider c in colliders)
        {
            c.enabled = false;
        }
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(respawnTime);
        foreach (MeshRenderer mr in meshRenderers)
        {
            mr.enabled = true;
        }
        foreach (Collider c in colliders)
        {
            c.enabled = true;
        }
        spriteRenderer.enabled = true;
    }
}