using ChangePrice.Models;

namespace ChangePrice.Repository
{
    public interface IPriceRepository
    {
        List<RegisterPriceModel> GetList();
        object Get(Guid id);
        void Remove(Guid id);
        void Add(List<RegisterPriceModel> item);
    }
}
