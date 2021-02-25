using System;
using System.Collections.Generic;
using System.Linq;
using restaurant_server.Models;
using restaurant_server.Repositories;

namespace restaurant_server.Services
{
  public class RestaurantsService
  {
    private readonly RestaurantsRepository _repo;
    public RestaurantsService(RestaurantsRepository repo)
    {
      _repo = repo;
    }

    public IEnumerable<Restaurant> GetAll()
    {
      IEnumerable<Restaurant> restaurants = _repo.GetAll();
      return restaurants;
    }

    internal Restaurant Get(int id)
    {
      var data = _repo.Get(id);
      if (data == null)
      {
        throw new Exception("Invalid Id");
      }
      return data;
    }


    public Restaurant Create(Restaurant newRestaurant)
    {
      newRestaurant.Id = _repo.Create(newRestaurant);
      return newRestaurant;
    }

    internal Restaurant Edit(Restaurant editData, string userId)
    {
      Restaurant original = Get(editData.Id);
      if (original.OwnerId != userId) { throw new Exception("Access Denied: Cannot Edit a Restaurant You did not Create"); }
      editData.Name = editData.Name == null ? original.Name : editData.Name;
      editData.Location = editData.Location == null ? original.Location : editData.Location;

      return _repo.Edit(editData);

    }

    internal string Delete(int id, string userId)
    {
      Restaurant original = _repo.Get(id);
      if (original == null) { throw new Exception("Bad ID"); }
      if (original.OwnerId != userId) { throw new Exception("Access Denied: Cannot Edit a Restaurant You did not Create"); }
      _repo.Remove(id);
      return "successfully deleted";
    }

  }
}