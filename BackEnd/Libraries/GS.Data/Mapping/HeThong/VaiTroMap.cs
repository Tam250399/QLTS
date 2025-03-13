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
    public partial class VaiTroMap : GSEntityTypeConfiguration<VaiTro>
    {
        public override void Configure(EntityTypeBuilder<VaiTro> builder)
        {
            builder.ToTable("QL_VAI_TRO");
            builder.HasKey(c => c.ID);
            base.Configure(builder);
        }
    }
}
