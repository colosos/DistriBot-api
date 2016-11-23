using System.Threading.Tasks;

namespace Interfaces
{
        public interface IStock
        {
        Task<int> RemainingStock(int PrdId);
        }
}
