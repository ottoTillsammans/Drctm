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

            #region Tests.

            userRepository.Create("Иван Иванов", "1234567890", State.Offline);
            userRepository.Create("Анна Поздеева", "1Qwerty?", State.Online);
            userRepository.Create("Илон Маск", "%^$^*@&*%0", State.Offline);
            userRepository.Create("Мария Петрова", "000000", State.Online);
            userRepository.Create("Иван Иванов", "zxcvbn", State.Offline);

            Console.WriteLine("Получение пользователя по Id:\n");

            User? byId1 = userRepository.GetById(1);
            User? byId2 = userRepository.GetById(2);
            User? byId4 = userRepository.GetById(4);

            Console.WriteLine(byId1.Name);
            Console.WriteLine(byId2.Name);
            Console.WriteLine(byId4.Name);
            Console.WriteLine("_________________________\n");

            Console.WriteLine("Получение пользователя по имени Илон Маск:\n");

            User? byName = userRepository.GetByName("Илон Маск");

            Console.WriteLine(byName.Name);
            Console.WriteLine("_________________________\n");

            Console.WriteLine("Поиск пользователей по имени Иван Иванов:\n");

            var users = userRepository.GetAllByName("Иван Иванов");

            foreach (var u in users)
            {
                Console.Write(u.Name);
                Console.WriteLine(" Id = {0}", u.Id);
            }
            Console.WriteLine("_________________________\n");

            Console.WriteLine("Добавление нового пользователя и возврат его Id:\n");

            int id = userRepository.Create("Николай Романов", "1918", State.Offline);

            Console.WriteLine("Id нового пользователя: {0}", id);
            Console.WriteLine("_________________________\n");

            Console.WriteLine("Обновление данных пользователя Id 1:\n");

            var user = userRepository.GetById(1);

            Console.WriteLine(user.Name);
            Console.WriteLine(user.Password);
            Console.WriteLine(user.State);
            Console.WriteLine(user.Id);
            Console.WriteLine();

            userRepository.Update(user.Id, "Петр Петров", "999_888_777", State.Online);

            Console.WriteLine(user.Name);
            Console.WriteLine(user.Password);
            Console.WriteLine(user.State);
            Console.WriteLine(user.Id);
            Console.WriteLine("_________________________\n");

            Console.WriteLine("Добавление нового контакта в список контактов пользователя Id 2:\n");

            var success1 = contactRepository.AddInUserContacts(2, 1);
            var success6 = contactRepository.AddInUserContacts(2, 6);

            Console.WriteLine("Добавление пользователя Id 1 в список контактов пользователя Id 2: {0}", success1 ? "Успешно" : "Не удалось");
            Console.WriteLine("Добавление пользователя Id 6 в список контактов пользователя Id 2: {0}", success6 ? "Успешно" : "Не удалось");
            Console.WriteLine("_________________________\n");


            Console.WriteLine("Получение контакта пользователя Id 1:\n");

            var contact = contactRepository.GetContact(1);

            Console.WriteLine("Id пользователя: {0}", 1);
            Console.WriteLine("Id пользователя, привязанного к контакту: {0}", contact.ContactId);
            Console.WriteLine("_________________________\n");

            Console.WriteLine("Получение списка контактов пользователя Id 2:\n");

            var contacts = contactRepository.GetUserContasts(2);

            foreach (var c in contacts)
            {
                Console.WriteLine("Id пользователя: {0}, Id контакта: {1}\n", c.UserId, c.ContactId);
            }
            Console.WriteLine("_________________________\n");

            Console.WriteLine("Удаление контакта пользователя Id 1:\n");

            var success = contactRepository.Delete(1);
            var contactsAfterDeleting = contactRepository.GetUserContasts(2);

            Console.WriteLine("Удаление контакта: {0}", success ? "Успешно\n" : "Не удалось\n");

            foreach (var c in contactsAfterDeleting)
            {
                Console.WriteLine("User Id: {0}, contact Id: {1}\n", c.UserId, c.ContactId);
            }
            Console.WriteLine("_________________________\n");

            Console.WriteLine("Получение списка сообщений пользователя Id 1:\n");

            messageRepository.Create(1, 2, DateTime.Now, DateTime.Now, "Привет!");
            messageRepository.Create(1, 3, DateTime.Now, DateTime.Now, "Пойдёшь сегодня в кино?");
            messageRepository.Create(1, 4, DateTime.Now, DateTime.Now, "Привет. Перезвони вечером в 6.");

            var messages = messageRepository.GetAllByUserId(1);

            foreach (var m in messages)
            {
                Console.WriteLine(m.Content);
                Console.WriteLine();
            }
            Console.WriteLine("_________________________\n");

            Console.WriteLine("Получение сообщения по строке \"Привет\" в списке сообщений пользователя Id 1:\n");

            var usersMessages = messageRepository.GetBySubstring(1, "Привет");

            foreach (var m in usersMessages)
            {
                Console.WriteLine(m.Content);
                Console.WriteLine();
            }
            Console.WriteLine("_________________________\n");

            Console.WriteLine("Добавление нового сообщения пользователю Id 1:\n");

            messageRepository.Create(1, 5, DateTime.Now, DateTime.Now, "Не звони мне больше.");
            
            var updatedMessages = messageRepository.GetAllByUserId(1);

            foreach (var m in updatedMessages)
            {
                Console.WriteLine(m.Content);
                Console.WriteLine();
            }

            Console.ReadKey();

            #endregion

            testDbContext.Dispose();
        }
    }
}

