using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using WhatsForDinner.DataModels;

namespace WhatsForDinner.Data
{
    public class RestaurantDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public RestaurantDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<RestaurantHistory>().Wait();
        }

        public Task<List<RestaurantHistory>> GetHistoryAsync()
        {
            return _database.Table<RestaurantHistory>().OrderByDescending(x => x.DateTime).ToListAsync();
        }

        public Task<int> SaveHistory(RestaurantHistory restaurant)
        {
            return _database.InsertAsync(restaurant);
        }
    }
}
