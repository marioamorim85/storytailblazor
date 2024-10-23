using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StoryTailBlazor.Models;

namespace StoryTailBlazor.Data
{
    public static class DatabaseSeeder
    {
        public static void SeedDatabase(IServiceProvider serviceProvider)
        {
            try
            {
                using (var context = new StoryTailDbContext(
                    serviceProvider.GetRequiredService<DbContextOptions<StoryTailDbContext>>()))
                {
                    Console.WriteLine("Starting database seeding process...");

                    // 1. User Types
                    if (!context.UserTypes.Any())
                    {
                        Console.WriteLine("Inserting user types...");
                        context.UserTypes.AddRange(
                            new UserType { UserTypeName = "admin" },
                            new UserType { UserTypeName = "premium" },
                            new UserType { UserTypeName = "free" }
                        );
                        context.SaveChanges();
                        Console.WriteLine("User types inserted successfully.");
                    }

                    // 2. Plans
                    if (!context.Plans.Any())
                    {
                        Console.WriteLine("Inserting plans...");
                        context.Plans.AddRange(
                            new Plan { Name = "Basic Plan", AccessLevel = 1 },
                            new Plan { Name = "Premium Plan", AccessLevel = 2 }
                        );
                        context.SaveChanges();
                        Console.WriteLine("Plans inserted successfully.");
                    }

                    // 3. Age Groups
                    if (!context.AgeGroups.Any())
                    {
                        Console.WriteLine("Inserting age groups...");
                        context.AgeGroups.AddRange(
                            new AgeGroup { AgeGroupDescription = "3-4" },
                            new AgeGroup { AgeGroupDescription = "5-6" },
                            new AgeGroup { AgeGroupDescription = "7-9" }
                        );
                        context.SaveChanges();
                        Console.WriteLine("Age groups inserted successfully.");
                    }

                    // 4. Authors
                    if (!context.Authors.Any())
                    {
                        Console.WriteLine("Inserting authors...");
                        var giles = new Author { FirstName = "Giles", LastName = "Andreae", Description = "Giles Andreae is a British poet and writer, best known for his children’s books.", Nationality = "British" };
                        var julia = new Author { FirstName = "Julia", LastName = "Donaldson", Description = "Julia Donaldson is a British author of children’s books, famous for her work 'The Gruffalo.'", Nationality = "British" };
                        var eric = new Author { FirstName = "Eric", LastName = "Carle", Description = "Eric Carle was an American writer and illustrator, known for 'Brown Bear, Brown Bear, What Do You See?'.", Nationality = "American" };
                        var rachel = new Author { FirstName = "Rachel", LastName = "Bright", Description = "Rachel Bright is a British author and illustrator, creator of popular children’s books.", Nationality = "British" };

                        context.Authors.AddRange(giles, julia, eric, rachel);
                        context.SaveChanges();
                        Console.WriteLine("Authors inserted successfully.");

                        // 5. Books
                        Console.WriteLine("Inserting books...");
                        var giraffes = new Book { Title = "Giraffes Can't Dance", Description = "A story about a giraffe that can’t dance, but learns to find his own rhythm.", ReadTime = 15, AccessLevel = 1, IsActive = true, AgeGroupId = 1 };
                        var monkey = new Book { Title = "Monkey Puzzle", Description = "A story about a monkey searching for his mother.", ReadTime = 10, AccessLevel = 2, IsActive = true, AgeGroupId = 2 };
                        var brownBear = new Book { Title = "Brown Bear, Brown Bear, What Do You See?", Description = "A classic story that teaches children about colors and animals.", ReadTime = 20, AccessLevel = 1, IsActive = true, AgeGroupId = 1 };
                        var koala = new Book { Title = "The Koala Who Could", Description = "A story about a koala who learns to be brave.", ReadTime = 15, AccessLevel = 2, IsActive = true, AgeGroupId = 3 };
                        var pancakes = new Book { Title = "Pancakes, Pancakes!", Description = "A delightful story about a boy trying to make pancakes.", ReadTime = 12, AccessLevel = 1, IsActive = true, AgeGroupId = 1 };

                        context.Books.AddRange(giraffes, monkey, brownBear, koala, pancakes);
                        context.SaveChanges();
                        Console.WriteLine("Books inserted successfully.");

                        // 6. Author-Book Associations
                        Console.WriteLine("Associating authors with books...");
                        context.AuthorBooks.AddRange(
                            new AuthorBook { Author = giles, Book = giraffes },
                            new AuthorBook { Author = julia, Book = monkey },
                            new AuthorBook { Author = eric, Book = brownBear },
                            new AuthorBook { Author = rachel, Book = koala },
                            new AuthorBook { Author = eric, Book = pancakes }
                        );
                        context.SaveChanges();
                        Console.WriteLine("Authors associated with books successfully.");
                    }

                    // 7. Insert Users
                    if (!context.Users.Any())
                    {
                        Console.WriteLine("Inserting users...");
                        var adminUser = new User { FirstName = "Admin", LastName = "User", UserTypeId = 1, UserName = "adminuser", Email = "admin@example.com", Password = "adminpassword" };
                        var premiumUser = new User { FirstName = "Pedro", LastName = "Silva", UserTypeId = 2, UserName = "pedrosilva", Email = "pedro.silva@example.com", Password = "password123" };
                        var anaUser = new User { FirstName = "Ana", LastName = "Santos", UserTypeId = 2, UserName = "anasantos", Email = "ana.santos@example.com", Password = "password123" };
                        var luisUser = new User { FirstName = "Luís", LastName = "Oliveira", UserTypeId = 3, UserName = "luisoliveira", Email = "luis.oliveira@example.com", Password = "password123" };

                        context.Users.AddRange(adminUser, premiumUser, anaUser, luisUser);
                        context.SaveChanges();
                        Console.WriteLine("Users inserted successfully.");
                    }

                    // 8. Insert Subscriptions
                    if (!context.Subscriptions.Any())
                    {
                        Console.WriteLine("Inserting subscriptions...");
                        context.Subscriptions.Add(new Subscription { UserId = 2, PlanId = 2, StartDate = DateTime.Now });
                        context.SaveChanges();
                        Console.WriteLine("Subscriptions inserted successfully.");
                    }

                    // 9. Insert Activities
                    if (!context.Activities.Any())
                    {
                        Console.WriteLine("Inserting activities...");
                        var activity1 = new Activity { Title = "Dance Like a Giraffe", Description = "An activity where kids are encouraged to dance like the characters in the book 'Giraffes Can't Dance'." };
                        var activity2 = new Activity { Title = "Giraffe Puzzle", Description = "Solve a puzzle based on the book 'Giraffes Can't Dance.'" };
                        var activity3 = new Activity { Title = "Coloring Page", Description = "Color a page from 'Giraffes Can't Dance.'" };

                        context.Activities.AddRange(activity1, activity2, activity3);
                        context.SaveChanges();
                        Console.WriteLine("Activities inserted successfully.");
                    }

                    // 10. Insert Videos
                    if (!context.Videos.Any())
                    {
                        Console.WriteLine("Inserting videos...");
                        context.Videos.Add(new Video { Title = "Giraffes Can't Dance - Animated", BookId = 1, VideoUrl = "https://example.com/giraffes_video" });
                        context.SaveChanges();
                        Console.WriteLine("Videos inserted successfully.");
                    }

                    // 11. Insert Tags
                    if (!context.Tags.Any())
                    {
                        Console.WriteLine("Inserting tags...");
                        context.Tags.AddRange(
                            new Tag { Name = "Children" },
                            new Tag { Name = "Adventure" },
                            new Tag { Name = "Funny" },
                            new Tag { Name = "Learning" }
                        );
                        context.SaveChanges();
                        Console.WriteLine("Tags inserted successfully.");
                    }

                    // 12. Insert Comments
                    if (!context.Comments.Any())
                    {
                        Console.WriteLine("Inserting comments...");
                        context.Comments.AddRange(
                            new Comment { BookId = 1, UserId = 2, CommentText = "A wonderful book!", Status = "approved" },
                            new Comment { BookId = 1, UserId = 1, CommentText = "Great illustrations and story!", Status = "approved" },
                            new Comment { BookId = 2, UserId = 2, CommentText = "A touching story about love and family.", Status = "approved" }
                        );
                        context.SaveChanges();
                        Console.WriteLine("Comments inserted successfully.");
                    }

                    // 13. Insert Activity Images
                    if (!context.ActivityImages.Any())
                    {
                        Console.WriteLine("Inserting activity images...");
                        context.ActivityImages.AddRange(
                            new ActivityImage { ActivityId = 2, Title = "Image of Giraffe Puzzle", ImageUrl = "https://example.com/image_of_giraffe_puzzle" },
                            new ActivityImage { ActivityId = 3, Title = "Image of Coloring Page", ImageUrl = "https://example.com/image_of_coloring_page" }
                        );
                        context.SaveChanges();
                        Console.WriteLine("Activity images inserted successfully.");
                    }

                    // 14. Insert Book User Read
                    if (!context.BookUserReads.Any())
                    {
                        Console.WriteLine("Inserting book user reads...");
                        context.BookUserReads.AddRange(
                            new BookUserRead { BookId = 1, UserId = 2, Progress = 50, Rating = 5, ReadDate = DateTime.Now },
                            new BookUserRead { BookId = 3, UserId = 2, Progress = 80, Rating = 4, ReadDate = DateTime.Now },
                            new BookUserRead { BookId = 4, UserId = 2, Progress = 100, Rating = 5, ReadDate = DateTime.Now }
                        );
                        context.SaveChanges();
                        Console.WriteLine("Book user reads inserted successfully.");
                    }

                    // 15. Insert Book User Favourite
                    if (!context.BookUserFavourites.Any())
                    {
                        Console.WriteLine("Inserting book user favourites...");
                        context.BookUserFavourites.AddRange(
                            new BookUserFavourite { BookId = 1, UserId = 2 },
                            new BookUserFavourite { BookId = 2, UserId = 2 },
                            new BookUserFavourite { BookId = 4, UserId = 3 }
                        );
                        context.SaveChanges();
                        Console.WriteLine("Book user favourites inserted successfully.");
                    }

                    // 16. Insert Book Clicks
                    if (!context.BookClicks.Any())
                    {
                        Console.WriteLine("Inserting book clicks...");
                        context.BookClicks.AddRange(
                            new BookClick { BookId = 1, ClickedAt = DateTime.Now },
                            new BookClick { BookId = 3, ClickedAt = DateTime.Now },
                            new BookClick { BookId = 4, ClickedAt = DateTime.Now }
                        );
                        context.SaveChanges();
                        Console.WriteLine("Book clicks inserted successfully.");
                    }

                    // 17. Insert Reports
                    if (!context.Reports.Any())
                    {
                        Console.WriteLine("Inserting reports...");
                        context.Reports.AddRange(
                            new Report { ReportType = "book_clicks", ReportData = "{ \"book_id\": 1, \"clicks\": 100 }" },
                            new Report { ReportType = "book_clicks", ReportData = "{ \"book_id\": 2, \"clicks\": 75 }" },
                            new Report { ReportType = "book_clicks", ReportData = "{ \"book_id\": 3, \"clicks\": 90 }" }
                        );
                        context.SaveChanges();
                        Console.WriteLine("Reports inserted successfully.");
                    }

                    // 18. Insert Pages
                    if (!context.Pages.Any())
                    {
                        Console.WriteLine("Inserting pages...");
                        context.Pages.AddRange(
                            new Page { BookId = 1, PageImageUrl = "https://example.com/giraffes_page1", PageIndex = 1 },
                            new Page { BookId = 2, PageImageUrl = "https://example.com/monkey_page1", PageIndex = 1 },
                            new Page { BookId = 3, PageImageUrl = "https://example.com/brown_bear_page1", PageIndex = 1 }
                        );
                        context.SaveChanges();
                        Console.WriteLine("Pages inserted successfully.");
                    }

                    // 19. Insert Comment Moderation
                    if (!context.CommentModerations.Any())
                    {
                        Console.WriteLine("Inserting comment moderation...");
                        context.CommentModerations.AddRange(
                            new CommentModeration { CommentId = 1, UserId = 1, Status = "approved", ModerationDate = DateTime.Now },
                            new CommentModeration { CommentId = 2, UserId = 2, Status = "approved", ModerationDate = DateTime.Now },
                            new CommentModeration { CommentId = 3, UserId = 1, Status = "rejected", ModerationDate = DateTime.Now }
                        );
                        context.SaveChanges();
                        Console.WriteLine("Comment moderation inserted successfully.");
                    }

                    // 20. Insert Activity Book
                    if (!context.ActivityBooks.Any())
                    {
                        Console.WriteLine("Inserting activity books...");
                        context.ActivityBooks.AddRange(
                            new ActivityBook { ActivityId = 1, BookId = 1 },
                            new ActivityBook { ActivityId = 2, BookId = 2 },
                            new ActivityBook { ActivityId = 3, BookId = 4 }
                        );
                        context.SaveChanges();
                        Console.WriteLine("Activity books inserted successfully.");
                    }

                    // 21. Insert Activity Book User
                    if (!context.ActivityBookUsers.Any())
                    {
                        Console.WriteLine("Inserting activity book users...");
                        context.ActivityBookUsers.AddRange(
                            new ActivityBookUser { ActivityBookId = 1, UserId = 2, Progress = 75 },
                            new ActivityBookUser { ActivityBookId = 2, UserId = 2, Progress = 50 },
                            new ActivityBookUser { ActivityBookId = 3, UserId = 1, Progress = 100 }
                        );
                        context.SaveChanges();
                        Console.WriteLine("Activity book users inserted successfully.");
                    }




                    // 22. Insert Tagging Tagged
                    if (!context.TaggingTaggeds.Any())
                    {
                        Console.WriteLine("Inserting tagging tagged...");
                        context.TaggingTaggeds.AddRange(
                            new TaggingTagged { BookId = 1, TagId = 1 },
                            new TaggingTagged { BookId = 1, TagId = 3 },
                            new TaggingTagged { BookId = 2, TagId = 2 },
                            new TaggingTagged { BookId = 2, TagId = 1 },
                            new TaggingTagged { BookId = 3, TagId = 4 },
                            new TaggingTagged { BookId = 3, TagId = 1 }
                        );
                        context.SaveChanges();
                        Console.WriteLine("Tagging tagged inserted successfully.");
                    }

                    // Final logging
                    Console.WriteLine("All data inserted successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during the seeding process: {ex.Message}");
            }
        }
    }
}
