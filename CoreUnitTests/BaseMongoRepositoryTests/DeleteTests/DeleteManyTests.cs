﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using AutoFixture;
using CoreUnitTests.Infrastructure;
using CoreUnitTests.Infrastructure.Model;
using FluentAssertions;
using MongoDbGenericRepository.DataAccess.Delete;
using Moq;
using Xunit;

namespace CoreUnitTests.BaseMongoRepositoryTests.DeleteTests;

public class DeleteManyTests : TestMongoRepositoryContext
{
    [Fact]
    public void WithDocuments_ShouldDeleteMany()
    {
        // Arrange
        var documents = Fixture.CreateMany<TestDocument>().ToList();
        var count = Fixture.Create<long>();
        Eraser = new Mock<IMongoDbEraser>();

        Eraser
            .Setup(x => x.DeleteMany<TestDocument, Guid>(It.IsAny<IEnumerable<TestDocument>>(), It.IsAny<CancellationToken>()))
            .Returns(count);

        // Act
        var result = Sut.DeleteMany(documents);

        // Assert
        result.Should().Be(count);
        Eraser.Verify(x => x.DeleteMany<TestDocument, Guid>(documents, CancellationToken.None), Times.Once);
    }

    [Fact]
    public void WithDocumentsAndCancellationToken_ShouldDeleteMany()
    {
        // Arrange
        var documents = Fixture.CreateMany<TestDocument>().ToList();
        var count = Fixture.Create<long>();
        var token = new CancellationToken(true);
        Eraser = new Mock<IMongoDbEraser>();

        Eraser
            .Setup(x => x.DeleteMany<TestDocument, Guid>(It.IsAny<IEnumerable<TestDocument>>(), It.IsAny<CancellationToken>()))
            .Returns(count);

        // Act
        var result = Sut.DeleteMany(documents, token);

        // Assert
        result.Should().Be(count);
        Eraser.Verify(x => x.DeleteMany<TestDocument, Guid>(documents, token), Times.Once);
    }

