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

            var userRepository = UserRepository.GetInstance(testDbContext);
            var messageRepository = MessageRepository.GetInstance(testDbContext);
            var contactRepository = ContactRepository.GetInstance(testDbContext);

            userRepository.Create("Иван Иванов", "1234567890", State.Offline);
            userRepository.Create("Анна Поздеева", "1Qwerty?", State.Online);
            userRepository.Create("Илон Маск", "%^$^*@&*%0", State.Offline);
            userRepository.Create("Мария Петрова", "000000", State.Online);

            User? byName = userRepository.GetByName("Иван");
            User? byId1 = userRepository.GetById(1);
            User? byId2 = userRepository.GetById(2);
            User? byId3 = userRepository.GetById(3);
            User? byId4 = userRepository.GetById(4);

            Console.WriteLine(byName.Name);
            Console.WriteLine(byId1.Name);
            Console.WriteLine(byId2.Name);
            Console.WriteLine(byId3.Name);
            Console.WriteLine(byId4.Name);

            Console.ReadKey();

            testDbContext.Dispose();
        }
    }
}

