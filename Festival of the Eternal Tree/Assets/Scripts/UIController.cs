using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    public HealthUIController healthUIController;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
