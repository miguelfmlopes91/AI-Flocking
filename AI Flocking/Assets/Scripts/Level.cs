using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Level : MonoBehaviour
{
    public Transform memberPrefab;
    public Transform enemyPrefab;
    public int numberOfMembers;
    public int numberOfEnemies;
    public List<Member> members;
    public List<Enemy> enemies;
    public float bounds;
    public float spawnRadius;
    
    
    // Start is called before the first frame update
    void Start()
    {
        members = new List<Member>();
        enemies = new List<Enemy>();
        
        //spawn members
        Spawn(memberPrefab, numberOfMembers);
        Spawn(enemyPrefab, numberOfEnemies);
        
        members.AddRange(FindObjectsOfType<Member>());
        enemies.AddRange(FindObjectsOfType<Enemy>());
    }

    private void Spawn(Transform prefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(prefab,
                new Vector3(
                    Random.Range(-spawnRadius, spawnRadius), 
                    Random.Range(-spawnRadius, spawnRadius),
                    0
                   ), 
                Quaternion.identity);
        }
    }

    public List<Member> GetNeighbours(Member member, float radius)
    {
        var neighboursFound = new List<Member>();
        foreach (var otherMember in members)
        {
            if (otherMember == member)
                continue;
            if (Vector3.Distance(member.position, otherMember.position) <= radius)
            {
                neighboursFound.Add(otherMember);
            }
        }
        return neighboursFound;
    }

}
