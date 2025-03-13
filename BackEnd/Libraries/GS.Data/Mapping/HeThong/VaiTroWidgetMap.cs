//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 20/5/2020
//----------------------------------------------------------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Core.Domain.HeThong;

namespace GS.Data.Mapping.QL
{
    public partial class VaiTroWidgetMap: GSEntityTypeConfiguration<VaiTroWidget>
	{
     public override void Configure(EntityTypeBuilder<VaiTroWidget> builder)
        { 
            builder.ToTable("QL_VAI_TRO_WIDGET");
            builder.HasKey(c => new { c.VAI_TRO_ID, c.WIDGET_ID });
            builder.Property(mapping => mapping.VAI_TRO_ID).HasColumnName("VAI_TRO_ID");
            builder.Property(mapping => mapping.WIDGET_ID).HasColumnName("WIDGET_ID");
            builder.HasOne(c => c.widget).WithMany().HasForeignKey(c => c.WIDGET_ID);
            builder.HasOne(c => c.vaiTro).WithMany().HasForeignKey(c => c.VAI_TRO_ID);
            builder.Ignore(mapping => mapping.ID);
            base.Configure(builder);
        }	
	}
}
