using _0_Framework.Domain;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManagement.Domain.InventoryAgg
{
    public class Inventory : EntityBase
    {
        public long ProductId { get; private set; }
        public double UnitPrice { get; private set; }
        public bool InStock { get; private set; }
        public List<InventoryOperation> Operations { get; private set; }
      
        public Inventory(long productId, double unitPrice)
        {
            ProductId=productId;
            UnitPrice=unitPrice;
            InStock=false;
        }
        public void Edit(long productId, double unitPrice)
        {
            ProductId=productId;
            UnitPrice=unitPrice;
        }
        public long CalculateCurrentCount()
        {
            var plus = Operations.Where(x => x.Operation).Sum(x => x.Count);
            var minus = Operations.Where(x => !x.Operation).Sum(x => x.Count);
            return plus-minus;
        }

        public void Increase(long count,long opratorId,string description)
        {
            var currentCount = CalculateCurrentCount() + count;
            var opreaton = new InventoryOperation(true, count, opratorId, currentCount, description, 0, Id);
            Operations.Add(opreaton);
            InStock=currentCount>0;
        }

        public void Reduce(long count, long opratorId, string description ,long orderId)
        {
            var currentCount = CalculateCurrentCount() - count;
            var opreaton = new InventoryOperation(false, count, opratorId, currentCount, description, orderId, Id);
            Operations.Add(opreaton);
            InStock=currentCount>0;
        }
    }

}
