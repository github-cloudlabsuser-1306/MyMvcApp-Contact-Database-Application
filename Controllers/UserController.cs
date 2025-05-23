using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;

namespace MyMvcApp.Controllers;

public class UserController : Controller
{
    public static System.Collections.Generic.List<User> userlist = new System.Collections.Generic.List<User>();

        // GET: User
        public ActionResult Index()
        {
            return View(userlist);
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            var user = userlist.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound();
            return View(user);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(User user)
        {
            // Auto-increment Id to ensure uniqueness
            if (userlist.Count == 0)
                user.Id = 1;
            else
                user.Id = userlist.Max(u => u.Id) + 1;
            userlist.Add(user);
            return RedirectToAction("Index");
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            var user = userlist.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound();
            return View(user);
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, User user)
        {
            // Use user.Id from the posted model to ensure correct user is updated
            var existingUser = userlist.FirstOrDefault(u => u.Id == user.Id);
            if (existingUser == null)
                return NotFound();
            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            return RedirectToAction("Index");
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            var user = userlist.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound();
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            var user = userlist.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound();
            userlist.Remove(user);
            return RedirectToAction("Index");
        }

    // GET: User/Search
    public ActionResult Search(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
            return View("Index", userlist);
        var filtered = userlist.Where(u =>
            (!string.IsNullOrEmpty(u.Name) && u.Name.Contains(query, StringComparison.OrdinalIgnoreCase)) ||
            (!string.IsNullOrEmpty(u.Email) && u.Email.Contains(query, StringComparison.OrdinalIgnoreCase))
        ).ToList();
        return View("Index", filtered);
    }
}
