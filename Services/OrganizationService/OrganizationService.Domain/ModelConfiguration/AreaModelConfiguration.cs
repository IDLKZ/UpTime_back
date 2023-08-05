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
    public class AreaModelConfiguration : IEntityTypeConfiguration<AreaModel>
    {
        public void Configure(EntityTypeBuilder<AreaModel> builder)
        {
            builder.Property(p=>p.TitleRu).HasMaxLength(255).IsRequired();
            builder.Property(p=>p.TitleEn).HasMaxLength(255).IsRequired();
            builder.Property(p=>p.TitleKk).HasMaxLength(255).IsRequired();
            builder.Property(p=>p.Code).HasMaxLength(255).IsRequired();
            builder.Property(p=>p.Status).IsRequired();
        }
    }
}
