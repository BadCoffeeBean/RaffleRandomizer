using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RaffleRandomizer.Core
{
	/// <summary>
	/// The EF Core database context. Don't change anything here unless you are familiar with EF Core.
	/// </summary>
	public partial class RaffleContext : DbContext
	{
		public RaffleContext()
		{
		}

		public RaffleContext(DbContextOptions<RaffleContext> options) : base(options)
		{
		}

		public virtual DbSet<Participant> Participants { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Participant>(entity =>
			{
				entity.ToTable("Participant");

				entity.Property(e => e.CreatedUtc)
					.HasPrecision(3)
					.HasDefaultValueSql("(getutcdate())");

				entity.Property(e => e.GrandPrizeEligible)
					.IsRequired()
					.HasDefaultValueSql("((1))");

				entity.Property(e => e.HireDate).HasColumnType("date");

				entity.Property(e => e.LastUpdateUtc).HasPrecision(3);

				entity.Property(e => e.MajorPrizeEligible)
					.IsRequired()
					.HasDefaultValueSql("((1))");

				entity.Property(e => e.MinorPrizeEligible)
					.IsRequired()
					.HasDefaultValueSql("((1))");

				entity.Property(e => e.ParticipantId).ValueGeneratedOnAdd();

				entity.Property(e => e.ResignDate).HasColumnType("date");
			});

			OnModelCreatingPartial(modelBuilder);
		}

		partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
	}
}
