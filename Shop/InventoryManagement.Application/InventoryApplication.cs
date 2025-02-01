using _0_Framework.Application;
using InventoryManagement.Application.Contract.Inventory;
using InventoryManagement.Domain.InventoryAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application
{
    public class InventoryApplication : IInventoryApplication
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryApplication(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository=inventoryRepository;
        }

        public OpreatinResult Create(CreateInventory command)
        {
            var opration = new OpreatinResult();
            if (_inventoryRepository.Exists(x=>x.ProductId==command.ProductId))
              return opration.Faild(ApplicationMessages.DuplicatedRecord);

            var inventory = new Inventory(command.ProductId, command.UnitPrice);
            _inventoryRepository.Create(inventory);
            _inventoryRepository.SaveChanges();

            return opration.Succedded();
        }

        public OpreatinResult Edit(EditInventory command)
        {
            var opration = new OpreatinResult();
            var inventory = _inventoryRepository.Get(command.Id);
            if (inventory==null)
                return opration.Faild(ApplicationMessages.RecordNotFound);
            if (_inventoryRepository.Exists(x => x.ProductId==command.ProductId && x.Id !=command.Id))
                return opration.Faild(ApplicationMessages.DuplicatedRecord);

            inventory.Edit(command.ProductId, command.UnitPrice);
            _inventoryRepository.SaveChanges();

            return opration.Succedded();
        }

        public EditInventory GetDetails(long id)
        {
          return _inventoryRepository.GetDetails(id);
        }

        public List<InventoryOperationViewModel> GetOperationLog(long inventoryId)
        {
            return _inventoryRepository.GetOperationLog(inventoryId);
        }

        public OpreatinResult Increase(IncreaseInventory command)
        {
            var opration = new OpreatinResult();
            var inventory = _inventoryRepository.Get(command.InventoryId);
            if (inventory==null)
                return opration.Faild(ApplicationMessages.RecordNotFound);

            const long operatorId = 1;
            inventory.Increase(command.Count, operatorId, command.Description);
            _inventoryRepository.SaveChanges();
            return opration.Succedded();
        }

        public OpreatinResult Reduce(ReduceInventory command)
        {
            var opration = new OpreatinResult();
            var inventory = _inventoryRepository.Get(command.InventoryId);
            if (inventory==null)
                return opration.Faild(ApplicationMessages.RecordNotFound);

            const long operatorId = 1;
            inventory.Reduce(command.Count, operatorId, command.Description,0); 
            _inventoryRepository.SaveChanges();
            return opration.Succedded();
        }

        public OpreatinResult Reduce(List<ReduceInventory> command)
        {
            const long operatorId = 1;
            var opration = new OpreatinResult();
            foreach (var item in command)
            {
                var inventory = _inventoryRepository.GetBy(item.ProductId);
                inventory.Reduce(command.Count, operatorId, item.Description,item.OrderId);
            }
            _inventoryRepository.SaveChanges();
            return opration.Succedded();
        }

        public List<InventoryViewModel> Search(InventorySearchModel searchModel)
        {
            return _inventoryRepository.Search(searchModel);
        }
    }
}
