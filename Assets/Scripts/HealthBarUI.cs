using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Health target;
    [SerializeField] private Slider slider;
    [SerializeField] private bool autoFindPlayer = false;

    void Start()
    {
        if (slider == null) slider = GetComponent<Slider>();

        if (target == null && autoFindPlayer)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                target = playerObject.GetComponent<Health>();
            }
        }

        if (target != null)
        {
            slider.maxValue = target.MaxHealth;
            slider.value = target.CurrentHealth;
            target.OnHealthChanged += UpdateBar;
        }
    }

    void UpdateBar()
    {
        slider.value = target.CurrentHealth;
    }

    void OnDestroy()
    {
        if (target != null)
        {
            target.OnHealthChanged -= UpdateBar;
        }
    }
}