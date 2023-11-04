using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    public Vector3 spawnLimits = new Vector3(5, 0, 5);
    public GameObject zombiePrefab;
    public GameObject[] allZombies;
    public int numOfZombies = 0;

    private void Start()
    {
        allZombies = new GameObject[numOfZombies];

        for (int i = 0; i < numOfZombies; ++i)
        {
            Vector3 pos = this.transform.position + new Vector3(Random.Range(-spawnLimits.x, spawnLimits.x),
                                                        0,
                                                        Random.Range(-spawnLimits.z, spawnLimits.z));

            allZombies[i] = (GameObject)Instantiate(zombiePrefab, pos, Quaternion.identity, this.transform);
        }
    }


    public void CallToAllZombies()
    {
        BroadcastMessage("StartFollowPlayer");
    }

}
