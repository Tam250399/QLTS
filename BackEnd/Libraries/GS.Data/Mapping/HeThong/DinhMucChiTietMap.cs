//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 18/6/2021
//----------------------------------------------------------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Core.Domain.HeThong;

namespace GS.Data.Mapping.HeThong
{
    public partial class DinhMucChiTietMap: GSEntityTypeConfiguration<DinhMucChiTiet>
	{
     public override void Configure(EntityTypeBuilder<DinhMucChiTiet> builder)
        { 
            builder.ToTable("QL_DINH_MUC_CHI_TIET");
            //builder.HasKey(c => c.ID);    

            builder.HasKey(c => new { c.CHUC_DANH_ID, c.LOAI_TAI_SAN_ID,c.DINH_MUC_ID });
            builder.Property(mapping => mapping.DINH_MUC_ID).HasColumnName("DINH_MUC_ID");
            builder.Property(mapping => mapping.CHUC_DANH_ID).HasColumnName("CHUC_DANH_ID");
            builder.Property(mapping => mapping.LOAI_TAI_SAN_ID).HasColumnName("LOAI_TAI_SAN_ID");
            builder.Ignore(mapping => mapping.ID);
            base.Configure(builder);
            
        }	
	}
}
