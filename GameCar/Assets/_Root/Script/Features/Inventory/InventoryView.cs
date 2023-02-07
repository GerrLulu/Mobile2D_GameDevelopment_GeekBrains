using Features.Inventory.Items;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Features.Inventory
{
    internal interface IInventoryView
    {
        public void Display(IEnumerable<IItem> itemsCollection, Action<string> itemClicked);
        public void Clear();
        public void Select(string id);
        public void UnSelected(string id);
    }


    internal sealed class InventoryView : MonoBehaviour, IInventoryView
    {
        [SerializeField] private GameObject _itemViewPrefab;
        [SerializeField] private Transform _plaseForItems;

        private readonly Dictionary<string, ItemView> _itemViews = new();

        public void Display(IEnumerable<IItem> itemsCollection, Action <string> itemClicked)
        {
            Clear();

            foreach (IItem item in itemsCollection)
                _itemViews[item.Id] = CreateItemView(item, itemClicked);
        }

        public void Clear()
        {
            foreach(ItemView itemView in _itemViews.Values)
                DestroyItemView(itemView);

            _itemViews.Clear();
        }

        public void Select(string id) => _itemViews[id].Select();

        public void UnSelected(string id) => _itemViews[id].Unselect();


        private ItemView CreateItemView(IItem item, Action<string> itemClicked)
        {
            GameObject objectView = Instantiate(_itemViewPrefab, _plaseForItems);
            ItemView itemView = objectView.GetComponent<ItemView>();

            itemView.Init(item, () => itemClicked?.Invoke(item.Id));

            return itemView;
        }
        
        private void DestroyItemView(ItemView itemView)
        {
            itemView.Deinit();
            Destroy(itemView.gameObject);
        }
    }
}