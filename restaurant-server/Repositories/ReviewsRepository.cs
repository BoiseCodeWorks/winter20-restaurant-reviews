using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using restaurant_server.Models;

namespace restaurant_server.Repositories
{
  public class ReviewsRepository
  {

    private readonly IDbConnection _db;

    public ReviewsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal IEnumerable<Review> GetAll()
    {
      string sql = @"
       SELECT 
       review.*,
       profile.* 
       FROM reviews review 
       JOIN profiles profile ON review.ownerId = profile.id;";
      return _db.Query<Review, Profile, Review>(sql, (review, profile) => { review.Owner = profile; return review; }, splitOn: "id");
    }

    internal Review Get(int id)
    {
      string sql = @"
       SELECT 
       review.*,
       profile.* 
       FROM reviews review 
       JOIN profiles profile ON review.ownerId = profile.id
       WHERE review.id = @id;";
      return _db.Query<Review, Profile, Review>(sql, (review, profile) => { review.Owner = profile; return review; }, new { id }, splitOn: "id").FirstOrDefault();
    }

    internal int Create(Review ReviewData)
    {
      string sql = @"
      INSERT INTO reviews
      (title, body, ownerId, published, restaurantId, rating)
      VALUES
      (@Title, @Body, @OwnerId, @Published, @RestaurantId, @Rating);
      SELECT LAST_INSERT_ID();";
      return _db.ExecuteScalar<int>(sql, ReviewData);
    }

    internal Review Edit(Review editData)
    {
      string sql = @"
     UPDATE reviews
     SET
     title = @Title,
     body = @Body
     WHERE id = @Id;";
      _db.Execute(sql, editData);
      return editData;
    }

    internal void Remove(int id)
    {
      string sql = "DELETE FROM reviews WHERE id = @id LIMIT 1";
      _db.Execute(sql, new { id });
    }

    internal IEnumerable<Review> GetByRestaurantId(int id)
    {
      string sql = @"
       SELECT 
       review.*,
       profile.* 
       FROM reviews review 
       JOIN profiles profile ON review.ownerId = profile.id
       WHERE review.restaurantId = @id;";
      return _db.Query<Review, Profile, Review>(sql, (review, profile) => { review.Owner = profile; return review; }, new { id }, splitOn: "id");
    }

    internal IEnumerable<Review> GetByOwnerId(string id)
    {
      string sql = @"
       SELECT 
       review.*,
       profile.* 
       FROM reviews review 
       JOIN profiles profile ON review.ownerId = profile.id
       WHERE review.ownerId = @id;";
      return _db.Query<Review, Profile, Review>(sql, (review, profile) => { review.Owner = profile; return review; }, new { id }, splitOn: "id");

    }





  }
}