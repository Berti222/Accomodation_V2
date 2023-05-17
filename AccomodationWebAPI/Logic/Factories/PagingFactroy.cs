using AccomodationWebAPI.DTOs;

namespace AccomodationWebAPI.Logic.Factories
{
    public class PagingFactroy : IPagingFactroy
    {
        public PagingDTO<T> Create<T>(List<T> values, int pageNumber, int pageSize)
            where T : class
        {
            PagingDTO<T> result = new();

            int totalPages = GetTotalPage(values, pageSize);
            IsListOutOfRange(pageNumber, totalPages);

            result.TotalPages = totalPages;
            result.CurrentPage = pageNumber;
            result.PageSize = pageSize;
            result.TotalItems = values.Count;

            int numberOfItemToScip = pageNumber * pageSize;
            result.Items = values.Skip(numberOfItemToScip).Take(pageSize).ToList();

            result.PreviousPage = GetPreviousPage(pageNumber);
            result.NextPage = GetNextPage(pageNumber, totalPages);

            return result;
        }

        private int GetPreviousPage(int currentPage)
        {
            int previousPage = currentPage - 1;
            return previousPage < 0 ? 0 : previousPage;
        }

        private int GetNextPage(int currentPage, int totalPage)
        {
            int nextPage = currentPage + 1;
            return nextPage > totalPage ? 0 : nextPage;
        }

        private int GetTotalPage<T>(List<T> values, int pageSize)
            => (int)Math.Ceiling(values.Count / (double)pageSize);

        private void IsListOutOfRange(int currentPage, int totalPages)
        {
            if (currentPage < 1 || currentPage > totalPages)
                throw new Exception("Page number is out of range");
        }
    }
}
