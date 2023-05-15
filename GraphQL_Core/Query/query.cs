using GraphQL_Core.Types;

namespace GraphQL_Core.Query
{
    //Класс, который использует graphQL для запроса
    
    public class query
    {
        public Book Getbook() =>
            new Book()
            {
                Title = "Война и мир",
                Author = new Author()
                {
                    Name = "Лев Николаевич Толстой"
                }
            };
    }
}
