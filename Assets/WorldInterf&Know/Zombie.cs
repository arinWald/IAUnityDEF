using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    public void CallToAllZombies()
    {
        BroadcastMessage("StartFollowPlayer");
    }

}
