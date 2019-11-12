using System;
using System.Collections.Generic;
using Litz.Services.Models;
namespace Litz.Services
{
    public interface IAccountService
    {
        AccountInfo GetAccountInfo(int id);
        List<Group> GetGroups(int userId);
        EditableResult Create(Group group);
        Group GetGroup(int userId, int groupId);

        EditableResult Update(Group group);

        EditableResult Create(Transaction transaction);

        List<Transaction> GetTransactions(int id, DateTime periodStart, DateTime periodEnd);

        List<PieModel> GetSummaryPerGroup(int id, DateTime periodStart, DateTime periodEnd);

        EditableResult Delete(Transaction transaction);

        List<GoalWithMetrics> GetGoalsWithMetrics(int userId, DateTime periodStart, DateTime periodEnd);
    }
}
