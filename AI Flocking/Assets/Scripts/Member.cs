using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Member : MonoBehaviour
{
    public Vector3 position;
    public Vector3 velocity;
    public Vector3 acceleration;

    public Level level;
    public MemberConfig memConfig;

    private Vector3 wanderTarget;
    
    // Start is called before the first frame update
    void Start()
    {
        level = FindObjectOfType<Level>();
        memConfig = FindObjectOfType<MemberConfig>();

        position = transform.position;
        velocity = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), 0);
    }
    
    private void Update()
    {
        acceleration = Combine();
        acceleration = Vector3.ClampMagnitude(acceleration, memConfig.maxAcceleration);
        velocity = velocity + acceleration * Time.deltaTime;
        velocity = Vector3.ClampMagnitude(velocity, memConfig.maxVelocity);
        position = position + velocity * Time.deltaTime;
        WrapAround(ref position, -level.bounds, level.bounds);
        
        transform.position = position;
    }

    protected Vector3 Wander()
    {
        float jitter = memConfig.wanderJitter * Time.deltaTime;
        wanderTarget += new Vector3(RandomBinomial() * jitter, RandomBinomial() * jitter, 0);
        wanderTarget = wanderTarget.normalized;
        wanderTarget *= memConfig.wanderRadius;

        Vector3 targetInLocalSpace = wanderTarget + new Vector3(0, memConfig.wanderDistance, 0);
        Vector3 targetInWorldSpace = transform.TransformPoint(targetInLocalSpace);
        targetInWorldSpace -= this.position;
        return targetInWorldSpace.normalized;
    }

    private Vector3 Alignment()
    {
        Vector3 alignVector = new Vector3();
        var members = level.GetNeighbours(this, memConfig.alignmentRadius);
        if (members.Count == 0) return alignVector;
        foreach (var member in members)
        {
            if (IsInFOV(member.position))
                alignVector += member.velocity;
        }

        return alignVector.normalized;
    }

    Vector3 Cohesion()
    {
        Vector3 cohesionVector = new Vector3();
        int countMembers = 0;
        var neighbours = level.GetNeighbours(this, memConfig.cohesionRadius);
        if (neighbours.Count == 0)
            return cohesionVector;

        foreach (var member in neighbours)
        {
            if (IsInFOV(member.position))
            {
                cohesionVector += member.position;
                countMembers++;
            }
        }
        if (countMembers == 0)
            return cohesionVector;
        
        cohesionVector /= countMembers;
        cohesionVector -= position;
        cohesionVector = Vector3.Normalize(cohesionVector);
        return cohesionVector;
    }

    virtual protected Vector3 Combine()
    {
        return memConfig.cohesionPriority * Cohesion() 
               + memConfig.wanderPriority * Wander() 
               + memConfig.alignmentPriority * Alignment();
    }

    private void WrapAround(ref Vector3 vector, float min, float max)
    {
        vector.x = WrapAroundFloat(vector.x, min, max);
        vector.y = WrapAroundFloat(vector.y, min, max);
        vector.z = WrapAroundFloat(vector.z, min, max);
    }

    private float WrapAroundFloat(float value, float min, float max)
    {
        if (value > max)
            value = min;
        else if (value < min)
            value = max;

        return value;
    }


    private float RandomBinomial()
    {
        return Random.Range(0f, 1f) - Random.Range(0f, 1f);
    }

    private bool IsInFOV(Vector3 vec)
    {
        return Vector3.Angle(velocity, vec - position) <= memConfig.maxFOV;
    }
}
