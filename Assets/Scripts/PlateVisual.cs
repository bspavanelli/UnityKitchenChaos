using UnityEngine;

public class PlateVisual : MonoBehaviour, IInitializable {

    [SerializeField] private PlateKitchenObject plateKitchenObject;

    public void Initialize() {
        plateKitchenObject.OnHasPlateChanged += PlateKitchenObject_OnHasPlateChanged;
    }

    private void PlateKitchenObject_OnHasPlateChanged(object sender, PlateKitchenObject.OnHasPlateChangedEventArgs e) {
        gameObject.SetActive(e.hasPlate);
    }
}
