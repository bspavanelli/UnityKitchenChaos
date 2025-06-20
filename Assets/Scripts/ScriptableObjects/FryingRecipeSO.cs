using UnityEngine;

[CreateAssetMenu(fileName = "FryingRecipeSO", menuName = "FryingRecipe/FryingRecipeSO")]
public class FryingRecipeSO : ScriptableObject {
    public KitchenObjectSO input;
    public KitchenObjectSO output;
    public float fryingTimeMax;
}
