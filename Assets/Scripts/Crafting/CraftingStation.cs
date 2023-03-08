using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace OM
{
    public class CraftingStation : MonoBehaviour
    {
        [SerializeField] private Image recipieImage;
        [SerializeField] private List<CraftingRecipeSO> craftingRecipeSOList;
        [SerializeField] private BoxCollider placeItemsArea;
        [SerializeField] private Transform itemSpawnPoint;
        //private bool isItemCrafted = false;

        private CraftingRecipeSO craftingRecipieSO;


        private void Start()
        {
            NextRecipe();
        }

        public void NextRecipe()
        {
            //if(isItemCrafted)
            //{
            //    return;
            //}
            if (craftingRecipieSO == null)
            {
                craftingRecipieSO = craftingRecipeSOList[0];
            }
            else
            {
                int index = craftingRecipeSOList.IndexOf(craftingRecipieSO);
                index = (index + 1) % craftingRecipeSOList.Count;
                craftingRecipieSO = craftingRecipeSOList[index];
            }
            recipieImage.sprite = craftingRecipieSO.craftingSprite;
        }

        public void Craft()
        {
            //isItemCrafted = false;
            Collider[] colliderArray = Physics.OverlapBox(
                transform.position + placeItemsArea.center, placeItemsArea.size,
                placeItemsArea.transform.rotation);

            List<ItemSO> inputItemList = new List<ItemSO>(craftingRecipieSO.inputItemSOList);
            List<GameObject> consumeItemGOList = new List<GameObject>();
            foreach (Collider collider in colliderArray)
            {
                if (collider.TryGetComponent(out ItemSOHolder itemSOHolder))
                {
                    if (inputItemList.Contains(itemSOHolder.ItemSO))
                    {
                        inputItemList.Remove(itemSOHolder.ItemSO);
                        consumeItemGOList.Add(collider.gameObject);
                    }

                }
            }

            if (inputItemList.Count == 0)
            {
                //Craft needed item
                //isItemCrafted = true;
                Debug.Log("Crafted!");
                EvaluateCraftingGoal(craftingRecipieSO.outputItemSO.name);
                Instantiate(craftingRecipieSO.outputItemSO.Prefab, itemSpawnPoint.position, itemSpawnPoint.rotation);


                foreach (GameObject consumeItemGO in consumeItemGOList)
                {
                    Destroy(consumeItemGO);
                }
            }
        }

        public void EvaluateCraftingGoal(string itemName)
        {
            EventManager.Instance.QueueEvent(new CraftingGameEvent(itemName));
        }
    }
}