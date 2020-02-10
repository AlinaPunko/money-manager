using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DataAccess.Core;
using DataAccess.Helpers;
using DataAccess.Models;
using DataAccess.Projections;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class TransactionRepository : GenericRepository<Transaction>
    {
        public TransactionRepository(DbContext context) : base(context) { }

        public IReadOnlyList<Transaction> GetAll()
        {
            return Get()
                .ToList();
        }

        public new void Add(Transaction transaction)
        {
            base.Add(transaction);
            Transaction addedTransaction = Get().Include(t => t.Asset).Include(t => t.Category).FirstOrDefault(t => t.Id==transaction.Id);
            if (addedTransaction == null)
            {
                return;
            }

            if (transaction.Category.Type == CategoryType.Income)
            {
                transaction.Asset.Balance += transaction.Amount;
            }
            else
            {
                transaction.Asset.Balance -= transaction.Amount;
            }

            Context.SaveChanges();
        }

        public new void Remove(Transaction transaction)
        {
            base.Remove(transaction);
            if (transaction.Category.Type == CategoryType.Income)
            {
                transaction.Asset.Balance -= transaction.Amount;
            }
            else
            {
                transaction.Asset.Balance += transaction.Amount;
            }

            Context.SaveChanges();
        }

        public new void Update(Transaction transaction)
        {
            double previousAmountValue = Convert.ToDouble(Context.Entry(transaction).OriginalValues["Balance"]);
            base.Update(transaction);

            if (transaction.Category.Type == CategoryType.Income)
            {
                transaction.Asset.Balance -= previousAmountValue;
                transaction.Asset.Balance += transaction.Amount;
            }
            else
            {
                transaction.Asset.Balance += previousAmountValue;
                transaction.Asset.Balance -= transaction.Amount;
            }

            Context.SaveChanges();
        }

        public IReadOnlyList<FullTransactionInfo> GetTransactionsByUser(Guid userId)
        {
            List<Transaction> transactions = Get(t => t.Asset.UserId == userId)
                .OrderByDescending(t => t.Date)
                .ThenBy(t => t.Asset.Name)
                .ThenBy(t => t.Category.Name)
                .ToList();

            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Transaction, FullTransactionInfo>()
                    .ForMember(fullInfo => fullInfo.AssetName,
                        fullInfo => fullInfo.MapFrom(t => t.Asset.Name))
                    .ForMember(fullInfo => fullInfo.CategoryName,
                        fullInfo => fullInfo.MapFrom(t => t.Category.Name))
                    .ForMember(fullInfo => fullInfo.ParentName,
                        fullInfo => fullInfo.MapFrom(t => t.Category.Parent.Name));
            });

            IMapper mapper = config.CreateMapper();
            return mapper.Map<IReadOnlyList<FullTransactionInfo>>(transactions);
        }

        public IReadOnlyList<BudgetInfo> GetBudgetForPeriod(Guid userId, DateTime startDate, DateTime endDate)
        {
            List<Transaction> transactions = 
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
            IQueryable<Transaction> transactions = Get(t => t.Asset.UserId == userId && t.Date.Month==DateTime.Now.Month);

            foreach (Transaction transaction in transactions)
            {
                Remove(transaction);
            }
        }
    }
}
