using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight : MonoBehaviour
{
    [SerializeField] private SphereCollider detectionVolume;
    [SerializeField] private string searchTag;
    [SerializeField] private float fieldOfView = 120f;
    public event Action onPlayerView;
    public Transform Target { get; private set; }
    public bool isTargetSighted { get; private set; }
 
    private void Start()
    {

    }
 
 
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == searchTag)
        {
            Target = other.transform;
        }
    }
 
    private void OnTriggerExit(Collider other)
    {
        if (other.transform == Target)
        {
            Target = null;
        }
    }
 
    private void Update()
    {
        if (Target == null)
        {
            isTargetSighted = false;
            return;
        }
 
        Vector3 vectorToTarget = Target.position - transform.position;
        float targetDeviation = Vector3.Dot(vectorToTarget.normalized, transform.forward);
        float fieldOfViewToRange = Mathf.Abs(((fieldOfView / 2) - 90f) / 90f);
        Ray ray = new Ray(
            transform.position + Vector3.up * 1f,
            Target.position - transform.position
            );
 
        if (vectorToTarget.magnitude < 2f)
        {
            isTargetSighted = true;
        }
       
        if (targetDeviation > fieldOfViewToRange)
        {
            Debug.DrawRay(ray.origin, ray.direction * detectionVolume.radius, Color.red);
            onPlayerView?.Invoke();
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * detectionVolume.radius, Color.white);
            isTargetSighted = false;
        }
    }

}
