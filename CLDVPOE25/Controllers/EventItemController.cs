using CLDVPOE25.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CLDVPOE25.Controllers
{
    public class EventItemController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventItemController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EventItem/Index
        public async Task<IActionResult> Index()
        {
            var events = await _context.EventItem.Include(e => e.Venue).ToListAsync();
            return View(events);
        }

        // GET: EventItem/Create
        public IActionResult Create()
        {
            ViewBag.Venues = _context.Venue.ToList();
            ViewBag.EventTypes = _context.EventType.ToList(); // <-- Add this line
            return View();
        }

        // POST: EventItem/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,EventTypeId,DateOfEvent,ContactEmail,VenueId")] EventItem eventItem)
        {
            Console.WriteLine("POST action hit");
            if (ModelState.IsValid)
            {
                // Optionally load navigation properties if needed
                eventItem.EventType = await _context.EventType.FindAsync(eventItem.EventTypeId);

                _context.EventItem.Add(eventItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Venues = _context.Venue.ToList();
            ViewBag.EventTypes = _context.EventType.ToList();
            return View(eventItem);
        }

        // GET: EventItem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var eventItem = await _context.EventItem
                .Include(e => e.Venue)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (eventItem == null) return NotFound();

            return View(eventItem);
        }

        // GET: EventItem/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var eventItem = await _context.EventItem.FindAsync(id);
            if (eventItem == null) return NotFound();

            ViewBag.Venues = _context.Venue.ToList();
            return View(eventItem);
        }

        // POST: EventItem/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EventItem eventItem)
        {
            if (id != eventItem.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.EventItem.Any(e => e.Id == eventItem.Id))
                        return NotFound();
                    else
                        throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Venues = _context.Venue.ToList();
            return View(eventItem);
        }

        // GET: EventItem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var eventItem = await _context.EventItem
                .Include(e => e.Venue)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (eventItem == null) return NotFound();

            return View(eventItem);
        }

        // POST: EventItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eventItem = await _context.EventItem.FindAsync(id);
            if (eventItem != null)
            {
                _context.EventItem.Remove(eventItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
