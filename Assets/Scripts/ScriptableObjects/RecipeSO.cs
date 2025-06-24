using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RecipeSO", menuName = "Recipe/RecipeSO")]
public class RecipeSO : ScriptableObject {

    public List<KitchenObjectSO> kitchenObjectSOList;
    public string recipeName;

}
