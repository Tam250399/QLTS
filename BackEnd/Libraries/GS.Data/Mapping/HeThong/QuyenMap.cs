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
    public partial class QuyenMap : GSEntityTypeConfiguration<Quyen>
    {
        public override void Configure(EntityTypeBuilder<Quyen> builder)
        {
            builder.ToTable("QL_QUYEN");
            builder.HasKey(c => c.ID);
            base.Configure(builder);
        }
    }
}
