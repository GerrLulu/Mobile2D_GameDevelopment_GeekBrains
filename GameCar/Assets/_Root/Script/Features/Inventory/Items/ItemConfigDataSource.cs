using System.Collections.Generic;
using UnityEngine;

namespace Features.Inventory.Items
{
    [CreateAssetMenu(fileName = nameof(ItemConfigDataSource), menuName = "Configs/" + nameof(ItemConfigDataSource))]
    internal sealed class ItemConfigDataSource : ScriptableObject
    {
        [SerializeField] private ItemConfig[] _itemsConfigs;

        public IReadOnlyList<ItemConfig> ItemConfigs => _itemsConfigs;
    }
}