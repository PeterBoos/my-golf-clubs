using System.Collections.Generic;
using SQLite;
using golfclubdistanceorganizer.DAL;
using golfclubdistanceorganizer.Models;

namespace golfclubdistanceorganizer.Repositories
{
    public class GolfClubRepository
    {
        private SQLiteConnection _connection;

        public GolfClubRepository()
        {
            _connection = new SQLiteConnection(DatabaseFilePathRetriever.GetPath());
            _connection.CreateTable<GolfClub>();
        }

        public IEnumerable<GolfClub> GetAll()
        {
            return (from gc in _connection.Table<GolfClub>()
                    select gc);
        }

        public GolfClub Get(int id)
        {
            return _connection.Table<GolfClub>().FirstOrDefault(gc => gc.Id == id);
        }

        public void Add(GolfClub entity)
        {
            _connection.Insert(entity);
        }

        public void Update(GolfClub entity)
        {
            _connection.Update(entity);
        }

        public void Delete(int id)
        {
            _connection.Delete<GolfClub>(id);
        }
    }
}