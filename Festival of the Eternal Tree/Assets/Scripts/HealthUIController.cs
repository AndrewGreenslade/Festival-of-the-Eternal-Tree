using System.Collections.Generic;
using UnityEngine;

public class HealthUIController : MonoBehaviour
{
    public List<GameObject> healthIcons = new List<GameObject>();
    public GameObject healthIcon;

    /// <summary>
    /// adds a new health icon and parents it to this gameobject
    /// </summary>
    public void AddHealthIcon()
    {
        GameObject icon = Instantiate(healthIcon, transform);
        healthIcons.Add(icon);
    }

    /// <summary>
    /// removes the last health icon in the list
    /// </summary>
    public void MinusHealthIcon()
    {
        int index = healthIcons.Count - 1;
        Destroy(healthIcons[index]);
        healthIcons.RemoveAt(index);
    }
}
