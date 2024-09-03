using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public GameObject[] screens; // Referências para diferentes telas

    private void OnEnable()
    {
        GameManager.OnScreenChange += HandleScreenChange;
    }

    private void OnDisable()
    {
        GameManager.OnScreenChange -= HandleScreenChange;
    }

    private void HandleScreenChange(string screenName)
    {
        foreach (var screen in screens)
        {
            screen.SetActive(screen.name == screenName);
        }
    }
}
