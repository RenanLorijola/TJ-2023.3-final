using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    [SerializeField] private float maxInteractDistance = 4f;
    [SerializeField] private Camera playerCamera;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxInteractDistance, LayerMask.GetMask("Interactible")))
        {
            var interactible = hit.transform.gameObject.GetComponent<Interactible>();
            if (interactible != null && interactible.AbleToInteract())
            {
                GameHudManager.Singleton.ShowInteract(true);
                Debug.Log("Interactible in range brahhh");

                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("Interact");
                    interactible.Interact();
                }
            }
            else
            {
                GameHudManager.Singleton.ShowInteract(false);
            }
        }
        else
        {
            GameHudManager.Singleton.ShowInteract(false);
        }
    }
}
