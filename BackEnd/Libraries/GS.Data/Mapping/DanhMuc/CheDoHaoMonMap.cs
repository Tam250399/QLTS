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
    public partial class CheDoHaoMonMap : GSEntityTypeConfiguration<CheDoHaoMon>
    {
        public override void Configure(EntityTypeBuilder<CheDoHaoMon> builder)
        {
            builder.ToTable("DM_CHE_DO_HAO_MON");
            builder.HasKey(c => c.ID);
            base.Configure(builder);
        }
    }
}
