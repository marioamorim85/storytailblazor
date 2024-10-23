using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoryTailBlazor.Data;
using StoryTailBlazor.Models;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoryTailApi.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly StoryTailDbContext _context;

        public BooksController(StoryTailDbContext context)
        {
            _context = context;
        }

        // GET: api/books/all
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetAllBooks()
        {
            var books = await _context.Books
                .Include(b => b.AgeGroup)
                .Include(b => b.AuthorBooks).ThenInclude(ab => ab.Author)
                .Select(b => new BookDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    Description = b.Description,
                    ReadTime = b.ReadTime,
                    IsActive = b.IsActive ? 1 : 0,  // Mapear para 1 ou 0
                    AgeGroup = new AgeGroupDto  // Usando a DTO AgeGroupDto
                    {
                        Id = b.AgeGroup.Id,
                        AgeGroupDescription = b.AgeGroup.AgeGroupDescription
                    },
                    Authors = b.AuthorBooks.Select(ab => new AuthorDto
                    {
                        FirstName = ab.Author.FirstName,
                        LastName = ab.Author.LastName
                    }).ToList()
                })
                .ToListAsync();

            if (books == null || books.Count == 0)
            {
                return NotFound("No books found.");
            }

            return Ok(books); // Returns all books in DTO form
        }

        // GET: api/books (com filtros)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks(
            string title = null,
            string author = null,
            int? isActive = null,  // Alterado de bool? para int?
            int? minReadTime = null,
            int? maxReadTime = null,
            int? ageGroupId = null,
            string tag = null)
        {
            var query = _context.Books.AsQueryable();

            // Filtrar por título
            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(b => b.Title.Contains(title));
            }

            // Filtrar por autor
            if (!string.IsNullOrEmpty(author))
            {
                query = query.Where(b => b.AuthorBooks.Any(ab => ab.Author.FirstName.Contains(author) || ab.Author.LastName.Contains(author)));
            }

            // Filtrar por estado ativo (1 para ativo, 0 para inativo)
            if (isActive.HasValue)
            {
                query = query.Where(b => b.IsActive == (isActive == 1));
            }

            // Filtrar por intervalo de tempo de leitura
            if (minReadTime.HasValue && maxReadTime.HasValue)
            {
                query = query.Where(b => b.ReadTime >= minReadTime && b.ReadTime <= maxReadTime);
            }

            // Filtrar por grupo etário
            if (ageGroupId.HasValue)
            {
                query = query.Where(b => b.AgeGroupId == ageGroupId);
            }

            // Incluir relacionamentos e mapear para DTOs
            var books = await query
                .Include(b => b.AgeGroup)
                .Include(b => b.AuthorBooks).ThenInclude(ab => ab.Author)
                .Select(b => new BookDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    Description = b.Description,
                    ReadTime = b.ReadTime,
                    IsActive = b.IsActive ? 1 : 0,  // Mapear para 1 ou 0
                    AgeGroup = new AgeGroupDto  // Usando a DTO AgeGroupDto
                    {
                        Id = b.AgeGroup.Id,
                        AgeGroupDescription = b.AgeGroup.AgeGroupDescription
                    },
                    Authors = b.AuthorBooks.Select(ab => new AuthorDto
                    {
                        FirstName = ab.Author.FirstName,
                        LastName = ab.Author.LastName
                    }).ToList()
                })
                .ToListAsync();

            if (books == null || books.Count == 0)
            {
                return NotFound(); // Returns 404 if no books are found
            }

            return Ok(books); // Returns the found books in DTO form
        }

        // GET: api/books/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetBook(int id)
        {
            var book = await _context.Books
                .Include(b => b.AgeGroup)
                .Include(b => b.AuthorBooks).ThenInclude(ab => ab.Author)
                .Select(b => new BookDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    Description = b.Description,
                    ReadTime = b.ReadTime,
                    IsActive = b.IsActive ? 1 : 0,  // Mapear para 1 ou 0
                    AgeGroup = new AgeGroupDto  // Usando a DTO AgeGroupDto
                    {
                        Id = b.AgeGroup.Id,
                        AgeGroupDescription = b.AgeGroup.AgeGroupDescription
                    },
                    Authors = b.AuthorBooks.Select(ab => new AuthorDto
                    {
                        FirstName = ab.Author.FirstName,
                        LastName = ab.Author.LastName
                    }).ToList()
                })
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                return NotFound($"Book with ID {id} not found.");
            }

            return Ok(book); // Returns the found book in DTO form
        }

        // GET: api/books/{bookId}/activities
        [HttpGet("{bookId}/activities")]
        public async Task<ActionResult<IEnumerable<ActivityDto>>> GetActivitiesForBook(int bookId)
        {
            var bookExists = await _context.Books.AnyAsync(b => b.Id == bookId);
            if (!bookExists)
            {
                return NotFound($"Book with ID {bookId} not found.");
            }

            var activities = await _context.ActivityBooks
                .Where(ab => ab.BookId == bookId)
                .Select(ab => new ActivityDto
                {
                    Id = ab.Activity.Id,
                    Title = ab.Activity.Title,
                    Description = ab.Activity.Description
                })
                .ToListAsync();

            if (activities == null || activities.Count == 0)
            {
                return NotFound("No activities found for this book.");
            }

            return Ok(activities); // Returns the activities associated with the book
        }

        // GET: api/books/favorites/{userId}
        [HttpGet("favorites/{userId}")]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetFavoriteBooks(int userId)
        {
            var userExists = await _context.Users.AnyAsync(u => u.Id == userId);
            if (!userExists)
            {
                return NotFound($"User with ID {userId} not found.");
            }

            var favoriteBooks = await _context.BookUserFavourites
                .Where(fb => fb.UserId == userId)
                .Include(fb => fb.Book).ThenInclude(b => b.AgeGroup)
                .Include(fb => fb.Book).ThenInclude(b => b.AuthorBooks).ThenInclude(ab => ab.Author)
                .Select(fb => new BookDto
                {
                    Id = fb.Book.Id,
                    Title = fb.Book.Title,
                    Description = fb.Book.Description,
                    ReadTime = fb.Book.ReadTime,
                    IsActive = fb.Book.IsActive ? 1 : 0,  // Mapear para 1 ou 0
                    AgeGroup = new AgeGroupDto  // Usando a DTO AgeGroupDto
                    {
                        Id = fb.Book.AgeGroup.Id,
                        AgeGroupDescription = fb.Book.AgeGroup.AgeGroupDescription
                    },
                    Authors = fb.Book.AuthorBooks.Select(ab => new AuthorDto
                    {
                        FirstName = ab.Author.FirstName,
                        LastName = ab.Author.LastName
                    }).ToList()
                })
                .ToListAsync();

            if (favoriteBooks == null || favoriteBooks.Count == 0)
            {
                return NotFound("No favorite books found for this user.");
            }

            return Ok(favoriteBooks); // Returns the user's favorite books
        }

        // GET: api/books/read/{userId}
        [HttpGet("read/{userId}")]
        public async Task<ActionResult<IEnumerable<BookUserReadDto>>> GetReadBooksWithProgress(int userId)
        {
            var userExists = await _context.Users.AnyAsync(u => u.Id == userId);
            if (!userExists)
            {
                return NotFound($"User with ID {userId} not found.");
            }

            var readBooks = await _context.BookUserReads
                .Where(br => br.UserId == userId)
                .Include(br => br.Book).ThenInclude(b => b.AgeGroup)
                .Include(br => br.Book).ThenInclude(b => b.AuthorBooks).ThenInclude(ab => ab.Author)
                .Select(br => new BookUserReadDto
                {
                    BookId = br.Book.Id,
                    Title = br.Book.Title,
                    Description = br.Book.Description,
                    ReadTime = br.Book.ReadTime,
                    IsActive = br.Book.IsActive ? 1 : 0,  // Mapear para 1 ou 0
                    AgeGroupDescription = br.Book.AgeGroup.AgeGroupDescription,
                    Progress = br.Progress,
                    Authors = br.Book.AuthorBooks.Select(ab => new AuthorDto
                    {
                        FirstName = ab.Author.FirstName,
                        LastName = ab.Author.LastName
                    }).ToList()
                })
                .ToListAsync();

            if (readBooks == null || readBooks.Count == 0)
            {
                return NotFound("No books read found for this user.");
            }

            return Ok(readBooks); // Returns the user's read books with progress
        }

        // GET: api/books/popular-books
        [HttpGet("popular-books")]
        public async Task<ActionResult<IEnumerable<PopularBookDto>>> GetPopularBooks()
        {
            var threeMonthsAgo = DateTime.Now.AddMonths(-3);

            var popularBooks = await _context.BookClicks
                .Where(bc => bc.ClickedAt >= threeMonthsAgo)
                .GroupBy(bc => bc.BookId)
                .Select(group => new PopularBookDto
                {
                    BookId = group.Key,
                    ClickCount = group.Count(),
                    BookTitle = group.FirstOrDefault().Book.Title
                })
                .OrderByDescending(b => b.ClickCount)
                .ToListAsync();

            if (popularBooks == null || popularBooks.Count == 0)
            {
                return NotFound("No popular books found in the last 3 months.");
            }

            return Ok(popularBooks); // Returns the most popular books
        }

        // GET: api/books/peak-hours
        [HttpGet("peak-hours")]
        public async Task<ActionResult<IEnumerable<PeakHourDto>>> GetPeakUsageHours()
        {
            var threeMonthsAgo = DateTime.Now.AddMonths(-3);

            var peakHours = await _context.BookClicks
                .Where(bc => bc.ClickedAt >= threeMonthsAgo)
                .GroupBy(bc => bc.ClickedAt.Hour)
                .Select(group => new PeakHourDto
                {
                    Hour = group.Key,
                    ClickCount = group.Count()
                })
                .OrderByDescending(h => h.ClickCount)
                .ToListAsync();

            if (peakHours == null || peakHours.Count == 0)
            {
                return NotFound("No peak usage hours found in the last 3 months.");
            }

            return Ok(peakHours); // Returns the peak usage hours
        }
    }

    // DTO classes for transferring the necessary data
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ReadTime { get; set; }
        public int IsActive { get; set; }  // Alterado para int
        public AgeGroupDto AgeGroup { get; set; } = new();
        public List<AuthorDto> Authors { get; set; } = new();
    }

    public class AuthorDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class ActivityDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class BookUserReadDto
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ReadTime { get; set; }
        public int IsActive { get; set; }  // Alterado para int
        public string AgeGroupDescription { get; set; }
        public double Progress { get; set; }
        public List<AuthorDto> Authors { get; set; }
    }

    public class PopularBookDto
    {
        public int BookId { get; set; }
        public int ClickCount { get; set; }
        public string BookTitle { get; set; }
    }

    public class PeakHourDto
    {
        public int Hour { get; set; }
        public int ClickCount { get; set; }
    }
}
