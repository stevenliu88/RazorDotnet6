using AbbyWeb.Data;
using AbbyWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Categories
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        public Category Category { get; set; }
        private readonly ApplicationDbContext _db;
        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet(int id)
        {
            Category = _db.Category.FirstOrDefault(c => c.Id == id);
        }

        public async Task<IActionResult> OnPost()
        {
            var CategoryFromDb = _db.Category.Find(Category.Id);
            if (CategoryFromDb != null)
            {
                _db.Category.Remove(CategoryFromDb);
                await _db.SaveChangesAsync();
                TempData["success"] = "Category delete successfully";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
