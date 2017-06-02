using System.Collections.Generic;
using SQLite;
using golfclubdistanceorganizer.DAL;
using golfclubdistanceorganizer.Models;

namespace golfclubdistanceorganizer.Repositories
{
    public class RecordRepository
    {
        private SQLiteConnection _connection;

        public RecordRepository()
        {
            _connection = new SQLiteConnection(DatabaseFilePathRetriever.GetPath());
            _connection.CreateTable<Record>();
        }

        public IEnumerable<Record> GetAll()
        {
            return (from r in _connection.Table<Record>()
                    select r);
        }

        public Record Get(int id)
        {
            return _connection.Table<Record>().FirstOrDefault(r => r.Id == id);
        }

        public Record GetLatestByGolfClubId(int id)
        {
            return _connection.Table<Record>().Where(r => r.GolfClubId == id).OrderByDescending(r => r.Id).FirstOrDefault();
        }

        public IEnumerable<Record> GetAllByGolfClubId(int id)
        {
            return (from r in _connection.Table<Record>()
                    where r.GolfClubId == id
                    select r);
        }

        public void Add(Record entity)
        {
            _connection.Insert(entity);
        }

        public void Update(Record entity)
        {
            _connection.Update(entity);
        }

        public void Delete(int id)
        {
            _connection.Delete<Record>(id);
        }
    }
}