using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContainerCounter : BaseCounter, IKitchenObjectParent {

    public event EventHandler OnPlayerGrabbedObject;
    [SerializeField] private KitchenObjectSO _kitchenObjectSO;

    public override void Interact(Player player) {
        if (!player.HasKitchenObject()) {
            Transform kitchenObjectTransform = Instantiate(_kitchenObjectSO.prefab);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);

            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }
}
