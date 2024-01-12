using ChangePrice.Models;

namespace ChangePrice.Repository
{
    public interface IPriceRepository
    {
        List<AlertModel> GetList();
        object Get(Guid id);
        void Remove(Guid id);
        void Add(List<AlertModel> listAlert);
    }
}
