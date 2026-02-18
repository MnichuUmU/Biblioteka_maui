using Microsoft.Maui.ApplicationModel.DataTransfer;
using Microsoft.Maui.Controls;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Biblioteka_v1
{
    public class LocalDbService
    {
        private const string DB_NAME = "db_biblioteka.db3";
        private SQLiteAsyncConnection _connection;

        public class KsiazkaWithLog : Ksiazka
        {
            public string RentedDate { get; set; }
        }

        public LocalDbService()
        {
        }

        private async Task Init()
        {
            if (_connection != null) return;

            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory , DB_NAME));

            //creating tables
            await _connection.CreateTableAsync<Ksiazka>();
            await _connection.CreateTableAsync<Accounts>();
            await _connection.CreateTableAsync<Logs>();

            var count = await _connection.Table<Ksiazka>().CountAsync();
            if(count == 0)
            {
                var noweKsiazki = new List<Ksiazka>
                {
                    new Ksiazka { Tytle = "Pan Tadeusz", Author = "Adam Mickiewicz", Tags = "Poezja epicka", Date = "1834", Stan = "tak" },
                    new Ksiazka { Tytle = "Lalka", Author = "Bolesław Prus", Tags = "Powieść społeczna", Date = "1890", Stan = "tak" },
                    new Ksiazka { Tytle = "Potop", Author = "Henryk Sienkiewicz", Tags = "Powieść historyczna", Date = "1886", Stan = "tak" },
                    new Ksiazka {Tytle = "Zbrodnia i kara" , Author = "Fiodor Dostojewski" , Tags = "Powiesć kryminalna", Date = "1866", Stan = "tak"}
                };
                await _connection.InsertAllAsync(noweKsiazki);

                var noweAccounts = new List<Accounts>
                {
                    new Accounts { FName = "Jerebi", LName = "Michorczyk", Mail = "yaoi@gmail.com", Password = "yaoi", Admin = false},
                    new Accounts { FName = "a", LName = "a", Mail = "a@gmail.com", Password = "a", Admin = false},
                    new Accounts { FName = "admin", LName = "123", Mail = "admin123@gmail.com", Password = "admin123",  Admin = true},

                };
                await _connection.InsertAllAsync(noweAccounts);

                var boweLogs = new List<Logs>
                {
                    new Logs { BookId = 1, AccountId = 1, Date = DateTime.Now.ToString("dd/MM/yyyy HH:mm") , Action = true,},
                };
                await _connection.InsertAllAsync(noweAccounts);
            }
        }

        //////////////////////////////////////////////////////
        /// METODY DO KSIAZKA
        //////////////////////////////////////////////////////
        public async Task CreateKsiazka( Ksiazka ksiazka )
        {
            await Init();
            await _connection.InsertAsync(ksiazka);
        }
        public async Task<List<Ksiazka>> GetKsiaki()
        {
            await Init();
            return await _connection.Table<Ksiazka>().ToListAsync();
        }
        public async Task<List<Ksiazka>> GetKsiakiByAvaible()
        {
            await Init();

            string sql = "SELECT * FROM ksiazka WHERE stan = 'tak';";
            return await _connection.QueryAsync<Ksiazka>(sql);
        }

        public async Task<List<KsiazkaWithLog>> GetKsiakiByUnavaible()
        {
            await Init();

            string sql = "SELECT k.*, MAX(l.date) AS RentedDate " +
                "FROM ksiazka k " +
                "JOIN logs l ON l.book_id = k.id " +
                "WHERE k.stan = 'nie' " +
                "GROUP BY k.id " +
                "ORDER BY RentedDate DESC; ";
            //string sql = "SELECT * FROM ksiazka WHERE stan = 'nie';";
            return await _connection.QueryAsync<KsiazkaWithLog>(sql);
        }

        public async Task<Ksiazka> LookForKsiazka( string Tytle , string Author)
        {
            await Init();
            return await _connection.Table<Ksiazka>().Where(x => x.Tytle == Tytle && x.Author == Author).FirstOrDefaultAsync();
        }

        public async Task<List<Ksiazka>> SearchKsiazki( string text)
        {
            await Init();

            string sql = "SELECT * FROM ksiazka WHERE tytle LIKE ? OR author LIKE ?";
            string temp = $"%{text}%";
            return await _connection.QueryAsync<Ksiazka>(sql , temp , temp);
            
        }
        public async Task UpdateKsiazka(Ksiazka ksiazka, int userId )
        {
            await CreateLog(ksiazka.Id , userId , ksiazka.Stan == "tak");
            await _connection.UpdateAsync(ksiazka);
        }


        //////////////////////////////////////////////////////
        /// METODY DO ACCOUNTS
        //////////////////////////////////////////////////////
        public async Task CreateAcc( Accounts account )
        {
            await Init();
            await _connection.InsertAsync(account);
        }
        public async Task<Accounts> LookForAccount(string mail, string password)
        {
            await Init();
            return await _connection.Table<Accounts>().Where(x=> x.Mail == mail && x.Password == password).FirstOrDefaultAsync();
        }
        public async Task<Accounts> GetAccountByMail(string mail)
        {
            await Init();
            return await _connection.Table<Accounts>().Where(x=> x.Mail == mail).FirstOrDefaultAsync();
        }
        //////////////////////////////////////////////////////
        /// METODY DO LOGOW
        //////////////////////////////////////////////////////
        public async Task CreateLog( int bookId, int userId, bool action )
        {
            var log = new Logs
            {
                BookId = bookId ,
                AccountId = userId ,
                Date = DateTime.Now.ToString("dd/MM/yyyy HH:mm") ,
                Action = action ,
            };
            await Init();
            await _connection.InsertAsync(log);
        }

        //public async Task<Ksiazka> GetById( int id )
        //{
        //    return await _connection.Table<Ksiazka>().Where(x => x.Id == id).FirstOrDefaultAsync() ;
        //}






        //public async Task Delete(Ksiazka ksiazka )
        //{
        //    await _connection.DeleteAsync(ksiazka);
        //}
    }
}
