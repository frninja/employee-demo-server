using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

using SimpleCode.EmployeeDemoServer.Models;

namespace SimpleCode.EmployeeDemoServer.Db
{
    public class EmployeeDatabaseInitializer : DropCreateDatabaseIfModelChanges<EmployeeContext>
    {
        protected override void Seed(EmployeeContext context) {
            GenerateEmployees(120, context);
        }

        private void GenerateEmployees(int count, EmployeeContext context) {
            for (int i = 0; i < count; ++i) {
                context.Employees.AddOrUpdate(GenerateEmployee());
            }
        }

        private Employee GenerateEmployee() {
            string fullName = $"{firstNames.SelectRandom()} {lastNames.SelectRandom()}";
            string email = $"{fullName.Replace(' ', '.').ToLower()}{random.Next(1000).ToString()}@{mailDomens.SelectRandom()}";
            DateTime birthDay = DateTimeExtensions.GetRandomDate(new DateTime(1950, 01, 01), new DateTime(2000, 01, 01));

            return new Employee(Guid.NewGuid(), fullName, email, birthDay, random.Next(35000, 200000));
        }

        private readonly string[] firstNames = new[] { "Oleg", "Amida", "Platon", "Alexander", "Ivan", "Sergey" };
        private readonly string[] lastNames = new[] { "Batashov", "Gavrilov", "Fedorovsky", "Khovanskyi" };
        private readonly string[] mailDomens = new[] { "gmail.com", "ya.ru", "mail.ru" };

        private Random random = new Random((int)DateTime.UtcNow.Ticks);
    }


    public static class IEnumerableExtensions
    {
        public static T SelectRandom<T>(this IEnumerable<T> items) {
            return items.ElementAt(random.Next(items.Count()));
        }

        private static readonly Random random = new Random((int)DateTime.UtcNow.Ticks);
    }

    public static class DateTimeExtensions
    {
        public static DateTime GetRandomDate(DateTime from, DateTime to) {
            if (to < from)
                throw new ArgumentException("to is greater than from");

            TimeSpan delta = to - from;
            TimeSpan randomTicks = TimeSpan.FromTicks((long)(random.NextDouble() * delta.Ticks));

            return (from + randomTicks).Date;
        }


        private static readonly Random random = new Random((int)DateTime.UtcNow.Ticks);
    }
}
