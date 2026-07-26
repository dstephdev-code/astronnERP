using AstronnERP.Domain.Purchasing.Master;
using AstronnERP.Domain.SharedObjects.Errors;

namespace AstronnERP.Domain.UnitTests
{
    public class Purchasing_Product
    {
        [Fact]
        public void Product_WhenNameAndCodeValid_ShouldCreate()
        {
            var successProduct = CreateSuccessTestProduct();

            Assert.IsType<Product>(successProduct);
            Assert.Equal("Product", successProduct.Name);
            Assert.Equal("Code", successProduct.Code);
            Assert.False(successProduct.IsService);
            Assert.True(successProduct.HasSerialNumber);
        }
        [Fact]
        public void Product_WhenChangeNameSame_ShouldFail()
        {
            var successProduct = CreateSuccessTestProduct();

            Assert.IsType<Product>(successProduct);
            Assert.Equal("Product", successProduct.Name);

            var result = successProduct.ChangeName("Product");

            Assert.True(result.IsFailed);
        }
        [Fact]
        public void Product_WhenChangeCodeSame_ShouldFail()
        {
            var successProduct = CreateSuccessTestProduct();

            Assert.IsType<Product>(successProduct);
            Assert.Equal("Code", successProduct.Code);

            var result = successProduct.ChangeCode("Code");

            Assert.True(result.IsFailed);
        }
        [Fact]
        public void Product_WhenChangeNameValid_ShouldSuccess()
        {
            var successProduct = CreateSuccessTestProduct();

            Assert.IsType<Product>(successProduct);
            Assert.Equal("Product", successProduct.Name);

            var result = successProduct.ChangeName("Detail");

            Assert.True(result.IsSuccess);
            Assert.Equal("Detail", successProduct.Name);
        }
        [Fact]
        public void Product_WhenChangeCodeValid_ShouldSuccess()
        {
            var successProduct = CreateSuccessTestProduct();

            Assert.IsType<Product>(successProduct);
            Assert.Equal("Code", successProduct.Code);

            var result = successProduct.ChangeCode("11.2F");

            Assert.True(result.IsSuccess);
            Assert.Equal("11.2F", successProduct.Code);
        }

        private static Product CreateSuccessTestProduct() => Product.CreateNew("Product", "Code", false, true).ValueOrDefault;
    }
}
