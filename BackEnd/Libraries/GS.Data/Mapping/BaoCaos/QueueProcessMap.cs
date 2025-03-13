using GS.Core.Domain.BaoCaos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Data.Mapping.BaoCaos
{
    public partial class QueueProcessMap : GSEntityTypeConfiguration<QueueProcess>
    {
        public override void Configure(EntityTypeBuilder<QueueProcess> builder)
        {
            builder.ToTable("BC_QUEUE_PROCESS");
            builder.HasKey(c => c.ID);
            builder.Property(c => c.DATA_JSON).HasColumnType("CLOB");
            builder.Property(c => c.SEARCH_MODEL_JSON).HasColumnType("CLOB");
            builder.Property(c => c.RESPONSE).HasColumnType("CLOB");
            base.Configure(builder);
        }
    }
    //public partial class QueueProcessSearchMap : GSEntityTypeConfiguration<QueueProcessSearch>
    //{
    //    public override void Configure(EntityTypeBuilder<QueueProcessSearch> builder)
    //    {
    //        builder.ToTable("BC_QUEUE_PROCESS");
    //        builder.HasKey(c => c.ID);
    //        builder.Property(c => c.RESPONSE).HasColumnType("CLOB");
    //        base.Configure(builder);
    //    }
    //}
}
