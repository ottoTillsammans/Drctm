using System;
using Microsoft.EntityFrameworkCore;

namespace ServerSln
{
    static class Test
    {
        static void Main()
        {
            var options = new DbContextOptionsBuilder<TestDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDB")
                .Options;

            var testDbContext = new TestDbContext(options);

            /// Тесты будут тут...
            
            testDbContext.Dispose();
        }
    }
}

