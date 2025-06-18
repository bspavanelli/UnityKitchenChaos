using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent {

    [SerializeField] private Transform _counterTopPoint;

    private KitchenObject kitchenObject;
    public virtual void Interact(Player player) {
        Debug.LogError("Basecounter.Interact();");
    }
    public Transform GetKitchenObjectFollowTransform() {
        return _counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    }

    public void ClearKitchenObject() {
        kitchenObject = null;
    }

    public bool HasKitchenObject() {
        return kitchenObject != null;
    }
}
