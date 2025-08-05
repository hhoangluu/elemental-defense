using UnityEngine;
using UnityEngine.UI;

public class UIBattle : MonoBehaviour
{
    [SerializeField] Button buttonPlaceTower;
    [SerializeField] Button buttonReturnToBattle;
    [SerializeField] TowerPlacerController towerPlacerController;
    void Start()
    {
        buttonPlaceTower.onClick.AddListener(OnButtonPlaceTowerClicked);
        buttonReturnToBattle.onClick.AddListener(OnButtonReturnToBattleClicked);
    }

    void OnButtonPlaceTowerClicked()
    {
        buttonPlaceTower.gameObject.SetActive(false);
        buttonReturnToBattle.gameObject.SetActive(true);
        towerPlacerController.enabled = true;
    }

    void OnButtonReturnToBattleClicked()
    {
        buttonPlaceTower.gameObject.SetActive(true);
        buttonReturnToBattle.gameObject.SetActive(false);
        towerPlacerController.enabled = false;
    }
}
