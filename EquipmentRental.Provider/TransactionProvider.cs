﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EquipmentRental.DataAccess;
using EquipmentRental.Domain.DTOs;
using EquipmentRental.Domain.Entities;
using EquipmentRental.Domain.Enums;
using EquipmentRental.Provider.Utilities;
using EquipmentRental.Provider.ViewModels;
using Microsoft.Extensions.Logging;

namespace EquipmentRental.Provider
{
    public class TransactionProvider : ITransactionProvider
    {
        private readonly ILogger<TransactionProvider> _logger;

        readonly IGenericEfRepository<Transaction> _rep;
        readonly IInventoryProvider _inventoryProvider;

        public TransactionProvider(IGenericEfRepository<Transaction> rep, ILogger<TransactionProvider> logger, IInventoryProvider inventoryProvider)
        {
            _rep = rep;
            _logger = logger;
            _inventoryProvider = inventoryProvider;
        }

        public async Task<Invoice> GetAllTransactions(string customerId)
        {
            try
            {
                var item = (await _rep.Get()).Where(b => b.UserId.Equals(customerId));
                var inventories = await _inventoryProvider.GetAllInventories();
                var dtOs = Mapper.Map<IEnumerable<TransactionDTo>>(item).ToList();

                var result = from transaction in dtOs
                             join inventory in inventories on transaction.EquipmentId equals inventory.InventoryID
                             select new TransactionDTo
                             {
                                 Days = transaction.Days,
                                 Points = transaction.Points,
                                 Price = transaction.Price,
                                 TransactionDateTime = transaction.TransactionDateTime,
                                 Type = inventory.Type,
                                 EquipmentName = inventory.Name
                             };

                //Since this is just for demonstration purposes and we have just a single user
                //in the system we have returned "Admin" as the customer's name (See the Invoice.CustomerName property)
                //However in a real production we can retrieve the Customer's name from User's table like below 

                //var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                //var customer = await _appDbContext.Users.SingleOrDefaultAsync(c => c.Id == userId);

                var invoice = new Invoice
                {
                    Transactions = result,
                    TotalPoints = dtOs.Sum(c => c.Points),
                    TotalPrice = dtOs.Sum(c => c.Price)
                };
                return invoice;
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
                throw;
            }
        }


        public async Task<TransactionDTo> GetTransaction(string customerId, int id)
        {
            try
            {
                var item = await _rep.Get(id);
                if (item == null || !item.UserId.Equals(customerId)) return null;
                var dto = Mapper.Map<TransactionDTo>(item);
                return dto;

            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
                throw;
            }
        }

        public TransactionDTo AddTransaction(string customerId, TransactionDTo transaction)
        {
            try
            {
                Enum.TryParse(transaction.Type, out EquipmentType equipmentType);
                transaction.Points = Helper.CalculatePoints(equipmentType);
                transaction.Price = Helper.CalculatePrice(transaction.Days, equipmentType);
                transaction.TransactionDateTime = DateTime.Now.ToString();
                var itemToCreate = Mapper.Map<Transaction>(transaction);
                itemToCreate.UserId = customerId;
                _rep.Add(itemToCreate);
                if (!_rep.Save()) return null;
                var transactionDTo = Mapper.Map<TransactionDTo>(itemToCreate);
                return transactionDTo;
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
                throw;
            }
        }

        public async Task<bool?> DeleteTransactions()
        {
            try
            {
                var transactions = await _rep.Get();
                foreach (var transaction in transactions)
                {
                    _rep.Delete(transaction);
                }
                if (!_rep.Save()) return null;
                return true;
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
                throw;
            }
        }
    }
}
