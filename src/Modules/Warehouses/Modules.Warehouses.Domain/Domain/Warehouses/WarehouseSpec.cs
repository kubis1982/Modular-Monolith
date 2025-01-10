namespace ModularMonolith.Modules.Warehouses.Domain.Warehouses
{
    using ModularMonolith.Shared.Kernel.Types;
    using System;
    using System.Linq;

    public class WarehouseSpec : Specification<Warehouse>, ISingleResultSpecification<Warehouse>
    {
        private WarehouseSpec(Action<ISpecificationBuilder<Warehouse>> action)
            => action(Query);

        public static WarehouseSpec ById(WarehouseId warehouseId)
            => new(query => query.Where(n => n.Id == warehouseId));
    }
}
