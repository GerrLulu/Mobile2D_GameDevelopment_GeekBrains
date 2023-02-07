using Features.Inventory.Items;
using GameCarTool;
using JetBrains.Annotations;
using System;
using UnityEngine;

namespace Features.Inventory
{
    internal class InventoryController : BaseController
    {
        private readonly ResourcePath _viewPath = new ResourcePath("");
        private readonly ResourcePath _dataSourcePath = new ResourcePath("");

        private readonly InventoryView _inventoryView;
        private readonly IInventoryModel _inventoryModel;
        private readonly ItemsRepository _repository;


        public InventoryController(
            [NotNull] Transform placeForUi,
            [NotNull] IInventoryModel inventoryModel)
        {
            if (placeForUi == null)
                throw new ArgumentNullException(nameof(placeForUi));

            _inventoryModel = inventoryModel ?? throw new ArgumentNullException(nameof(inventoryModel));

            _repository = CreateRepository();
            _inventoryView = LoadView(placeForUi);

            _inventoryView.Display(_repository.Items.Values, OnItemClicked);

            foreach (string itemId in _inventoryModel.EquippedItems)
                _inventoryView.Select(itemId);
        }

        private ItemsRepository CreateRepository()
        {
            ItemConfig[] itemConfigs = ContentDataSourceLoader.LoadItemConfig(_dataSourcePath);
            var repository = new ItemsRepository(itemConfigs);
            AddRepository(repository);

            return repository;
        }

        private InventoryView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_viewPath);
            GameObject objectView = UnityEngine.Object.Instantiate(prefab, placeForUi);
            AddGameObject(objectView);

            return objectView.GetComponent<InventoryView>();
        }
        
        private void OnItemClicked(string itemId)
        {
            bool isEquipped = _inventoryModel.IsEquipped(itemId);

            if (isEquipped)
                UnequipItem(itemId);
            else
                EquipItem(itemId);
        }

        private void EquipItem(string itemId)
        {
            _inventoryView.Select(itemId);
            _inventoryModel.EquipItem(itemId);
        }

        private void UnequipItem(string itemId)
        {
            _inventoryView.UnSelected(itemId);
            _inventoryModel.UnequipItem(itemId);
        }
    }
}