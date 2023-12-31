﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BloggWebAPI.Models;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Blogg> Blogger { get; set; }
    public DbSet<Post> Poster { get; set; }
    public DbSet<Kommentar> Kommentarer { get; set; }
    public DbSet<Tagg> Tagger { get; set; }
    public DbSet<Abonnement> Abonnementer { get; set; }
    public DbSet<Notifikasjon> Notifikasjoner { get; set; }
    public DbSet<Like> Likes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Abonnement>()
            .HasOne(a => a.Blogg)
            .WithMany()
            .HasForeignKey(a => a.BloggId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Like>()
            .HasOne(l => l.Post)
            .WithMany(p => p.Likes)
            .HasForeignKey(l => l.PostId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Like>()
            .HasOne(l => l.Kommentar)
            .WithMany(k => k.Likes)
            .HasForeignKey(l => l.KommentarId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
