using System.Collections.Generic;

namespace Features.Inventory.Items
{
    internal interface IItemRepository
    {
        IReadOnlyDictionary<string, IItem> Items { get;}
    }


    internal sealed class ItemsRepository : BaseRepository<string, IItem, ItemConfig>, IItemRepository
    {
        public ItemsRepository(IEnumerable<ItemConfig> configs) : base(configs)
        {}

        protected override string GetKey(ItemConfig config) => config.Id;

        protected override IItem CreateItem(ItemConfig config) =>
            new Item(config.Id, new ItemInfo(config.Title, config.Icon));
    }
}
