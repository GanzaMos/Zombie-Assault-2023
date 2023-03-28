using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{

    [SerializeField] int shootPower = 1;
    [SerializeField] float range = 100f;
    Camera _camera;

    [SerializeField] [NotNull] ParticleSystem muzzleFlash;
    EnemyHealth _enemyHealth;
    DestroyObjectPool _destroyObjectPool;

    void Awake()
    {
        _camera = GetComponentInParent<Camera>();
        _destroyObjectPool = FindObjectOfType<DestroyObjectPool>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        PlayMazzleFlash();
        ProcessRaycast();
    }

    void ProcessRaycast()
    {
        RaycastHit _hit;

        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out _hit, range))
        {
            
            _enemyHealth = _hit.transform.GetComponentInParent<EnemyHealth>();
            if (_enemyHealth == null) return;

            CreateHitImpact(_hit);
            _enemyHealth.DecreasingHealth(shootPower);
        }
        else
        {
            return;
        }
    }

    void CreateHitImpact(RaycastHit raycastHit)
    {
        ParticleSystem hitParticles = _enemyHealth.HitParticleEffect;
        ParticleSystem particleInstance = Instantiate(hitParticles, raycastHit.point, Quaternion.LookRotation(raycastHit.normal));
        particleInstance.transform.SetParent(_destroyObjectPool.transform);
    }

    void PlayMazzleFlash()
    {
        muzzleFlash.Play();
    }
}
