//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 29/5/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.HeThong;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GS.Data.Mapping.HeThong
{
    public partial class NhanHienThiMap : GSEntityTypeConfiguration<NhanHienThi>
    {
        public override void Configure(EntityTypeBuilder<NhanHienThi> builder)
        {
            builder.ToTable("QL_NHAN_HIEN_THI");
            builder.HasKey(c => c.ID);
            //builder.Property(c => c.ID).ForOracleUseSequenceHiLo("QL_NHAN_HIEN_THI_SEQ");
            base.Configure(builder);
        }
    }
}
