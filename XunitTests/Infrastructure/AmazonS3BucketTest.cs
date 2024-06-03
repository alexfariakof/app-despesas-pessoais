using Business.Dtos.Parser;
using Business.Dtos.v1;
using Despesas.Infrastructure.Amazon;
using Despesas.Infrastructure.Amazon.Abstractions;

namespace XUnit.Infrastructure;
public sealed class AmazonS3BucketTest
{
    private IAmazonS3Bucket _amazonS3Bucket;

    private readonly string _bucketName;

    public AmazonS3BucketTest()
    {
        _amazonS3Bucket = AmazonS3Bucket.Instance;
        _bucketName = "Amazon.Server";
    }

    [Fact]
    public void Should_Returns_Instance_Of_IAmazonS3Bucket()
    {
        // Assert
        Assert.NotNull(_amazonS3Bucket);
        Assert.IsAssignableFrom<IAmazonS3Bucket>(_amazonS3Bucket);
    }

    [Fact]
    public async Task WritingAnObjectAsync_Should_Return_Url()
    {
        // Arrange
        var perfilFileVM = new ImagemPerfilDto
        {
            Name = "test-image.jpg",
            ContentType = "image/jpeg",
            Arquivo = new byte[] { 0x01, 0x02, 0x03 }
        };


        var mockAmazonS3Bucket = new Mock<IAmazonS3Bucket>(MockBehavior.Strict);
        mockAmazonS3Bucket.Setup(s => s.WritingAnObjectAsync(It.IsAny<ImagemPerfilUsuario>(), It.IsAny<byte[]>())).ReturnsAsync($"https://{_bucketName}.s3.amazonaws.com/{perfilFileVM.Name}");

        // Act
        var url = await mockAmazonS3Bucket.Object.WritingAnObjectAsync(new ImagemPerfilUsuarioParser().Parse(perfilFileVM), perfilFileVM.Arquivo);

        // Assert
        Assert.NotNull(url);
        Assert.StartsWith($"https://{_bucketName}.s3.amazonaws.com/", url);
    }

    [Fact]
    public async Task WritingAnObjectAsync_Should_Throws_Exception()
    {
        // Arrange
        var mockAmazonS3Bucket = new Mock<IAmazonS3Bucket>(MockBehavior.Strict);
        mockAmazonS3Bucket.Setup(s => s.WritingAnObjectAsync(It.IsAny<ImagemPerfilUsuario>(), It.IsAny<byte[]>())).Throws(() => new Exception("AmazonS3Bucket_WritingAnObjectAsync_Errro"));
        var perfilFileVM = new ImagemPerfilDto
        {
            Name = "test-image.jpg",
            ContentType = "image/jpeg",
            Arquivo = new byte[] { 0x01, 0x02, 0x03 } // Sample image data
        };

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(async () => await mockAmazonS3Bucket.Object.WritingAnObjectAsync(new ImagemPerfilUsuarioParser().Parse(perfilFileVM), perfilFileVM.Arquivo));
        Assert.NotNull(exception);
        Assert.IsType<Exception>(exception);
        Assert.Equal("AmazonS3Bucket_WritingAnObjectAsync_Errro", exception.Message);
    }

    [Fact]
    public async Task DeleteObjectNonVersionedBucketAsync_Should_Return_True()
    {
        // Arrange
        var perfilFile = new ImagemPerfilUsuario { Name = "test-image.jpg" };
        var mockAmazonS3Bucket = new Mock<IAmazonS3Bucket>(MockBehavior.Strict);
        mockAmazonS3Bucket.Setup(s => s.DeleteObjectNonVersionedBucketAsync(perfilFile)).ReturnsAsync(true);

        // Act
        var result = await mockAmazonS3Bucket.Object.DeleteObjectNonVersionedBucketAsync(perfilFile);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task DeleteObjectNonVersionedBucketAsync_Should_Throws_Erro_And_Return_False()
    {
        // Arrange
        var mockAmazonS3Bucket = new Mock<IAmazonS3Bucket>(MockBehavior.Strict);
        var perfilFile = new ImagemPerfilUsuario { Name = "non-existing-file.jpg" };
        mockAmazonS3Bucket.Setup(s => s.DeleteObjectNonVersionedBucketAsync(perfilFile)).ReturnsAsync(() => false);

        // Act
        var result = await mockAmazonS3Bucket.Object.DeleteObjectNonVersionedBucketAsync(perfilFile);

        // Assert
        Assert.False(result);
    }
}
