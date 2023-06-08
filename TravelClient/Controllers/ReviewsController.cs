using Microsoft.AspNetCore.Mvc;
using TravelClient.Models;

namespace TravelClient.Controllers;

public class ReviewsController : Controller
{
  public IActionResult Index()
  {
    List<Review> reviews = Review.GetReviews();
    ViewBag.ReviewPopular = Review.GetPopular(); 
    return View(reviews);
  }

  public IActionResult Random()
  {
    List<Review> reviews = Review.GetRandom();
    return View(reviews);
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
  public ActionResult SearchDestination(string destination)
  {
    return RedirectToAction("GetDestination", new { destination});
  }

  public ActionResult GetDestination(string destination)
  {
    List<Review> reviews = Review.GetDestination(destination);
    return View(reviews);
  }
}