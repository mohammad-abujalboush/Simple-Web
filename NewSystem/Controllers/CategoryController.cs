using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop.Implementation;
using NewSystem.Data;
using NewSystem.Models;

namespace NewSystem.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDBContext _db;
        public CategoryController(AppDBContext db)
        {
            _db = db;   
        }
        public IActionResult Index()
        {
            List<Category> ObjCategoryList = _db.Categories.ToList();
            return View(ObjCategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if(obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name","Name and Display Order can't match");
            }
            if(obj.Name != null && obj.Name == "Test" || obj.Name == "test")
            {
                ModelState.AddModelError("","Test is invalid");
            }

            if(ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["Success"] = "Category Created Succefully";
                return RedirectToAction("Index");
            }
                return View();
            
        }
        
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? CategoryFromDB = _db.Categories.FirstOrDefault(u=> u.Id == id);
            if(CategoryFromDB == null)
            {
                return NotFound();
            }
            return View(CategoryFromDB);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Name and Display Order can't match");
            }
            if (obj.Name != null && obj.Name == "Test" || obj.Name == "test")
            {
                ModelState.AddModelError("", "Test is invalid");
            }

            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["Success"] = "Category Edited Succefully";
                return RedirectToAction("Index");
            }
                return View();
            
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? CategoryFromDB = _db.Categories.Find(id);
            if (CategoryFromDB == null)
            {
                return NotFound();
            }
            return View(CategoryFromDB);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? obj = _db.Categories.Find(id);
            if(obj == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["Success"] = "Category Deleted Succefully";
            return RedirectToAction("Index");
        }

    }
}
