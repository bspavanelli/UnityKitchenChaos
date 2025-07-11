using System;
using UnityEngine;

public class ClearCounter : BaseCounter {

    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;

    [Header("Kitchen Objects To Place In Plate")]
    [SerializeField] private ValidKitchenObjectsInPlateListSO validKitchenObjectSOList;

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
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObjectInPlayerHand)) {
                    // Player is holding a Plate
                    if (plateKitchenObjectInPlayerHand.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
                        GetKitchenObject().DestroySelf();
                    } else {
                        if (GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObjectInClearCounter) && plateKitchenObjectInPlayerHand.HasPlate()) {
                            plateKitchenObjectInClearCounter.SetHasPlate(true);
                            plateKitchenObjectInPlayerHand.DestroySelf();
                        }   
                    }
                } else {
                    // Player is not carrying plate but something else
                    if (GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObjectInClearCounter)) {
                        // Counter is holding a Plate
                        if (plateKitchenObjectInClearCounter.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO())) {
                            player.GetKitchenObject().DestroySelf();
                        }
                    } else {
                        // Counter and Player are not holding a Plate
                        if (CanKitchenObjectBePlacedInPlate(player.GetKitchenObject().GetKitchenObjectSO()) && 
                            CanKitchenObjectBePlacedInPlate(GetKitchenObject().GetKitchenObjectSO())) {
                            // Both player and counter kitchen objects can be placed in a plate

                            // Keep kitchen object type in player hand
                            KitchenObjectSO oldPlayerKitchenObjectInHand = player.GetKitchenObject().GetKitchenObjectSO();

                            // Destroy the player kitchen object
                            player.GetKitchenObject().DestroySelf();

                            // Add a plate in players hand
                            KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
                            // Set the plate invisible in plate kitchen object in player's hand
                            PlateKitchenObject playerPlateKitchenObject = player.GetKitchenObject().GetPlateKitchenObject();
                            playerPlateKitchenObject.SetHasPlate(false);

                            // Add Kitchen Object previously in hand in the plate
                            playerPlateKitchenObject.TryAddIngredient(oldPlayerKitchenObjectInHand);

                            // Add Kitchen Object in clear counter in the plate
                            playerPlateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO());

                            // Destroy the counter kitchen object
                            GetKitchenObject().DestroySelf();                            
                        }
                    }
                }
            } else {
                // Player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
    
    private bool CanKitchenObjectBePlacedInPlate(KitchenObjectSO kitchenObjecSOToVerify) {
        foreach (KitchenObjectSO kitchenObjectSO in validKitchenObjectSOList.validKitchenObjectsInPlateList) {
            if (kitchenObjectSO == kitchenObjecSOToVerify) {
                return true;
            }
        }
        return false;
    }
}
