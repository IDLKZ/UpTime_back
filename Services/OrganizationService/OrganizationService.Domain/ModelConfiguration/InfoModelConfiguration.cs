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
    public class InfoModelConfiguration : IEntityTypeConfiguration<InfoModel>
    {
        public void Configure(EntityTypeBuilder<InfoModel> builder)
        {
            builder.HasOne<InfoTypeModel>(p => p.InfoType).WithMany().HasForeignKey(p => p.InfoTypeId).OnDelete(DeleteBehavior.SetNull);
            builder.HasOne<OrganizationModel>(p => p.Organization).WithMany().HasForeignKey(p => p.OrganizationId).OnDelete(DeleteBehavior.SetNull);
            builder.HasOne<BranchModel>(p => p.Branch).WithMany().HasForeignKey(p => p.BranchId).OnDelete(DeleteBehavior.SetNull);
            builder.Property(p => p.Value).IsRequired();
            builder.Property(p => p.Status).IsRequired();




        }
    }
}
