using UnityEngine;
using UnityEngine.UI;

public class LifebarUI : MonoBehaviour
{
    [SerializeField] private Image lifebar;
    private Transform _cameraTransform;

    private void Start()
    {
        _cameraTransform = Camera.main?.transform;
    }

    private void Update()
    {
        transform.LookAt(_cameraTransform);
    }

    public void UpdateLifeBar(BaseHealth character)
    {
        var currentLife = character.CurrentLife;
        var maxLife = character.MaxLife;
        var lifePercent = Mathf.Clamp(currentLife / maxLife, 0, 1);
        lifebar.fillAmount = lifePercent;
    }
}
