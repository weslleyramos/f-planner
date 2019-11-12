using System;
using System.Collections.Generic;
using Litz.Services.Models;

namespace Litz.Services
{
    public class AccountServiceMock : IAccountService
    {
        public EditableResult Create(Group group)
        {
            throw new NotImplementedException();
        }

        public EditableResult Create(Transaction transaction)
        {
            throw new NotImplementedException();
        }

        public EditableResult Delete(Transaction transaction)
        {
            throw new NotImplementedException();
        }

        public AccountInfo GetAccountInfo(int id)
        {
            var number = new Random().Next(-1000, 1000);
            return new AccountInfo
            {
                Balance = number,
                IsNegative = number < 0
            };
        }

        public List<GoalWithMetrics> GetGoalsWithMetrics(int userId, DateTime periodStart, DateTime periodEnd)
        {
            return new List<GoalWithMetrics>
            {
                new GoalWithMetrics
                {
                    Color = "#0099ff",
                    GoalName = "Supermercado",
                    ExpectedExpenses = 400,
                    ActualExpenses = 280
                },
                new GoalWithMetrics
                {
                    Color = "#ffcccb",
                    GoalName = "Contas Gerais",
                    ExpectedExpenses = 400,
                    ActualExpenses = 280
                },
                new GoalWithMetrics
                {
                    Color = "#fed8b1",
                    GoalName = "Transporte",
                    ExpectedExpenses = 200,
                    ActualExpenses = 380
                },
            };
        }

        public Group GetGroup(int userId, int groupId)
        {
            return new Group
            {
                Id = 11,
                Name = "Group 2",
                Color = "#ffcccb",
                Description = "Group 2 description",
            };
        }

        public List<Group> GetGroups(int userId)
        {
            return new List<Group>
            {
                new Group
                {
                    Id = 10,
                    Name = "Group 1",
                    Color = "#fed8b1",
                    Description = "Group 1 description",
                },
                new Group
                {
                    Id = 11,
                    Name = "Group 2",
                    Color = "#ffcccb",
                    Description = "Group 2 description",
                },
                new Group
                {
                    Id = 11,
                    Name = "Group 3",
                    Color = "#0099ff",
                    Description = "Group 3 description",
                }
            };
        }

        public List<PieModel> GetSummaryPerGroup(int id, DateTime periodStart, DateTime periodEnd)
        {
            return new List<PieModel>
            {
                new PieModel
                {
                    Color = "#686de0",
                    Yaxis = 20,
                    Legend = "Desp. gerais",
                    Name = "Desp. gerais"
                },
                new PieModel
                {
                    Color = "#130f40",
                    Yaxis = 26,
                    Legend = "Saúde",
                    Name = "Saúde"
                },
                new PieModel
                {
                    Color = "#90ee90",
                    Yaxis = 59,
                    Legend = "Cartão",
                    Name = "Cartão"
                }
            };
        }

        public List<Transaction> GetTransactions(int id, DateTime periodStart, DateTime periodEnd)
        {
            throw new NotImplementedException();
        }

        public EditableResult Update(Group group)
        {
            throw new NotImplementedException();
        }
    }
}