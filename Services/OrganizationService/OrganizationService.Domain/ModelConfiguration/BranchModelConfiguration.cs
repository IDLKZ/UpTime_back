using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrganizationService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationService.Domain.ModelConfiguration
{
    public class BranchModelConfiguration : IEntityTypeConfiguration<BranchModel>
    {
        public void Configure(EntityTypeBuilder<BranchModel> builder)
        {
            builder.HasOne<OrganizationModel>(p => p.Organization).WithMany().HasForeignKey(p => p.OrganizationId).OnDelete(DeleteBehavior.SetNull);
            builder.HasOne<AreaModel>(p => p.Area).WithMany().HasForeignKey(p => p.AreaId).OnDelete(DeleteBehavior.SetNull);
            builder.Property(p => p.TitleRu).HasMaxLength(255).IsRequired();
            builder.Property(p => p.TitleEn).HasMaxLength(255).IsRequired();
            builder.Property(p => p.TitleKk).HasMaxLength(255).IsRequired();
            builder.Property(p => p.DescriptionRu).IsRequired();
            builder.Property(p => p.AddressRu).IsRequired();
            builder.Property(p => p.Status).IsRequired();
            builder.Property(p => p.Code).HasMaxLength(255).IsRequired();
        }
    }
}
