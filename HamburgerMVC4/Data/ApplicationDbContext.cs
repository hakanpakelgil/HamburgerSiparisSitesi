using HamburgerMVC4.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HamburgerMVC4.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Extra> Extras { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Order> Orders { get; set; }        
        public DbSet<Drink> Drinks { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Category>().HasData(
                new Category() { Id = 1, Name = "Beef" },
                new Category() { Id = 2, Name = "Chicken" },
                new Category() { Id = 3, Name = "Kids" },
                new Category() { Id = 4, Name = "Vegan" }
                );

            builder.Entity<Menu>().HasData(
                new Menu() { Id = 1, Name = "Big King", CategoryId = 1, UnitPrice = 185, Image = "bigking.png", Description = "Two flame grilled 100% beef patties with melted American cheese, freshly cut \r\niceberg lettuce, sliced white onions, crunchy pickles and Big king sauce all \r\non a warm, toasted, sesame seed bun." },
                new Menu() { Id = 2, Name = "Big Mac", CategoryId = 1, UnitPrice = 195, Image = "bigmac.png", Description = "The Boost Burger's Big Mac® is a 100% beef burger with a taste like no other. \r\nThe mouthwatering perfection starts with two 100% pure all beef patties and \r\nBig Mac® sauce sandwiched between a sesame seed bun. It's topped off with pickles, \r\ncrisp shredded lettuce, finely chopped onion, and a slice of American cheese." },
                new Menu() { Id = 3, Name = "Big Tasty", CategoryId = 1, UnitPrice = 200, Image = "bigtasty.png", Description = "This legendary American burger is a true all-rounder! A 100% beef patty, \r\nEmmental cheese, onions, juicy tomatoes and a unique smoky flavoured sauce \r\nall nested in a freshly toasted bun." },
                new Menu() { Id = 4, Name = "Cheeseburger", CategoryId = 1, UnitPrice = 180, Image = "cheeseburger.png", Description = "Cheeseburger is a hamburger with a slice of melted cheese on top of the \r\nmeat patty, added near the end of the cooking time. Cheeseburgers can include \r\nvariations in structure, ingredients and composition.\r\n" },
                new Menu() { Id = 5, Name = "Chicken Royale", CategoryId = 2, UnitPrice = 165, Image = "chickenroyale.png", Description = "Tasty chicken wrapped in a special crisp coating, topped with iceberg lettuce, \r\ncreamy mayo and crowned with a toasted sesame seed bun." },
                new Menu() { Id = 6, Name = "Double Cheeseburger", CategoryId = 1, UnitPrice = 185, Image = "doublecheeseburger.png", Description = "The Boost Burger's Double Cheeseburger features two 100% pure all beef patties \r\nseasoned with just a pinch of salt and pepper. It's topped with tangy pickles, \r\nchopped onions, ketchup, mustard, and two melty American cheese slices." },
                new Menu() { Id = 7, Name = "McChicken Sandwich", CategoryId = 2, UnitPrice = 150, Image = "mcchicken.png", Description = "McChicken® Sandwich. Crispy coated chicken with lettuce and our sandwich sauce, \r\nin a soft, sesame-topped bun. A true classic.\r\n" },
                new Menu() { Id = 8, Name = "Whopper", CategoryId = 1, UnitPrice = 197, Image = "whopper.png", Description = "The Whopper is a hamburger consisting of a flame-grilled 4 oz (110 g) beef patty, \r\nsesame seed bun, mayonnaise, lettuce, tomato, pickles, ketchup, and sliced onion. \r\nOptional ingredients such as American cheese, bacon, mustard, guacamole or \r\njalapeño peppers may be added upon request." },
                new Menu() { Id = 9, Name = "Happy Kids", CategoryId = 3, UnitPrice = 145, Image = "happykids.png", Description = "Charbroiled all-beef patty topped with dill pickles, ketchup and mustard on a \r\nplain bun. Served with kid's drink and kid's fry." },
                new Menu() { Id = 10, Name = "Plant Based Whopper", CategoryId = 4, UnitPrice = 165, Image = "plantbasedwhopper.png", Description = "A juicy flame-grilled, 100% plant-based patty topped with creamy mayonnaise, \r\nfreshly cut iceberg lettuce, juicy tomatoes, sliced white onions, crunchy pickles, \r\nand ketchup on a toasted sesame seed bun." }
                );

            builder.Entity<Drink>().HasData(
                new Drink() { Id = 1, Name = "Coca Cola", Image = "cocacola.png" },
                new Drink() { Id = 2, Name = "Fanta", Image = "fanta.png" },
                new Drink() { Id = 3, Name = "Ayran", Image = "ayran.png" },
                new Drink() { Id = 4, Name = "Sprite", Image = "sprite.png" },
                new Drink() { Id = 5, Name = "Coca Cola Zero", Image = "cocacolazero.png" },
                new Drink() { Id = 6, Name = "Fuse Tea Lemon", Image = "fusetealemon.png" },
                new Drink() { Id = 7, Name = "Fuse Tea Peach", Image = "fuseteapeach.png" }
                );

            builder.Entity<Extra>().HasData(
                new Extra() { Id = 1, Name = "Ketchup", UnitPrice = 5 },
                new Extra() { Id = 2, Name = "Mayonaise", UnitPrice = 5 },
                new Extra() { Id = 3, Name = "Mustard", UnitPrice = 10 },
                new Extra() { Id = 4, Name = "Chipotle", UnitPrice = 15 },
                new Extra() { Id = 5, Name = "Ranch", UnitPrice = 10 },
                new Extra() { Id = 6, Name = "BBQ", UnitPrice = 15 }
                );
        }
    }

   
}
