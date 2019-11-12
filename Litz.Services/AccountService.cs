using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Litz.Services.Models;

namespace Litz.Services
{
    public class AccountService : IAccountService
    {
        private readonly LitzEntities _context;

        public AccountService()
        {
            _context = new LitzEntities();
        }

        public AccountInfo GetAccountInfo(int id)
        {
            var sum = _context.Transaction.Where(x => x.UserId == id).
                Select(l => l.Amount).AsEnumerable().Sum(y => y);

            return new AccountInfo
            {
                Balance = sum,
                IsNegative = sum < 0
            };
        }

        public List<Group> GetGroups(int userId)
        {
            return _context.Group.Where(x => x.UserId == userId).ToList();
        }

        public EditableResult Create(Group group)
        {
            _context.Group.Add(group);
            _context.SaveChanges();

            return new EditableResult
            {
                Message = "Tag cadastrada com sucesso",
                HasError = false
            };
        }

        public Group GetGroup(int userId, int groupId)
        {
            return _context.Group.SingleOrDefault(x => x.UserId == userId && x.Id == groupId);
        }

        public EditableResult Update(Group group)
        {
            var groupToEdit = _context.Group.Find(group.Id);

            if (groupToEdit != null)
            {
                groupToEdit.Name = group.Name;
                groupToEdit.Description = group.Description;
                groupToEdit.Color = group.Color;

                _context.SaveChanges();

                return new EditableResult
                {
                    Message = "Alterações foram salvas com sucesso",
                    HasError = false
                };
            }

            throw new ApplicationException("As alterações não puderam ser salvas porque o item marcado para ser editado não existe");
        }

        public EditableResult Create(Transaction transaction)
        {
            try
            {
                _context.Transaction.Add(transaction);
                _context.SaveChanges();

                return new EditableResult
                {
                    Id = transaction.Id,
                    HasError = false,
                    Message = "Item criado com sucesso"
                };
            }
            catch (Exception e)
            {

                return new EditableResult
                {
                    HasError = true,
                    Message = e.Message
                };
            }
        }

        public List<Transaction> GetTransactions(int id, DateTime periodStart, DateTime periodEnd)
        {
            var transactions = _context.Transaction.Include(t => t.Group).Include(t => t.User)
                .Where(x => x.UserId == id
                            && DbFunctions.DiffDays(periodStart, x.Date) >= 0
                            && DbFunctions.DiffDays(periodEnd, x.Date) <= 0).ToList();

            return transactions;
        }

        public List<PieModel> GetSummaryPerGroup(int id, DateTime periodStart, DateTime periodEnd)
        {
            var allExpenses = _context.Transaction.Where(x => x.UserId == id && x.Type == "D"
                                                              && DbFunctions.DiffDays(periodStart, x.Date) >= 0
                                                              && DbFunctions.DiffDays(periodEnd, x.Date) <= 0)
                                  .Select(l => l.Amount).AsEnumerable().Sum(y => y) * -1;

            var queryResult =
                _context.Transaction.Where(x => x.UserId == id
                                                && x.Type == "D"
                                                && DbFunctions.DiffDays(periodStart, x.Date) >= 0
                                                && DbFunctions.DiffDays(periodEnd, x.Date) <= 0)
                    .GroupBy(y => y.GroupId).ToList().Select(x => new PieModel
                    {
                        Name = x.First().Group.Name,
                        Color = x.First().Group.Color,
                        Legend = (x.Sum(y => y.Amount) * -1).ToString("F2"),
                        Yaxis = (x.Sum(y => y.Amount) * -1 / allExpenses) * 100
                    }).ToList();

            return queryResult;
        }

        public EditableResult Delete(Transaction transaction)
        {
            var transactionToRemove = _context.Transaction.Find(transaction.Id);


            if (transactionToRemove != null)
            {
                _context.Transaction.Remove(transactionToRemove);
                _context.SaveChanges();

                return new EditableResult
                {
                    Message = "Item removido do extrato com sucesso",
                    HasError = false
                };
            }

            throw new ApplicationException("O item marcado para ser removido não existe");
        }

        public List<GoalWithMetrics> GetGoalsWithMetrics(int userId, DateTime periodStart, DateTime periodEnd)
        {
            var response = new List<GoalWithMetrics>();

            var goals = _context.Goal.Where(x => x.UserId == userId
                                                 && DbFunctions.DiffDays(periodStart, x.Period) >= 0
                                                 && DbFunctions.DiffDays(periodEnd, x.Period) <= 0).ToList();

            foreach (var goal in goals)
            {
                //Get all debt transactions and multiplies by -1 to get it as positive values
                var transactionOfGoalType = _context.Transaction.Where(x => x.UserId == userId
                                                && x.Type == "D"
                                                && x.GroupId == goal.GroupId
                                                && DbFunctions.DiffDays(periodStart, x.Date) >= 0
                                                && DbFunctions.DiffDays(periodEnd, x.Date) <= 0)
                                                .Select(l => l.Amount).AsEnumerable().Sum(x => x) * -1;

                response.Add(new GoalWithMetrics
                {
                    Color = goal.Group.Color,
                    GoalName = goal.Group.Name,
                    ExpectedExpenses = goal.Amount,
                    ActualExpenses = transactionOfGoalType
                });
            }

            return response;
        }
    }
}