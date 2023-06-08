using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TravelClient.Models
{
  public class Review
  {
    public int ReviewId { get; set; }
    [Required]
    [StringLength(255, ErrorMessage = "length can't exceed 255 characters")]
    public string ReviewDestination { get; set; }
    public string ReviewCountry { get; set; }
    [Required]
    public string ReviewUserName {get;set;}
    [Range(0,10, ErrorMessage ="Rating should be between 0 and 10")]
    public int ReviewRating { get; set; }


    public static List<Review> GetReviews()
    {
        Task<string> apiCallTask = ApiHelper.GetAll();
        string result = apiCallTask.Result;

        JArray jsonResponse = JsonConvert.DeserializeObject<JArray>(result);
        List<Review> reviewList = JsonConvert.DeserializeObject<List<Review>>(jsonResponse.ToString());

        return reviewList;
    }

    public static Review GetDetails(int id)
    {
      Task<string> apiCallTask = ApiHelper.Get(id);
      string result = apiCallTask.Result;

      JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(result);
      Review review = JsonConvert.DeserializeObject<Review>(jsonResponse.ToString());

      return review;
    }

    public static void Post(Review review)
    {
      string jsonReview = JsonConvert.SerializeObject(review);
      ApiHelper.Post(jsonReview);
    }

    public static void Put(Review review)
    {
      string jsonReview = JsonConvert.SerializeObject(review);
      ApiHelper.Put(review.ReviewId, jsonReview);
    }
    public static void Delete(int id)
    {
      ApiHelper.Delete(id);
    }

    public static List<Review> GetRandom()
    {
        var apiCallTask = ApiHelper.GetRandom();
        var result = apiCallTask.Result;

        JArray jsonResponse = JsonConvert.DeserializeObject<JArray>(result);
        List<Review> reviewList = JsonConvert.DeserializeObject<List<Review>>(jsonResponse.ToString());
        // string destination = reviewList[0].ReviewDestination;

        return reviewList;
    }

    public static String GetPopular()
    {
      Task<string> apiCallTask = ApiHelper.GetPopular();
      string result = apiCallTask.Result;

      return result;
    }

    public static List<Review> GetDestination(string destination, string country)
    {
        var apiCallTask = ApiHelper.GetDestination(destination, country);
        var result = apiCallTask.Result;

        JArray jsonResponse = JsonConvert.DeserializeObject<JArray>(result);
        List<Review> reviewList = JsonConvert.DeserializeObject<List<Review>>(jsonResponse.ToString());

        return reviewList;
    }
  }
}