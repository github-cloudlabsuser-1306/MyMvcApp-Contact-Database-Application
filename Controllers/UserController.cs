using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyMvcApp.Controllers;

public class UserController : Controller
{
    private static readonly List<User> userlist = new List<User>();
    private static readonly object userlistLock = new object();

    // GET: User
    public IActionResult Index()
    {
        lock (userlistLock)
        {
            return View(userlist.ToList());
        }
    }

    // GET: User/Details/5
    public IActionResult Details(int id)
    {
        lock (userlistLock)
        {
            var user = userlist.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
    }

    // GET: User/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: User/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(User user)
    {
        if (ModelState.IsValid)
        {
            lock (userlistLock)
            {
                user.Id = userlist.Count > 0 ? userlist.Max(u => u.Id) + 1 : 1;
                userlist.Add(user);
            }
            return RedirectToAction("Index");
        }
        return View(user);
    }

    // GET: User/Edit/5
    public IActionResult Edit(int id)
    {
        lock (userlistLock)
        {
            var user = userlist.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
    }

    // POST: User/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, User user)
    {
        if (ModelState.IsValid)
        {
            lock (userlistLock)
            {
                var existingUser = userlist.FirstOrDefault(u => u.Id == id);
                if (existingUser == null)
                {
                    return NotFound();
                }
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;
            }
            return RedirectToAction("Index");
        }
        return View(user);
    }

    // GET: User/Delete/5
    public IActionResult Delete(int id)
    {
        lock (userlistLock)
        {
            var user = userlist.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
    }

    // POST: User/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        lock (userlistLock)
        {
            var user = userlist.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            userlist.Remove(user);
        }
        return RedirectToAction("Index");
    }
}
// Note: The above code assumes that the User model has properties like Id, Name, and Email.
// Ensure that you have the necessary using directives for your project, such as:
