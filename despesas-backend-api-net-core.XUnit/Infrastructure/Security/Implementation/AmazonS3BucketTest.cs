using despesas_backend_api_net_core.Infrastructure.Security.Implementation;
using despesas_backend_api_net_core.Infrastructure.Security;
using Microsoft.Extensions.Configuration;
using Xunit.Extensions.Ordering;
using System.Reflection;
using MySqlX.XDevAPI.Common;
using Moq;
using System;
using Amazon.S3;

namespace Test.XUnit.Infrastructure.Security.Implementation
{
    [Order(20)]
    public class AmazonS3BucketTest
    {
        private IAmazonS3Bucket _amazonS3Bucket;
        private readonly string _accessKey = null;
        private readonly string _secretAccessKey = null;
        private readonly string _s3ServiceUrl = null;
        private readonly string _bucketName = null;
        private readonly string _regionEndpoint = null;

        public AmazonS3BucketTest()
        {
            _amazonS3Bucket = AmazonS3Bucket.GetInstance;
        }


        [Fact, Order(1)]
        public void Should_Returns_Instance_Of_IAmazonS3Bucket()
        {

            // Assert
            Assert.NotNull(_amazonS3Bucket);
            Assert.IsAssignableFrom<IAmazonS3Bucket>(_amazonS3Bucket);
        }


        [Fact, Order(3)]
        public async Task WritingAnObjectAsync_Should_Return_Url()
        {
            // Arrange
            var perfilFile = new ImagemPerfilUsuarioVM
            {
                Name = "test-image.jpg",
                ContentType = "image/jpeg",
                //  Arquivo = new byte[] { 0x01, 0x02, 0x03 } // Sample image data
            };
            var mockAmazonS3Bucket = new Mock<IAmazonS3Bucket>(MockBehavior.Strict);
            mockAmazonS3Bucket.Setup(s => s.WritingAnObjectAsync(perfilFile)).ReturnsAsync($"https://{_bucketName}.s3.amazonaws.com/{perfilFile.Name}");

            // Act
            var url = await mockAmazonS3Bucket.Object.WritingAnObjectAsync(perfilFile);

            // Assert
            Assert.NotNull(url);
            Assert.StartsWith($"https://{_bucketName}.s3.amazonaws.com/", url);
        }

        [Fact, Order(4)]
        public async Task WritingAnObjectAsync_Should_Throws_Exception()
        {
            // Arrange
            var perfilFile = new ImagemPerfilUsuarioVM
            {
                Name = "test-image.jpg",
                ContentType = "image/jpeg",
                //Arquivo = new byte[] { 0x01, 0x02, 0x03 } // Sample image data
            };
            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(async () => await _amazonS3Bucket.WritingAnObjectAsync(perfilFile));

            Assert.NotNull(exception);
            Assert.IsType<Exception>(exception);
            Assert.Equal("AmazonS3Bucket_WritingAnObjectAsync_Errro ", exception.Message);

        }

        [Fact, Order(5)]
        public async Task DeleteObjectNonVersionedBucketAsync_Should_Return_True()
        {
            // Arrange
            var perfilFile = new ImagemPerfilUsuarioVM
            {
                Name = "test-image.jpg"
            };
            var mockAmazonS3Bucket = new Mock<IAmazonS3Bucket>(MockBehavior.Strict);

            mockAmazonS3Bucket.Setup(s => s.DeleteObjectNonVersionedBucketAsync(perfilFile)).ReturnsAsync(true);

            // Act
            var result = await mockAmazonS3Bucket.Object.DeleteObjectNonVersionedBucketAsync(perfilFile);

            // Assert
            Assert.True(result);
        }

        [Fact, Order(6)]
        public async Task DeleteObjectNonVersionedBucketAsync_Should_Throws_Erro_And_Return_False()
        {
            // Arrange
            var perfilFile = new ImagemPerfilUsuarioVM
            {
                Name = "non-existing-file.jpg"
            };

            // Act
            var result = await _amazonS3Bucket.DeleteObjectNonVersionedBucketAsync(perfilFile);

            // Assert
            Assert.False(result);
        }
    }
}
