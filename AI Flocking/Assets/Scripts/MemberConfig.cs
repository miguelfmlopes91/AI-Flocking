using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemberConfig : MonoBehaviour
{
    public float maxFOV = 180;
    public float maxAcceleration;
    public float maxVelocity;

    [Header("Wonder variables")] 
    public float wonderJitter;
    public float wonderRadius;
    public float wonderDistqance;
    public float wonderPriority;
    
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
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
