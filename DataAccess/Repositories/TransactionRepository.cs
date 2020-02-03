using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DataAccess.Core;
using DataAccess.Enums;
using DataAccess.Helpers;
using DataAccess.Models;
using DataAccess.Projections;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class TransactionRepository : GenericRepository<Transaction>
    {

        public IReadOnlyList<Transaction> GetAll()
        {
            return Get()
                .ToList();
        }

        public new Transaction GetById(Guid id)
        {
            return base.GetById(id);
        }

        public new void Add(Transaction transaction)
        {
            base.Add(transaction);
            var addedTransaction = Get().Include(t => t.Asset).Include(t => t.Category).FirstOrDefault(t => t.Id==transaction.Id);
            if (addedTransaction == null)
            {
                return;
            }

            if (transaction.Category.Type == CategoryType.Income)
            {
                transaction.Asset.Amount += transaction.Amount;
            }
            else
            {
                transaction.Asset.Amount -= transaction.Amount;
            }

            context.SaveChanges();
        }

        public new void Remove(Transaction transaction)
        {
            base.Remove(transaction);
            if (transaction.Category.Type == CategoryType.Income)
            {
                transaction.Asset.Amount -= transaction.Amount;
            }
            else
            {
                transaction.Asset.Amount += transaction.Amount;
            }

            context.SaveChanges();
        }

        public new void Update(Transaction transaction)
        {
            double previousAmountValue = Convert.ToDouble(context.Entry(transaction).OriginalValues["Amount"]);
            base.Update(transaction);

            if (transaction.Category.Type == CategoryType.Income)
            {
                transaction.Asset.Amount -= previousAmountValue;
                transaction.Asset.Amount += transaction.Amount;
            }
            else
            {
                transaction.Asset.Amount += previousAmountValue;
                transaction.Asset.Amount -= transaction.Amount;
            }

            context.SaveChanges();
        }

        public IReadOnlyList<FullTransactionInfo> GetTransactionsByUser(Guid userId)
        {
            var transactions = Get(t => t.Asset.UserId == userId)
                .OrderByDescending(t => t.Date)
                .ThenBy(t => t.Asset.Name)
                .ThenBy(t => t.Category.Name)
                .ToList();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Transaction, FullTransactionInfo>()
                    .ForMember(fullInfo => fullInfo.AssetName,
                        fullInfo => fullInfo.MapFrom(t => t.Asset.Name))
                    .ForMember(fullInfo => fullInfo.CategoryName,
                        fullInfo => fullInfo.MapFrom(t => t.Category.Name))
                    .ForMember(fullInfo => fullInfo.ParentName,
                        fullInfo => fullInfo.MapFrom(t => t.Category.Parent.Name));
            });

            var mapper = config.CreateMapper();
            return mapper.Map<IReadOnlyList<FullTransactionInfo>>(transactions);
        }

        public IReadOnlyList<BudgetInfo> GetBudgetForPeriod(Guid userId, DateTime startDate, DateTime endDate)
        {
            var transactions = 
                Get(t => t.Asset.UserId == userId && t.Date >= startDate && t.Date <= endDate)
                .OrderBy(t => t.Date)
                .ToList();

            double incomes = BalanceHelper.GetIncomes(transactions);
            double expenses = BalanceHelper.GetOutcomes(transactions);

            return transactions
                .GroupBy(b => new { b.Date.Month, b.Date.Year },
                    (key, group) => new BudgetInfo(incomes, expenses, key.Month, key.Year))
                .ToList();
        }

        public void DeleteTransactionsOfUserInCurrentMonth(Guid userId)
        {
            var transactions = Get(t => t.Asset.UserId == userId && t.Date.Month==DateTime.Now.Month);

            foreach (var transaction in transactions)
            {
                Remove(transaction);
            }
        }

        public TransactionRepository(DbContext context) : base(context)
        {

        }
    }
}
