using Features.AbilitySystem.Abilities;
using GameCarTool;
using JetBrains.Annotations;
using System;
using UnityEngine;

namespace Features.AbilitySystem
{
    internal class AbilitiesContext : BaseContexte
    {
        private readonly ResourcePath _viewPath = new("Prefabs/Ability/AbilitiesView");
        private readonly ResourcePath _dataSourcePath = new("Configs/Ability/AbilityItemConfigDataSource");


        public AbilitiesContext(
            [NotNull] Transform placeForUi,
            [NotNull] IAbilityActivator abilityActivator)
        {
            if (placeForUi == null) 
                throw new ArgumentNullException(nameof(placeForUi));

            if (abilityActivator == null)
                throw new ArgumentNullException(nameof(abilityActivator));

            CreateController(placeForUi, abilityActivator);
        }


        public AbilitiesController CreateController(Transform placeForUi, IAbilityActivator abilityActivator)
        {
            IAbilitiesView view = LoadView(placeForUi);
            AbilityItemConfig[] itemConfigs = LoadItemConfigs();
            IAbilitiesRepository repository = CreateRepository(itemConfigs);

            var abilitiesController = new AbilitiesController(view, itemConfigs, repository, abilityActivator);
            AddController(abilitiesController);

            return abilitiesController;
        }


        private AbilityItemConfig[] LoadItemConfigs() =>
            ContentDataSourceLoader.LoadItemConfigs(_dataSourcePath);

        private AbilitiesRepository CreateRepository(AbilityItemConfig[] abilityItemConfigs)
        {
            var repository = new AbilitiesRepository(abilityItemConfigs);
            AddRepository(repository);

            return repository;
        }

        private AbilitiesView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_viewPath);
            GameObject objectView = UnityEngine.Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<AbilitiesView>();
        }

    }
}
