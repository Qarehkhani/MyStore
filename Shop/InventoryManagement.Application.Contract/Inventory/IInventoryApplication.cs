using _0_Framework.Application;
using System.Collections.Generic;

namespace InventoryManagement.Application.Contract.Inventory
{
    public interface IInventoryApplication
    {
        OpreatinResult Create(CreateInventory command);
        OpreatinResult Edit(EditInventory command);
        OpreatinResult Increase(IncreaseInventory command);
        OpreatinResult Reduce(ReduceInventory command);
        OpreatinResult Reduce(List<ReduceInventory> command);
        EditInventory GetDetails(long id);
        List<InventoryViewModel> Search(InventorySearchModel searchModel);
        List<InventoryOperationViewModel> GetOperationLog(long inventoryId);
    }
    
}
