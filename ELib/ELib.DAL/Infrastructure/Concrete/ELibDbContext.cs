﻿using System;
using ELib.Domain.Entities;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Diagnostics;

namespace ELib.DAL.Infrastructure.Concrete
{
    public partial class ELibDbContext : IdentityDbContext<ApplicationUser>
    {
        public ELibDbContext()
           : base("ELibDb")
        {
            this.Database.Log = message => Debug.Write(message);
            //  Database.SetInitializer<ELibDbContext>(new ELibDbInitializer());

        }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<AuthorGenre> AuthorGenres { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<BookAuthor> BookAuthors { get; set; }
        public virtual DbSet<BookInstance> BookInstances { get; set; }
        public virtual DbSet<BookGenre> BookGenres { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Publisher> Publishers { get; set; }
        public virtual DbSet<RatingBook> RatingBooks { get; set; }
        public virtual DbSet<RatingComment> RatingComments { get; set; }
        public virtual DbSet<UserBookStatus> UserBookStatus { get; set; }
        public virtual DbSet<Subgenre> Subgenres { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Favorite> Favorites { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
                .HasMany(e => e.AuthorGenres)
                .WithRequired(e => e.Author)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Author>()
                .HasMany(e => e.BookAuthors)
                .WithRequired(e => e.Author)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Book>()
                .Property(e => e.Isbn)
                .IsUnicode(false);

            modelBuilder.Entity<Book>()
                .HasMany(e => e.BookAuthors)
                .WithRequired(e => e.Book)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Book>()
                .HasMany(e => e.BookInstances)
                .WithRequired(e => e.Book)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Book>()
                .HasMany(e => e.BookGenres)
                .WithRequired(e => e.Book)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Book>()
                .HasMany(e => e.RatingBooks)
                .WithRequired(e => e.Book)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Book>()
                .HasMany(e => e.UserBookStatus)
                .WithRequired(e => e.Book)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Comment>()
                .HasMany(e => e.RatingComments)
                .WithRequired(e => e.Comment)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Genre>()
                .HasMany(e => e.AuthorGenres)
                .WithRequired(e => e.Genre)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Genre>()
                .HasMany(e => e.BookGenres)
                .WithRequired(e => e.Genre)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Language>()
                .HasMany(e => e.Books)
                .WithRequired(e => e.Language)
                .HasForeignKey(e => e.PublishLangId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Language>()
                .HasMany(e => e.Books1)
                .WithOptional(e => e.Language1)
                .HasForeignKey(e => e.OriginalLangId);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.RatingBooks)
                .WithRequired(e => e.Person)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.RatingComments)
                .WithRequired(e => e.Person)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.UserBookStatus)
                .WithRequired(e => e.Person)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);
            
            modelBuilder.Entity<Publisher>()
                .HasMany(e => e.Books)
                .WithRequired(e => e.Publisher)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Subgenre>()
                .HasMany(e => e.Books)
                .WithRequired(e => e.Subgenre)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });

            modelBuilder.Entity<Category>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.Books)
                .WithRequired(e => e.Category)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
                 .HasMany(p => p.Favorites)
                 .WithRequired(p => p.User)
                 .HasForeignKey(p => p.UserId)
                 .WillCascadeOnDelete(false);

            modelBuilder.Entity<Book>()
                .HasMany(b => b.Favorites)
                .WithRequired(p => p.Book)
                .HasForeignKey(p => p.BookId)
                .WillCascadeOnDelete(false);

        }
        public static ELibDbContext Create()
        {
            return new ELibDbContext();
        }
       
    }
}
