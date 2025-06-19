using System;
using UnityEngine;

public class ClearCounter : BaseCounter {

    [SerializeField] private KitchenObjectSO _kitchenObjectSO;

    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            // There is no Kitchen Object in the counter
            if (player.HasKitchenObject()) {
                // Player is carrying something
                player.GetKitchenObject().SetKitchenObjectParent(this);
            } else {
                // Player is not carrying anything
            }
        } else {
            // There is a Kitchen Object in the counter
            if (player.HasKitchenObject()) {
                // Player is carrying something
            } else {
                // Player is not carrying anithing
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}
