using Features.Inventory.Items;
using JetBrains.Annotations;
using System;

namespace Features.Inventory
{
    internal class InventoryController : BaseController
    {
        private readonly IInventoryView _inventoryView;
        private readonly IInventoryModel _inventoryModel;
        private readonly IItemRepository _repository;


        public InventoryController(
            [NotNull] IInventoryView view,
            [NotNull] IItemRepository repository,
            [NotNull] IInventoryModel inventoryModel)
        {
            _inventoryView = view ?? throw new ArgumentNullException(nameof(view));

            _repository = repository ?? throw new ArgumentNullException(nameof(repository));

            _inventoryModel = inventoryModel ?? throw new ArgumentNullException(nameof(inventoryModel));


            _inventoryView.Display(_repository.Items.Values, OnItemClicked);

            foreach (string itemId in _inventoryModel.EquippedItems)
                _inventoryView.Select(itemId);
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