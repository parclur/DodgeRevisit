using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerToControllerAssigner : MonoBehaviour {

    /*
    private List<int> assignedControllers = new List<int>();
    private PlayerPanel[] playerPanels;

    private void Awake()
    {
        playerPanels = FindObjectOfType<PlayerPanel>().OrderBy(t => t.PlayerNumber).ToArray();
    }

    private void Update()
    {
        for (int i = 1; i <= 4; i++)
        {
            if (assignedControllers.Contains(i))
            {
                continue;
            }

            if (Input.GetButton("P" + i + "A"))
            {
                AddPlayerController(i);
                break;
            }
        }
    }

    public Player AddPlayerController(int controller)
    {
        assignedControllers.Add(controller);
        for (int i = 0; i < playerPanels.Length; i++)
        {
            if (playerPanels[i].HasControllersAssigner == false)
            {
                return playerPanels[i].AssignController(controller);
            }
        }

        return null;
    }
    */
}
