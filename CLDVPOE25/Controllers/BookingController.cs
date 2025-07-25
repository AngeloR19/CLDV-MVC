using CLDVPOE25.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CLDVPOE25.Controllers
{
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(
            string searchQuery,
            int? eventTypeId,
            DateTime? startDate,
            DateTime? endDate,
            bool? venueAvailability)
        {
            var bookings = _context.Booking
                .Include(b => b.EventItem).ThenInclude(e => e.EventType)
                .Include(b => b.Venue)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                bookings = bookings.Where(b =>
                    b.Id.ToString().Contains(searchQuery) ||
                    b.EventItem.Name.Contains(searchQuery));
            }

            if (eventTypeId.HasValue)
            {
                bookings = bookings.Where(b => b.EventItem.EventTypeId == eventTypeId.Value);
            }

            if (startDate.HasValue)
            {
                bookings = bookings.Where(b => b.DateOfEvent >= startDate.Value);
            }
            if (endDate.HasValue)
            {
                bookings = bookings.Where(b => b.DateOfEvent <= endDate.Value);
            }

            
            if (venueAvailability.HasValue)
            {
                bookings = bookings.Where(b => b.Venue.IsAvailable == venueAvailability.Value);
            }

            ViewBag.EventTypes = _context.EventType.ToList();
            ViewBag.SelectedEventTypeId = eventTypeId;
            ViewBag.StartDate = startDate?.ToString("yyyy-MM-dd");
            ViewBag.EndDate = endDate?.ToString("yyyy-MM-dd");
            ViewBag.VenueAvailability = venueAvailability;

            return View(await bookings.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewBag.Venue = _context.Venue.ToList();
            ViewBag.EventItem = _context.EventItem.Include(e => e.EventType).ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Booking booking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Venue = _context.Venue.ToList();
            ViewBag.EventItem = _context.EventItem.ToList();
            return View(booking);
        }

        public async Task<IActionResult> Duration(int id)
        {
            var booking = await _context.Booking.FindAsync(id);
            if (booking == null) return NotFound();

            return View(booking);
        }

        [HttpPost]
        public async Task<IActionResult> Duration(int id, Booking model)
        {
            var booking = await _context.Booking.FindAsync(id);
            if (booking == null) return NotFound();

            booking.Duration = model.Duration;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Feedback(int id)
        {
            var booking = await _context.Booking.FindAsync(id);
            if (booking == null) return NotFound();

            return View(booking);
        }

        [HttpPost]
        public async Task<IActionResult> Feedback(int id, Booking model)
        {
            var booking = await _context.Booking.FindAsync(id);
            if (booking == null) return NotFound();

            booking.Feedback = model.Feedback;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ManageBooking(int id)
        {
            var booking = await _context.Booking
                .Include(b => b.EventItem).ThenInclude(e => e.EventType)
                .Include(b => b.Venue)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (booking == null)
            {
                return NotFound();
            }

            var bookingViewModel = new BookingViewModel
            {
                BookingId = booking.Id,
                EventName = booking.EventItem.Name,
                EventType = booking.EventItem.EventType?.Name, // Updated
                EventDate = booking.EventItem.DateOfEvent,
                VenueName = booking.Venue.Name,
                VenueLocation = booking.Venue.Location,
                VenueCapacity = booking.Venue.Capacity,
                VenuePricePerHour = booking.Venue.PricePerHour,
                BookingDate = booking.DateOfEvent,
                Duration = booking.Duration,
                Feedback = booking.Feedback
            };

            return View(bookingViewModel);
        }
    }
}

