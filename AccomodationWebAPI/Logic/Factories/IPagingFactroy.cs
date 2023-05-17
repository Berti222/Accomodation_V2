using AccomodationWebAPI.DTOs;

namespace AccomodationWebAPI.Logic.Factories
{
    public interface IPagingFactroy
    {
        PagingDTO<T> Create<T>(List<T> values, int pageNumber, int pageSize) where T : class;
    }
}