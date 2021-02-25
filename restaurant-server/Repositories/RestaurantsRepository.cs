using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using restaurant_server.Models;

namespace restaurant_server.Repositories
{
  public class RestaurantsRepository
  {

    private readonly IDbConnection _db;

    public RestaurantsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal IEnumerable<Restaurant> GetAll()
    {
      string sql = @"
       SELECT 
       restaurant.*,
       profile.* 
       FROM restaurants restaurant 
       JOIN profiles profile ON restaurant.ownerId = profile.id;";
      return _db.Query<Restaurant, Profile, Restaurant>(sql, (restaurant, profile) => { restaurant.Owner = profile; return restaurant; }, splitOn: "id");
    }

    internal Restaurant Get(int id)
    {
      string sql = @"
       SELECT 
       restaurant.*,
       profile.* 
       FROM restaurants restaurant 
       JOIN profiles profile ON restaurant.ownerId = profile.id
       WHERE restaurant.id = @id;";
      return _db.Query<Restaurant, Profile, Restaurant>(sql, (restaurant, profile) => { restaurant.Owner = profile; return restaurant; }, new { id }, splitOn: "id").FirstOrDefault();
    }


    internal int Create(Restaurant RestaurantData)
    {
      string sql = @"
            INSERT INTO Restaurants
            (name, ownerId, location)
            VALUES
            (@Name, @OwnerId, @Location);
            SELECT LAST_INSERT_ID()";
      return _db.ExecuteScalar<int>(sql, RestaurantData);
    }

    internal Restaurant Edit(Restaurant editData)
    {
      string sql = @"
            UPDATE restaurants
            SET 
            name = @Name,
            location = @Location
            WHERE id = @Id;";
      _db.Execute(sql, editData);
      return editData;
    }

    internal void Remove(int id)
    {
      string sql = "DELETE FROM restaurants WHERE id = @id LIMIT 1";
      _db.Execute(sql, new { id });
    }

  }
}