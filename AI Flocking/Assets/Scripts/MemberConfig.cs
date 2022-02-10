using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemberConfig : MonoBehaviour
{
    public float maxFOV = 180;
    public float maxAcceleration;
    public float maxVelocity;

    [Header("Wonder variables")] 
    public float wanderJitter;
    public float wanderRadius;
    public float wanderDistance;
    public float wanderPriority;
    
    [Header("Cohesion variables")]
    public float cohesionRadius;
    public float cohesionPriority;

    [Header("Alignment variables")] 
    public float alignmentRadius;
    public float alignmentPriority;
    
    [Header("Separation variables")]
    public float separationRadius;
    public float separationPriority;
    
    [Header("Avoidance variables")]
    public float avoidanceRadius;
    public float avoidancePriority;
}
