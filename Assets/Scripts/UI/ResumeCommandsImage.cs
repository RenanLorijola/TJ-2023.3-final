using UnityEngine;
using UnityEngine.UI;

public class ResumeCommandsImage : MonoBehaviour
{
    public Button inventoryButton;
    public Button inventoryCloseButton;
    private static bool isOpen;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
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