    [Fact]
    public void WithFilter_ShouldDeleteMany()
    {
        // Arrange
        var content = Fixture.Create<string>();
        var count = Fixture.Create<long>();

        Expression<Func<TestDocument, bool>> filter = x => x.SomeContent == content;

        Eraser = new Mock<IMongoDbEraser>();

        Eraser
            .Setup(x => x.DeleteMany<TestDocument, Guid>(It.IsAny<Expression<Func<TestDocument, bool>>>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .Returns(count);

        // Act
        var result = Sut.DeleteMany(filter);

        // Assert
        result.Should().Be(count);
        Eraser.Verify(x => x.DeleteMany<TestDocument, Guid>(filter, null, CancellationToken.None), Times.Once);
    }

    [Fact]
    public void WithFilterAndCancellationToken_ShouldDeleteMany()
    {
        // Arrange
        var content = Fixture.Create<string>();
        var count = Fixture.Create<long>();
        var token = new CancellationToken(true);

        Expression<Func<TestDocument, bool>> filter = x => x.SomeContent == content;

        Eraser = new Mock<IMongoDbEraser>();

        Eraser
            .Setup(x => x.DeleteMany<TestDocument, Guid>(It.IsAny<Expression<Func<TestDocument, bool>>>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .Returns(count);

        // Act
        var result = Sut.DeleteMany(filter, token);

        // Assert
        result.Should().Be(count);
        Eraser.Verify(x => x.DeleteMany<TestDocument, Guid>(filter, null, token), Times.Once);
    }

    [Fact]
    public void WithFilterAndPartitionKey_ShouldDeleteMany()
    {
        // Arrange
        var content = Fixture.Create<string>();
        var count = Fixture.Create<long>();
        var partitionKey = Fixture.Create<string>();

        Expression<Func<TestDocument, bool>> filter = x => x.SomeContent == content;

        Eraser = new Mock<IMongoDbEraser>();

        Eraser
            .Setup(x => x.DeleteMany<TestDocument, Guid>(It.IsAny<Expression<Func<TestDocument, bool>>>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .Returns(count);

        // Act
        var result = Sut.DeleteMany(filter, partitionKey);

        // Assert
        result.Should().Be(count);
        Eraser.Verify(x => x.DeleteMany<TestDocument, Guid>(filter, partitionKey, CancellationToken.None), Times.Once);
    }

    [Fact]
    public void WithFilterAndPartitionKeyAndCancellationToken_ShouldDeleteMany()
    {
        // Arrange
        var content = Fixture.Create<string>();
        var count = Fixture.Create<long>();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);

        Expression<Func<TestDocument, bool>> filter = x => x.SomeContent == content;

        Eraser = new Mock<IMongoDbEraser>();

        Eraser
            .Setup(x => x.DeleteMany<TestDocument, Guid>(It.IsAny<Expression<Func<TestDocument, bool>>>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .Returns(count);

        // Act
        var result = Sut.DeleteMany(filter, partitionKey, token);

        // Assert
        result.Should().Be(count);
        Eraser.Verify(x => x.DeleteMany<TestDocument, Guid>(filter, partitionKey, token), Times.Once);
    }

    #region Keyed

    [Fact]
    public void Keyed_WithDocuments_ShouldDeleteMany()
    {
        // Arrange
        var documents = Fixture.CreateMany<TestDocumentWithKey<int>>().ToList();
        var count = Fixture.Create<long>();
        Eraser = new Mock<IMongoDbEraser>();

        Eraser
            .Setup(x => x.DeleteMany<TestDocumentWithKey<int>, int>(It.IsAny<IEnumerable<TestDocumentWithKey<int>>>(), It.IsAny<CancellationToken>()))
            .Returns(count);

        // Act
        var result = Sut.DeleteMany<TestDocumentWithKey<int>, int>(documents);

        // Assert
        result.Should().Be(count);
        Eraser.Verify(x => x.DeleteMany<TestDocumentWithKey<int>, int>(documents, CancellationToken.None), Times.Once);
    }

    [Fact]
    public void Keyed_WithDocumentsAndCancellationToken_ShouldDeleteMany()
    {
        // Arrange
        var documents = Fixture.CreateMany<TestDocumentWithKey<int>>().ToList();
        var count = Fixture.Create<long>();
        var token = new CancellationToken(true);
        Eraser = new Mock<IMongoDbEraser>();

        Eraser
            .Setup(x => x.DeleteMany<TestDocumentWithKey<int>, int>(It.IsAny<IEnumerable<TestDocumentWithKey<int>>>(), It.IsAny<CancellationToken>()))
            .Returns(count);

        // Act
        var result = Sut.DeleteMany<TestDocumentWithKey<int>, int>(documents, token);

        // Assert
        result.Should().Be(count);
        Eraser.Verify(x => x.DeleteMany<TestDocumentWithKey<int>, int>(documents, token), Times.Once);
    }

    [Fact]
    public void Keyed_WithFilter_ShouldDeleteMany()
    {
        // Arrange
        var content = Fixture.Create<string>();
        var count = Fixture.Create<long>();

        Expression<Func<TestDocumentWithKey<int>, bool>> filter = x => x.SomeContent == content;

        Eraser = new Mock<IMongoDbEraser>();

        Eraser
            .Setup(x => x.DeleteMany<TestDocumentWithKey<int>, int>(It.IsAny<Expression<Func<TestDocumentWithKey<int>, bool>>>(), null, CancellationToken.None))
            .Returns(count);

        // Act
        var result = Sut.DeleteMany<TestDocumentWithKey<int>, int>(filter);

        // Assert
        result.Should().Be(count);
        Eraser.Verify(x => x.DeleteMany<TestDocumentWithKey<int>, int>(filter, null, CancellationToken.None), Times.Once);
    }

    [Fact]
    public void Keyed_WithFilterAndCancellationToken_ShouldDeleteMany()
    {
        // Arrange
        var content = Fixture.Create<string>();
        var count = Fixture.Create<long>();
        var token = new CancellationToken(true);

        Expression<Func<TestDocumentWithKey<int>, bool>> filter = x => x.SomeContent == content;

        Eraser = new Mock<IMongoDbEraser>();

        Eraser
            .Setup(x => x.DeleteMany<TestDocumentWithKey<int>, int>(It.IsAny<Expression<Func<TestDocumentWithKey<int>, bool>>>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .Returns(count);

        // Act
        var result = Sut.DeleteMany<TestDocumentWithKey<int>, int>(filter, token);

        // Assert
        result.Should().Be(count);
        Eraser.Verify(x => x.DeleteMany<TestDocumentWithKey<int>, int>(filter, null, token), Times.Once);
    }

    [Fact]
    public void Keyed_WithFilterAndPartitionKey_ShouldDeleteMany()
    {
        // Arrange
        var content = Fixture.Create<string>();
        var count = Fixture.Create<long>();
        var partitionKey = Fixture.Create<string>();

        Expression<Func<TestDocumentWithKey<int>, bool>> filter = x => x.SomeContent == content;

        Eraser = new Mock<IMongoDbEraser>();

        Eraser
            .Setup(x => x.DeleteMany<TestDocumentWithKey<int>, int>(It.IsAny<Expression<Func<TestDocumentWithKey<int>, bool>>>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .Returns(count);

        // Act
        var result = Sut.DeleteMany<TestDocumentWithKey<int>, int>(filter, partitionKey);

        // Assert
        result.Should().Be(count);
        Eraser.Verify(x => x.DeleteMany<TestDocumentWithKey<int>, int>(filter, partitionKey, CancellationToken.None), Times.Once);
    }

    [Fact]
    public void Keyed_WithFilterAndPartitionKeyAndCancellationToken_ShouldDeleteMany()
    {
        // Arrange
        var content = Fixture.Create<string>();
        var count = Fixture.Create<long>();
        var partitionKey = Fixture.Create<string>();
        var token = new CancellationToken(true);

        Expression<Func<TestDocumentWithKey<int>, bool>> filter = x => x.SomeContent == content;

        Eraser = new Mock<IMongoDbEraser>();

        Eraser
            .Setup(x => x.DeleteMany<TestDocumentWithKey<int>, int>(It.IsAny<Expression<Func<TestDocumentWithKey<int>, bool>>>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .Returns(count);

        // Act
        var result = Sut.DeleteMany<TestDocumentWithKey<int>, int>(filter, partitionKey, token);

        // Assert
        result.Should().Be(count);
        Eraser.Verify(x => x.DeleteMany<TestDocumentWithKey<int>, int>(filter, partitionKey, token), Times.Once);
    }
    #endregion
}
