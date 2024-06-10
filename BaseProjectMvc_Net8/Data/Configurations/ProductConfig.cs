using BaseProjectMvc_Net8.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseProjectMvc_Net8.Data.Configurations
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Product_Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Price)
                .IsRequired()
                .HasColumnType("decimal(10, 2)")
                .HasDefaultValue(0.00m);

            builder.HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey(p => p.Category_Id);
        }
    }
}
