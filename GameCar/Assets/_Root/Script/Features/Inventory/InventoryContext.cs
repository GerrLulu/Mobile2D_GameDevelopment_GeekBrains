using Features.Inventory.Items;
using GameCarTool;
using JetBrains.Annotations;
using System;
using UnityEngine;

namespace Features.Inventory
{
    internal class InventoryContext : BaseContexte
    {
        private readonly ResourcePath _viewPath = new ResourcePath("Prefabs/Inventory/InventoryView");
        private readonly ResourcePath _dataSourcePath = new ResourcePath("Configs/Inventory/ItemConfigDataSource");

        public InventoryContext([NotNull] Transform placeForUi, [NotNull]IInventoryModel model)
        {
            if (placeForUi = null) throw new ArgumentNullException(nameof(placeForUi));
            if (model == null) throw new ArgumentNullException(nameof(model));

            CreateController(placeForUi, model);
        }


        private InventoryController CreateController(Transform placeForUi, IInventoryModel model)
        {
            IInventoryView view = LoadView(placeForUi);
            IItemRepository repository = CreateRepository();

            var inventoryController = new InventoryController(view, repository, model);
            AddController(inventoryController);

            return inventoryController;
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
    }
}
