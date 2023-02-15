using Features.Inventory;
using Features.Shed.Upgrade;
using GameCarProfile;
using GameCarTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Features.Shed
{
    internal class ShedContext : BaseContexte
    {
        private readonly ResourcePath _viewPath = new ResourcePath("Prefabs/Shed/ShedView");
        private readonly ResourcePath _dataSourcePath = new ResourcePath("Configs/Shed/UpgradeItemConfigDataSource");

        public ShedContext(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            if (placeForUi == null)
                throw new ArgumentNullException(nameof(placeForUi));

            if (profilePlayer == null)
                throw new ArgumentNullException(nameof(profilePlayer));

            CreateController(placeForUi, profilePlayer);
        }


        private ShedController CreateController(Transform placeForUi, ProfilePlayer profilePlayer)
        {
            InventoryContext inventoryContext = CreateInventoryContext(placeForUi, profilePlayer.Inventory);
            UpgradeHandlersRepository shedHandlersRepository = CreateShedRepository();
            ShedView shedView = LoadView(placeForUi);

            return new ShedController(shedView, profilePlayer, shedHandlersRepository);
        }

        private InventoryContext CreateInventoryContext(Transform placeForUi, IInventoryModel inventoryModel)
        {
            var inventoryContext = new InventoryContext(placeForUi, inventoryModel);
            AddContext(inventoryContext);

            return inventoryContext;
        }

        private UpgradeHandlersRepository CreateShedRepository()
        {
            UpgradeItemConfig[] upgradeConfigs = ContentDataSourceLoader.LoadUpgradeItemConfigs(_dataSourcePath);
            var repository = new UpgradeHandlersRepository(upgradeConfigs);
            AddRepository(repository);

            return repository;
        }

        private ShedView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_viewPath);
            GameObject objectView = UnityEngine.Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<ShedView>();
        }
    }
}
