using AccomodationWebAPI.Logic.Factories;

namespace AccomodationWepApiTests
{
    public class PagingFactoryTest
    {
        [Fact]
        public void Create_PageNumberIsLesserThenOne_ThrowException()
        {
            List<object> objList = new()
            {
                new(),
                new(),
                new(),
                new(),
                new(),
            };
            IPagingFactroy factory = new PagingFactroy();

            Assert.Throws<Exception>(() => factory.Create(objList, 0, 2));
        }

        [Fact]
        public void Create_PageNumberIsGreaterThenTotalPage_ThrowException()
        {
            List<object> objList = new()
            {
                new(),
                new(),
                new(),
                new(),
                new(),
            };
            IPagingFactroy factory = new PagingFactroy();

            Assert.Throws<Exception>(() => factory.Create(objList, 4, 2));
        }

        [Fact]
        public void Create_ExamineTotalPages_ItHasToBeCorrect()
        {
            List<object> objList = new()
            {
                new(),
                new(),
                new(),
                new(),
                new(),
            };
            IPagingFactroy factory = new PagingFactroy();

            var dto = factory.Create(objList, 1, 2);

            Assert.NotNull(dto);
            Assert.Equal(3, dto.TotalPages);
        }

        [Fact]
        public void Create_ExamineTotalItems_ItHasToBeCorrect()
        {
            List<object> objList = new()
            {
                new(),
                new(),
                new(),
                new(),
                new(),
            };
            IPagingFactroy factory = new PagingFactroy();

            var dto = factory.Create(objList, 1, 2);

            Assert.NotNull(dto);
            Assert.Equal(5, dto.TotalItems);
        }

        [Theory]
        [InlineData(1, 2, 0)]
        [InlineData(3, 0, 2)]
        [InlineData(2, 3, 1)]
        public void Create_ExamineNextAndPreviousPageNumbers_ItHasToBeCorrect(int currentPage, int correctNextPage, int correctPrevPage)
        {
            List<object> objList = new()
            {
                new(),
                new(),
                new(),
                new(),
                new(),
            };
            IPagingFactroy factory = new PagingFactroy();

            var dto = factory.Create(objList, currentPage, 2);

            Assert.NotNull(dto);
            Assert.Equal(correctPrevPage, dto.PreviousPage);
            Assert.Equal(correctNextPage, dto.NextPage);
        }

        [Fact]
        public void Create_ExamineCurrentPageAndPageSizeSet_ItHasToBeCorrect()
        {

            List<object> objList = new()
            {
                new(),
                new(),
                new(),
                new(),
                new(),
            };
            IPagingFactroy factory = new PagingFactroy();
            int pageSize = 2;
            int currentPage = 1;

            var dto = factory.Create(objList, currentPage, pageSize);

            Assert.NotNull(dto);
            Assert.Equal(pageSize, dto.PageSize);
            Assert.Equal(currentPage, dto.CurrentPage);
        }

        [Fact]
        public void Create_GetTheFirstPageWhenThereIsJustOneElementInTheDB_ShowTheOneElementAndSetTheNextAndPrevPageToZero()
        {

            List<object> objList = new()
            {
                new()
            };
            IPagingFactroy factory = new PagingFactroy();
            int pageSize = 2;
            int currentPage = 1;

            var dto = factory.Create(objList, currentPage, pageSize);

            Assert.NotNull(dto);
            Assert.Equal(pageSize, dto.PageSize);
            Assert.Equal(currentPage, dto.CurrentPage);
            Assert.True(1 == dto.Items.Count());
            Assert.True(dto.NextPage == 0);
            Assert.True(dto.PreviousPage == 0);
        }
    }
}