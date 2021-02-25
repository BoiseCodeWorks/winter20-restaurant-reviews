using System;
using System.Collections.Generic;
using System.Linq;
using restaurant_server.Models;
using restaurant_server.Repositories;

namespace restaurant_server.Services
{
  public class ReviewsService
  {
    private readonly ReviewsRepository _repo;
    public ReviewsService(ReviewsRepository repo)
    {
      _repo = repo;
    }
    public IEnumerable<Review> GetAll()
    {
      IEnumerable<Review> reviews = _repo.GetAll();
      return reviews.ToList().FindAll(r => r.Published);
    }

    public Review Create(Review newReview)
    {
      newReview.Id = _repo.Create(newReview);
      return newReview;
    }

    public Review Get(int id)
    {
      Review original = _repo.Get(id);
      if (original == null) { throw new Exception("Invalid Id"); }
      return original;
    }

    internal Review Edit(Review editData, string userId)
    {
      Review original = Get(editData.Id);
      if (original.OwnerId != userId)
      {
        throw new Exception("Access Denied, You cannot edit something that is not yours");
      }
      editData.Title = editData.Title == null ? original.Title : editData.Title;
      editData.Body = editData.Body == null ? original.Body : editData.Body;
      return _repo.Edit(editData);
    }


    internal string Delete(int id, string userId)
    {
      Review original = Get(id);
      if (original.OwnerId != userId)
      {
        throw new Exception("Access Denied, You cannot delete something that is not yours");
      }
      _repo.Remove(id);
      return "succesfully deleted";
    }

    internal IEnumerable<Review> GetByRestaurantId(int id)
    {
      return _repo.GetByRestaurantId(id).ToList().FindAll(r => r.Published);
    }

    internal IEnumerable<Review> GetByProfileId(string id)
    {
      return _repo.GetByOwnerId(id).ToList().FindAll(r => r.Published);
    }


    internal IEnumerable<Review> GetReviewsByAccountId(string id)
    {
      return _repo.GetByOwnerId(id);

    }
  }
}