using Microsoft.EntityFrameworkCore;
using TildeTestAssignment.ORM.Entities;
using TildeTestAssignment.ORM.Services.Interfaces;

namespace TildeTestAssignment.ORM.Services
{
    public class SeedingService : ISeedingService
    {
        private readonly IApplicationDbContext _applicationDbContext;

        private List<Person> Persons { get; set; }

        public SeedingService(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task SeedAsync()
        {
            if (await _applicationDbContext.Database.CanConnectAsync())
            {
                return;
            }

            await _applicationDbContext.Database.MigrateAsync();

            SeedPersons();
            SeedDebtsAsync();

            await _applicationDbContext.SaveChangesAsync();
        }

        private void SeedPersons()
        {
            var persons = new List<Person>
            {
                new()
                {
                    Id = Guid.Parse("5de24ece-57c8-4677-9a0c-d9473f40cb23"),
                    FirstName = "Gina",
                    LastName = "Mcguire",
                    Balance = 150.75M
                },
                new()
                {
                    Id = Guid.Parse("4f15be28-fc39-42bc-884c-fcaf89fcab1e"),
                    FirstName = "Patrick",
                    LastName = "Cole",
                    Balance = 231.48M
                },
                new()
                {
                    Id = Guid.Parse("ab3daaea-4506-4bee-b19f-c9be03585036"),
                    FirstName = "Grayson",
                    LastName = "Stephens",
                    Balance = -45.33M
                },
                new()
                {
                    Id = Guid.Parse("1e49716e-e1cf-4731-ae5f-cf1e7dae3aaf"),
                    FirstName = "Fraser",
                    LastName = "Valenzuela",
                    Balance = -120.00M
                },
                new()
                {
                    Id = Guid.Parse("16c77fae-b858-4cf4-93b0-4e0074918746"),
                    FirstName = "Lucia",
                    LastName = "Mcgowan",
                    Balance = 0
                }
            };

            _applicationDbContext.Persons.AddRange(persons);
            Persons = [.. _applicationDbContext.Persons.Local];
        }

        private void SeedDebtsAsync()
        {
            var debts = new List<Debt>
            {
                new()
                {
                    Id = Guid.Parse("0cdce4f1-7599-43ef-8e61-1e6ffe9c097a"),
                    Debtor = Persons[3],
                    Creditor = Persons[0],
                    Amount = 10.51M,
                    Refunded = 0
                },
                new()
                {
                    Id = Guid.Parse("faad1107-070d-487e-a7d5-effe5000694e"),
                    Debtor = Persons[4],
                    Creditor = Persons[2],
                    Amount = 25.72M,
                    Refunded = 0
                },
                new()
                {
                    Id = Guid.Parse("d67d8a1d-c13b-4207-96ea-2b6dcf26d62c"),
                    Debtor = Persons[1],
                    Creditor = Persons[4],
                    Amount = 30.00M,
                    Refunded = 0
                },
                new()
                {
                    Id = Guid.Parse("efb72bf8-d77e-4a37-ac6e-62f328cafbf6"),
                    Debtor = Persons[2],
                    Creditor = Persons[3],
                    Amount = 15.25M,
                    Refunded = 0
                },
                new()
                {
                    Id = Guid.Parse("f9339a9a-76b6-4160-81a4-e136041e2fd3"),
                    Debtor = Persons[1],
                    Creditor = Persons[4],
                    Amount = 50.80M,
                    Refunded = 0
                },
                new()
                {
                    Id = Guid.Parse("ad684aae-5a01-43f2-a573-1659d6e35b6b"),
                    Debtor = Persons[0],
                    Creditor = Persons[4],
                    Amount = 100.00M,
                    Refunded = 0
                },
                new()
                {
                    Id = Guid.Parse("99e70282-c8c6-4b51-964f-829d66f99f2f"),
                    Debtor = Persons[3],
                    Creditor = Persons[2],
                    Amount = 75.53M,
                    Refunded = 0
                },
                new()
                {
                    Id = Guid.Parse("6e58f127-2e2c-49c2-bdee-109136814dc7"),
                    Debtor = Persons[1],
                    Creditor = Persons[0],
                    Amount = 45.39M,
                    Refunded = 45.39M
                },
                new()
                {
                    Id = Guid.Parse("2a9844bf-33ba-42f4-ac6c-a71f3e5b16b0"),
                    Debtor = Persons[2],
                    Creditor = Persons[4],
                    Amount = 87.20M,
                    Refunded = 87.20M
                },
                new()
                {
                    Id = Guid.Parse("d506b490-ebc5-4170-b8e8-4dec2c5976a4"),
                    Debtor = Persons[2],
                    Creditor = Persons[3],
                    Amount = 61.90M,
                    Refunded = 61.90M
                },
                new()
                {
                    Id = Guid.Parse("1bc4f833-bbad-48cd-975d-7b02080a0b2b"),
                    Debtor = Persons[4],
                    Creditor = Persons[1],
                    Amount = 150.28M,
                    Refunded = 150.28M
                },
                new()
                {
                    Id = Guid.Parse("00a0dad4-8beb-4e54-b401-341bbda5b4be"),
                    Debtor = Persons[0],
                    Creditor = Persons[3],
                    Amount = 200.50M,
                    Refunded = 200.50M
                },
                new()
                {
                    Id = Guid.Parse("d7180af9-04f4-4ddd-82b8-757011908896"),
                    Debtor = Persons[4],
                    Creditor = Persons[2],
                    Amount = 175.75M,
                    Refunded = 175.75M
                },
                new()
                {
                    Id = Guid.Parse("fca73e32-9fda-4e97-9ea9-8a8c60681012"),
                    Debtor = Persons[1],
                    Creditor = Persons[4],
                    Amount = 221.00M,
                    Refunded = 221.00M
                },
                new()
                {
                    Id = Guid.Parse("3ccc8a51-3462-413e-bce1-73dee38a93b1"),
                    Debtor = Persons[0],
                    Creditor = Persons[4],
                    Amount = 45.30M,
                    Refunded = 27.40M
                },
                new()
                {
                    Id = Guid.Parse("f623f387-e90c-439a-8f64-d8d0cd041815"),
                    Debtor = Persons[2],
                    Creditor = Persons[3],
                    Amount = 273.40M,
                    Refunded = 100.00M
                },
                new()
                {
                    Id = Guid.Parse("925493d0-1d79-4ca5-8c34-d6cc69433a14"),
                    Debtor = Persons[1],
                    Creditor = Persons[4],
                    Amount = 91.15M,
                    Refunded = 80.15M
                },
                new()
                {
                    Id = Guid.Parse("f070ba06-4e07-4281-8dbf-f8239fcab01a"),
                    Debtor = Persons[0],
                    Creditor = Persons[2],
                    Amount = 58.30M,
                    Refunded = 20.05M
                },
                new()
                {
                    Id = Guid.Parse("963c1f82-0899-46c3-847e-d6473b9302d8"),
                    Debtor = Persons[4],
                    Creditor = Persons[1],
                    Amount = 307.46M,
                    Refunded = 250.46M
                },
                new()
                {
                    Id = Guid.Parse("d7169cb2-7133-490f-a291-4798e96e5c2f"),
                    Debtor = Persons[3],
                    Creditor = Persons[0],
                    Amount = 79.99M,
                    Refunded = 9.99M,
                }
            };

            _applicationDbContext.Debts.AddRange(debts);
        }
    }
}