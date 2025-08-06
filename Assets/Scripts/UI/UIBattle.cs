using UnityEngine;
using UnityEngine.UI;

public class UIBattle : MonoBehaviour
{
    [SerializeField] Button buttonPlaceTower;
    [SerializeField] Button buttonReturnToBattle;
    [SerializeField] TowerPlacerController towerPlacerController;
    [SerializeField] GridVisualizer gridVisualizer;
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
        gridVisualizer.SetEnable(true);
    }

    void OnButtonReturnToBattleClicked()
    {
        buttonPlaceTower.gameObject.SetActive(true);
        buttonReturnToBattle.gameObject.SetActive(false);
        towerPlacerController.enabled = false;
        gridVisualizer.SetEnable(false);
    }
}
