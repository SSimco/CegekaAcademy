using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetShelter.DataAccessLayer.Models;

namespace PetShelter.DataAccessLayer.Configuration;

public class FundraiserConfiguration : IEntityTypeConfiguration<Fundraiser>
{
    public void Configure(EntityTypeBuilder<Fundraiser> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name).IsRequired().HasMaxLength(127);
        builder.Property(p => p.GoalValue).IsRequired();
        builder.Property(p => p.CurrentRaisedAmount).IsRequired();
        builder.Property(p => p.DueDate).IsRequired();
        builder.Property(p => p.CreationDate).IsRequired();
        builder.Property(p => p.Status).IsRequired().HasMaxLength(50);

        builder.HasOne(p => p.Owner).WithMany(p => p.FundraisersCreated).HasForeignKey(p => p.OwnerId).IsRequired();
        builder.HasMany(p => p.Donors)
            .WithMany(p => p.FundraisersDonatedTo)
            .UsingEntity<Dictionary<string, object>>(
                "FundraiserDonations",
                p => p.HasOne<Person>()
                    .WithMany()
                    .HasForeignKey("DonorId")
                    .OnDelete(DeleteBehavior.NoAction),
                p => p.HasOne<Fundraiser>()
                      .WithMany()
                      .HasForeignKey("FundraiserId")
                      .OnDelete(DeleteBehavior.Cascade));
    }
}