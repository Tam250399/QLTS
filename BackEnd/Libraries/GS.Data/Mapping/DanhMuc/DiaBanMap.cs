//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.DanhMuc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GS.Data.Mapping.DanhMuc
{
    public partial class DiaBanMap : GSEntityTypeConfiguration<DiaBan>
    {
        public override void Configure(EntityTypeBuilder<DiaBan> builder)
        {
            builder.ToTable("DM_DIA_BAN");
            builder.HasKey(c => c.ID);
            builder.HasOne(t => t.Quocgia)
               .WithMany()
               .HasForeignKey(t => t.QUOC_GIA_ID);

            builder.HasOne(t => t.DiaBanCha)
              .WithMany()
              .HasForeignKey(t => t.PARENT_ID);

            builder.Ignore(c => c.LoaiDiaBan);
            builder.Ignore(c => c.TrangThai);
            base.Configure(builder);
        }
    }
}
