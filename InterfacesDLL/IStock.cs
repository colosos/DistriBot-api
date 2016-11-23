using System.Threading.Tasks;

namespace InterfacesDLL
{
        public interface IStock
        {
        Task<int> RemainingStock(int PrdId);
        }
}
