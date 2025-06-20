using UnityEngine;

[CreateAssetMenu(fileName = "CuttingRecipeSO", menuName = "CuttingRecipe/CuttingRecipeSO")]
public class CuttingRecipeSO : ScriptableObject {
    public KitchenObjectSO input;
    public KitchenObjectSO output;
}
