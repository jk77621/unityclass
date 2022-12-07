using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    public enum collisionType { head, body, arms }
    public collisionType damageType;

    public enum playerType { player, AI }
    public playerType pType;

    public PlayerController playerController;
    public AiController aiController;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        if (aiController == null) aiController = gameObject.transform.root.GetComponent<AiController>();
    }

    public void HIT(float value)
    {
        try
        {
            if (pType == playerType.AI)
            {
                aiController.health -= value;
                if (aiController.health <= 0)
                    aiController.die();
            }
            else
            {
                playerController.health -= value;
                if (playerController.health <= 0)
                    playerController.die();
            }
        }
        catch
        {
            print("playerController is not connected!!!....");
        }
    }
}
