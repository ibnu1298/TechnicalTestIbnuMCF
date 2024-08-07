using BackEnd.DTOs;

namespace BackEnd.Interfaces
{
    public interface ICrud<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetByName(string name);
        Task<T> GetById(string id);
        Task<BaseResponse> Insert(T obj);
        Task<BaseResponse> Update(T obj);
        Task Delete(string id);
    }
}
