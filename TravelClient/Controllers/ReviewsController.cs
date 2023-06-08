using Microsoft.AspNetCore.Mvc;
using TravelClient.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace TravelClient.Controllers;

public class ReviewsController : Controller
{
  #nullable enable
  public IActionResult Index(int? pageNumber)
  {
    List<Review> reviews = Review.GetReviews();
    // IQueryable<Review> reviewsQueryable = reviews.AsQueryable();
    ViewBag.ReviewPopular = Review.GetPopular(); 
    int pageSize = 3;
    return View(PaginatedList<Review>.Create(reviews, pageNumber ?? 1, pageSize ));
  }
  #nullable disable

  public IActionResult Random(int? pageNumber)
  {
    List<Review> reviews = Review.GetRandom();
    int  pageSize = reviews.Count;
    return View(PaginatedList<Review>.Create(reviews, pageNumber ?? 1, pageSize ));
  }

  public IActionResult Details(int id)
  {
    Review review = Review.GetDetails(id);
    return View(review);
  }

  public ActionResult Create()
  {
    return View();
  }

  [HttpPost]
  public ActionResult Create(Review review)
  {
    Review.Post(review);
    return RedirectToAction("Index");
  }

  public ActionResult Edit(int id)
  {
    Review review = Review.GetDetails(id);
    return View(review);
  }

  [HttpPost]
  public ActionResult Edit(Review review)
  {
    Review.Put(review);
    return RedirectToAction("Details", new { id = review.ReviewId});
  }

  public ActionResult Delete(int id)
  {
    Review review = Review.GetDetails(id);
    return View(review);
  }

  [HttpPost, ActionName("Delete")]
  public ActionResult DeleteConfirmed(int id)
  {
    Review.Delete(id);
    return RedirectToAction("Index");
  }

  [HttpPost, ActionName("Index")]
  public ActionResult SearchDestination(string destination, string country)
  {
    return RedirectToAction("GetDestination", new { destination, country});
  }

  public ActionResult GetDestination(int? pageNumber, string destination, string country)
  {
    ViewBag.Destination = destination;
    ViewBag.Country =  country;
    List<Review> reviews = Review.GetDestination(destination, country);
    int  pageSize = 3;
    return View(PaginatedList<Review>.Create(reviews, pageNumber ?? 1, pageSize ));
  }
}