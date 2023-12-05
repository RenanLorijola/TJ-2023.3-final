using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    public Button inventoryButton;
    public Button inventoryCloseButton;
    private static bool isOpen;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (isOpen)
            {
                if (inventoryCloseButton != null)
                {
                    inventoryCloseButton.onClick.Invoke();
                    isOpen = false;
                }
            }    
            else if (inventoryButton != null)
            {
                inventoryButton.onClick.Invoke();
                isOpen = true;
            }
            else
            {
                Debug.LogWarning("Botão não atribuído ao script!");
            }
        }
    }
}
