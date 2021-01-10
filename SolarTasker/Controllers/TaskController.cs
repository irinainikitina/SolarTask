using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SolarTasker.Models;
using Task = SolarTasker.Models.Task;

namespace SolarTasker.Controllers
{
    public class TaskController : Controller
    {
        private readonly TaskContext _context;

        public TaskController(TaskContext context)
        {
            _context = context;
        }

        // GET: Task
        public async Task<IActionResult> Index()
        {
            return View(_context.Tasks.ToList());
        }


        // GET: Task/Create
        public IActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new Task());
            else
                return View(_context.Tasks.Find(id));
        }

        // POST: Task/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(Task task)
        {
            if (ModelState.IsValid)
            {
                if (task.Id == 0)
                    _context.Add(task);
                else
                    _context.Update(task);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }


        // GET: Task/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var task =await _context.Tasks.FindAsync(id);
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}