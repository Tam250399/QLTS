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
    public partial class NguoiDungMap : GSEntityTypeConfiguration<NguoiDung>
    {
        public override void Configure(EntityTypeBuilder<NguoiDung> builder)
        {
            builder.ToTable("QL_NGUOI_DUNG");
            builder.HasKey(c => c.ID);

            builder.HasMany(p => p.NguoiDungDonViMappings)
                .WithOne(p => p.nguoiDung)
                .HasForeignKey(p=>p.NGUOI_DUNG_ID);

            builder.Ignore(t => t.nguoiDungStatusID);
            builder.Ignore(t => t.vaiTroNguoiDungMappings);
            builder.Ignore(t => t.VaiTro);
            base.Configure(builder);
        }
    }
}
