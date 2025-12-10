// <copyright file="AppDbContext.cs" company="Cole Clark">
// Copyright (c) Cole Clark. Licensed under the MIT License.
// </copyright>

namespace URL_Shortening_Service.Backend.Models
{
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// class used to design the database with a table of shortURLs.
    /// </summary>
    /// <param name="options">DbContextOptions for the class.</param>
    public class AppDbContext(DbContextOptions<AppDbContext> options)
    : DbContext(options)
    {
        /// <summary>
        /// Gets or Sets the shortURLs DbSet.
        /// </summary>
        public DbSet<ShortURL> ShortURLs { get; set; }

        /// <summary>
        /// Creates the short_urls table in the database.
        /// </summary>
        /// <param name="modelBuilder">used to create the short_urls table.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ShortURL>(entity =>
            {
                entity.ToTable("short_urls");

                // Configure properties
                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasMaxLength(2000);

                entity.Property(e => e.ShortCode)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdatedAt)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.AccessCount)
                    .HasDefaultValue(0);

                // Create unique index on ShortCode
                entity.HasIndex(e => e.ShortCode)
                    .IsUnique();
            });
        }
    }
}