﻿using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DataAccess.Core;
using DataAccess.Helpers;
using DataAccess.MapperProfiles;
using DataAccess.Models;
using DataAccess.Projections;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class TransactionRepository : GenericRepository<Transaction>
    {
        readonly UnitOfWork unitOfWork;

        public TransactionRepository(DbContext context) : base(context)
        {
            unitOfWork = new UnitOfWork();
        }

        public IReadOnlyList<Transaction> GetAll()
        {
            return Get()
                .ToList();
        }

        public new void Add(Transaction transaction)
        {
            base.Add(transaction);
            Transaction addedTransaction = Get()
                .Include(t => t.Asset)
                .Include(t => t.Category)
                .FirstOrDefault(t => t.Id==transaction.Id);

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

            unitOfWork.Save();
        }

        public new void Remove(Transaction transaction)
        {
            if (transaction.Category.Type == CategoryType.Income)
            {
                transaction.Asset.Balance -= transaction.Amount;
            }
            else
            {
                transaction.Asset.Balance += transaction.Amount;
            }

            base.Remove(transaction);
            unitOfWork.Save();
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

            unitOfWork.Save();
        }

        public IReadOnlyList<FullTransactionInfo> GetTransactionsByUser(Guid userId)
        {
            List<Transaction> transactions = Get(t => t.Asset.UserId == userId)
                .OrderByDescending(t => t.Date)
                .ThenBy(t => t.Asset.Name)
                .ThenBy(t => t.Category.Name)
                .ToList();

            IMapper mapper = MapperWrapper.GetMapper();

            return mapper.Map<IReadOnlyList<FullTransactionInfo>>(transactions);
        }

        public IReadOnlyList<BudgetInfo> GetBudgetForPeriod(Guid userId, DateTime startDate, DateTime endDate)
        {
            List<Transaction> transactions = 
                Get(t => t.Asset.UserId == userId && t.Date >= startDate && t.Date <= endDate)
                .OrderBy(t => t.Date)
                .ToList();

            double incomes = BalanceHelper.GetSumMoney(transactions, CategoryType.Income);
            double expenses = BalanceHelper.GetSumMoney(transactions, CategoryType.Expense);

            return transactions
                .GroupBy(b => new { b.Date.Month, b.Date.Year },
                    (key, group) =>
                    {
                        BudgetInfo info = new BudgetInfo
                        {
                            Expenses = expenses,
                            Income = incomes,
                            Month = key.Month,
                            Year = key.Year
                        };
                        return info;
                    })
                .ToList();
        }

        public void DeleteUserTransactionsInCurrentMonth(Guid userId)
        {
            IQueryable<Transaction> transactions = Get(t => t.Asset.UserId == userId && t.Date.Month==DateTime.Now.Month);

            foreach (Transaction transaction in transactions)
            {
                Remove(transaction);
            }
        }
    }
}
